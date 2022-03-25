using System;
using System.Threading;
using System.Threading.Tasks;
using IdentityModel.Client;
using GSMP.AuthClient.Models.RequestModels;

namespace GSMP.AuthClient.Services
{
	public interface IAuthTokenManager
	{
		string Login(LoginRequestModel model, Action<TokenResponse> saveToken);
		Task<string> LoginAsync(LoginRequestModel model, Action<TokenResponse> saveToken, CancellationToken ct);

		Task<string> RefreshTokenAsync(RefreshTokenRequestModel model, Action<TokenResponse> saveToken, CancellationToken ct);

		void Logout(string token);
		Task LogoutAsync(string token, CancellationToken ct);
	}
}
