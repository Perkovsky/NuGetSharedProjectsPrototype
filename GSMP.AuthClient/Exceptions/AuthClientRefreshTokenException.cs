using System;

namespace GSMP.AuthClient.Exceptions
{
	public class AuthClientRefreshTokenException : Exception
	{
		public AuthClientRefreshTokenException(string message)
			: base(message)
		{
		}
	}
}
