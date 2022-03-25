using System.Threading;
using System.Threading.Tasks;
using GSMP.AuthClient.Models.RequestModels;

namespace GSMP.AuthClient.Services
{
	public interface IAuthUserManager
	{
		Task<string> CreateAsync(UserRequestModel model, CancellationToken ct);
		Task<string> UpdateAsync(string id, UserRequestModel model, CancellationToken ct);
		Task<string> UpdateWithoutPasswordAsync(string id, UserRequestModel model, CancellationToken ct);
		Task DeleteAsync(string id, CancellationToken ct);
	}
}
