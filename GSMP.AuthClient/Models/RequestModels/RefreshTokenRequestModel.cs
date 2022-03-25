namespace GSMP.AuthClient.Models.RequestModels
{
	public class RefreshTokenRequestModel
	{
		public string RefreshToken { get; set; }
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
	}
}
