namespace GSMP.AuthClient.Models.RequestModels
{
	public class LoginRequestModel
	{
		public string UserName { get; set; }
		public string Password { get; set; }
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
	}
}
