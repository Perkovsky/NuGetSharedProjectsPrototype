using System;
using System.Collections.Specialized;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace GSMP.Telemetry.GoogleAnalytics
{
    public class MeasurementProtocolClient
    {
        private readonly ClientOptions _context;

        private readonly HttpClient _httpClient;

        public MeasurementProtocolClient(IOptions<ClientOptions> context, HttpClient httpClient)
        {
            _context = context.Value;
            _httpClient = httpClient;

            _httpClient.BaseAddress = _context.BaseUrl;
        }

        public async Task PageViewAsync(RequestPageView request, CancellationToken cancellationToken = default)
        {
            var parameters = ToParameters(request);

            await PostAsync(parameters, cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task TellThatRequestEnded(RequestTiming request, CancellationToken cancellationToken = default)
        {
            var parameters = ToParameters(request);

            await PostAsync(parameters, cancellationToken)
                .ConfigureAwait(false);
        }

        private NameValueCollection ToParameters(BaseRequest request)
        {
            var parameters = new NameValueCollection
            {
                ["tid"] = _context.TrackingId,
                ["cid"] = request.ClientId,
                ["uid"] = request.UserId,
                ["v"] = "1", // Protocol version. For now "1" only possible
                ["t"] = request.HitType // Possible values: 'pageview', 'screenview', 'event', 'transaction', 'item', 'social', 'exception', 'timing'
            };

            AddParameter("ds", _context.DataSource, parameters);
            AddParameter("dr", request.DocumentReferrer, parameters);
            AddParameter("dh", request.DocumentHostName, parameters);
            AddParameter("dp", request.DocumentPath, parameters);
            AddParameter("aid", request.ApplicationId, parameters);
            AddParameter("an", _context.ApplicationName, parameters);
            AddParameter("uip", request.UserIp, parameters);
            AddParameter("ua", request.UserAgent, parameters);
            
            return parameters;
        }

        private static void AddParameter(string key, string value, NameValueCollection parameters)
        {
            if (string.IsNullOrEmpty(value))
                return;

            parameters[key] = value;
        }

        private NameValueCollection ToParameters(RequestTiming request)
        {
            var parameters = ToParameters(request as BaseRequest);
            parameters["utc"] = request.Category;
            parameters["utv"] = request.Variable;
            parameters["utl"] = GetTimingLabel(request);

            var time = request.Value.ToString();
            parameters["utt"] = time;
            parameters["plt"] = time;

            return parameters;
        }

        private static string GetTimingLabel(RequestTiming request)
        {
            var queryPartStartAt = request.DocumentPath.IndexOf('?');

            var label = queryPartStartAt >= 0
                ? request.DocumentPath.Substring(0, queryPartStartAt)
                : request.DocumentPath;

            if (!string.IsNullOrEmpty(request.HttpMethod))
                label = request.HttpMethod + ":" + label;

            return label;
        }

        private Task PostAsync(NameValueCollection parameters, CancellationToken cancellationToken)
        {
            return PostAsync(parameters, _context.CollectUrl, cancellationToken);
        }

        private async Task PostAsync(NameValueCollection parameters, string urlPath, CancellationToken cancellationToken)
        {
            var content = ToHttpContent(parameters);

            var response = await _httpClient.PostAsync(urlPath, content, cancellationToken)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();
        }

        private static StringContent ToHttpContent(NameValueCollection parameters)
        {
            var serializedParams = SerializeParameters(parameters);
            return new StringContent(serializedParams, Encoding.UTF8);
        }

        private static string SerializeParameters(NameValueCollection parameters)
        {
            var builder = new StringBuilder();
            foreach (var key in parameters.AllKeys)
            {
                if (builder.Length > 0)
                    builder.Append('&');

                var value = parameters[key];

                builder
                    .Append(Encode(key))
                    .Append('=')
                    .Append(Encode(value));
            }
            return builder.ToString();
        }

        private static string Encode(string data)
        {
            if (string.IsNullOrEmpty(data))
                return string.Empty;

            // Escape spaces as '+'.
            return Uri.EscapeDataString(data).Replace("%20", "+");
        }
    }
}
