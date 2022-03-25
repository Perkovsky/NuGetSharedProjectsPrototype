using System.Threading;
using System.Threading.Tasks;
using GSMP.AuthClient.Models.RequestModels;

namespace GSMP.AuthClient.Services.Implementations
{
	public class NullAuthUserManager : IAuthUserManager
	{
		public Task<string> CreateAsync(UserRequestModel model, CancellationToken ct)
		{
			return Task.FromResult<string>(null);
		}

		public Task DeleteAsync(string id, CancellationToken ct)
		{
			return Task.CompletedTask;
		}

		public Task<string> UpdateAsync(string id, UserRequestModel model, CancellationToken ct)
		{
			return Task.FromResult<string>(null);
		}

		public Task<string> UpdateWithoutPasswordAsync(string id, UserRequestModel model, CancellationToken ct)
		{
			return Task.FromResult<string>(null);
		}
	}
}
