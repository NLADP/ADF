namespace Adf.Core.Encryption
{
    public interface IEncryptionProvider
    {
        string Encrypt(string value, params object[] p);

        string Decrypt(string value, params object[] p);
    }
}
