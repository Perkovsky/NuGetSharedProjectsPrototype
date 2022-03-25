using System;

namespace GSMP.AuthClient.Models
{
	public class TokenStorageSlim
	{
		public string AccessToken { get; set; }
		public string RefreshToken { get; set; }
		public DateTime ExpiresIn { get; set; }
	}
}
