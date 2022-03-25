using System.Collections.Generic;

namespace GSMP.AuthClient.Models.RequestModels
{
	public class UserRequestModel
	{
		public string Email { get; set; }
		public string Password { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public IEnumerable<string> Roles { get; set; }
	}
}
