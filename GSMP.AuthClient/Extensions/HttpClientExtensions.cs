using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace GSMP.AuthClient.Extensions
{
	public static class HttpClientExtensions
	{
		#region Private Methods

		private static void HandleFinishSendAsyncError(Exception ex, CancellationTokenSource cts)
		{
			if (cts.IsCancellationRequested && ex is HttpRequestException)
				throw new OperationCanceledException(cts.Token);
		}

		private static async Task<HttpResponseMessage> FinishSendAsyncBuffered(Task<HttpResponseMessage> sendTask, CancellationTokenSource cts)
		{
			HttpResponseMessage response = null;
			try
			{
				response = await sendTask.ConfigureAwait(false);
				if (response == null)
					throw new InvalidOperationException("Handler did not return a response message.");

				if (response.Content != null)
					await response.Content.LoadIntoBufferAsync().ConfigureAwait(false);

				return response;
			}
			catch (Exception ex)
			{
				response?.Dispose();
				HandleFinishSendAsyncError(ex, cts);
				throw;
			}
			finally
			{
				cts?.Dispose();
			}
		}

		private static async Task<HttpResponseMessage> FinishSendAsyncUnbuffered(Task<HttpResponseMessage> sendTask, CancellationTokenSource cts)
		{
			try
			{
				var response = await sendTask.ConfigureAwait(false);
				if (response == null)
					throw new InvalidOperationException("Handler did not return a response message.");

				return response;
			}
			catch (Exception ex)
			{
				HandleFinishSendAsyncError(ex, cts);
				throw;
			}
			finally
			{
				cts?.Dispose();
			}
		}

		private static Task<HttpResponseMessage> SendAsync(
			HttpClient httpClient,
			HttpRequestMessage request,
			HttpCompletionOption completionOption,
			CancellationToken cancellationToken)
		{
			if (request == null)
				throw new ArgumentNullException(nameof(request));

			var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cancellationToken);
			cts.CancelAfter(TimeSpan.FromMinutes(1));

			Task<HttpResponseMessage> sendTask;
			try
			{
				sendTask = httpClient.SendAsync(request, cts.Token);
			}
			catch
			{
				cts.Dispose();
				throw;
			}

			return completionOption == HttpCompletionOption.ResponseContentRead && !string.Equals(request.Method.Method, "HEAD", StringComparison.OrdinalIgnoreCase)
				? FinishSendAsyncBuffered(sendTask, cts)
				: FinishSendAsyncUnbuffered(sendTask, cts);
		}

		#endregion

		public static Task<HttpResponseMessage> PatchAsync(this HttpClient httpClient, string requestUri, HttpContent content, CancellationToken ct)
		{
			var method = new HttpMethod("PATCH");
			var uri = string.IsNullOrEmpty(requestUri)
				? null
				: new Uri(requestUri, UriKind.RelativeOrAbsolute);

			var request = new HttpRequestMessage(method, uri)
			{
				//Version = new Version(2, 0),
				Content = content
			};

			return SendAsync(httpClient, request, HttpCompletionOption.ResponseContentRead, ct);
		}

		public static HttpClient SetAuthorization(this HttpClient httpClient, string token, string scheme = "Bearer")
		{
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, token);
			return httpClient;
		}
	}
}
