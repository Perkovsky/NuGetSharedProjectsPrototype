using System;
using System.IO;
using System.Security.Cryptography;

namespace GSMP.Utilities.Encryption
{
	public class Encryptor : IEncryptor
	{
		private readonly string _key;
		private readonly string _initVector;

		public Encryptor(string key, string initVector)
		{
			_key        = key;
			_initVector = initVector;
		}
		public string Encrypt(string str)
		{
			using (var cipher = CreateCipher())
			{
				using (var encryptor = cipher.CreateEncryptor(cipher.Key, cipher.IV))
				{
					using (var msEncrypt = new MemoryStream())
					{
						using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
						{
							using (var swEncrypt = new StreamWriter(csEncrypt))
								swEncrypt.Write(str);

							var encryptedBytes = msEncrypt.ToArray();
							var encrypted      = Convert.ToBase64String(encryptedBytes);
							return encrypted;
						}
					}
				}
			}
		}

		public string Decrypt(string str)
		{
			using (var cipher = CreateCipher())
			{
				using (var encryptor = cipher.CreateDecryptor(cipher.Key, cipher.IV))
				{
					var bytes = Convert.FromBase64String(str);
					using (var msEncrypt = new MemoryStream(bytes))
					{
						using (var csDecrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Read))
						{
							using (var srDecrypt = new StreamReader(csDecrypt))
								return srDecrypt.ReadToEnd();
						}
					}
				}
			}
		}

		private Rijndael CreateCipher()
		{
			var cipher = Rijndael.Create();

			cipher.KeySize = 256;
			cipher.Mode    = CipherMode.CBC;
			cipher.Padding = PaddingMode.PKCS7;

			cipher.Key = Convert.FromBase64String(_key);
			cipher.IV  = Convert.FromBase64String(_initVector);

			return cipher;
		}
	}
}