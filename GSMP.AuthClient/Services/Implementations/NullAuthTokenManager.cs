using System;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using GSMP.AuthClient.Models.RequestModels;

namespace GSMP.AuthClient.Services.Implementations
{
	public class NullAuthTokenManager : IAuthTokenManager
	{
		public string Login(LoginRequestModel model, Action<TokenResponse> saveToken)
		{
			return null;
		}

		public Task<string> LoginAsync(LoginRequestModel model, Action<TokenResponse> saveToken, CancellationToken ct)
		{
			return Task.FromResult<string>(null);
		}

		public void Logout(string token)
		{
		}

		public Task LogoutAsync(string token, CancellationToken ct)
		{
			return Task.CompletedTask;
		}

		public Task<string> RefreshTokenAsync(RefreshTokenRequestModel model, Action<TokenResponse> saveToken, CancellationToken ct)
		{
			return Task.FromResult<string>(null);
		}
	}
}
