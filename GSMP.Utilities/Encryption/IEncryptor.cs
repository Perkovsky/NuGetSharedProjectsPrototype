namespace GSMP.Utilities.Encryption
{
	public interface IEncryptor
	{
		string Encrypt(string str);
		string Decrypt(string str);
	}
}
