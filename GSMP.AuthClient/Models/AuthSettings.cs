namespace GSMP.AuthClient.Models
{
	public class AuthSettings
	{
		public string Issuer { get; set; }
		public string ClientId { get; set; }
		public string ClientSecret { get; set; }
		public string AuthUser { get; set; }
		public string AuthUserPassword { get; set; }
	}
}
