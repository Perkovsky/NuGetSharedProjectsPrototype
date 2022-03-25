using System;
using System.Security.Cryptography;
using System.Text;

namespace GSMP.Utilities.Encryption
{
	public static class HashTools
	{
		public static string HashAsHmacSha256(string message, string secret)
		{
			secret ??= "";

			var encoding     = new ASCIIEncoding();
			var keyByte      = encoding.GetBytes(secret);
			var messageBytes = encoding.GetBytes(message);

			using (var hmacSha256 = new HMACSHA256(keyByte))
			{
				byte[] hashMessage = hmacSha256.ComputeHash(messageBytes);
				return Convert.ToBase64String(hashMessage);
			}
		}
	}
}
