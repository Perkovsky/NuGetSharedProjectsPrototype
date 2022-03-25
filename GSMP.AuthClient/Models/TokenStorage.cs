using System;
using IdentityModel.Client;

namespace GSMP.AuthClient.Models
{
	internal class TokenStorage
	{
		public TokenResponse TokenResponse { get; set; }
		public DateTime ExpiresIn { get; set; }
	}
}
