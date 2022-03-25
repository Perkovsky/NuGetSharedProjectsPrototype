using System;

namespace GSMP.AuthClient.Exceptions
{
	public class AuthClientLoginException : Exception
	{
		public AuthClientLoginException(string message)
			: base(message)
		{
		}
	}
}
