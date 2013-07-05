using System.Security.Cryptography;
using System.Text;
using Adf.Core.Encryption;

namespace Adf.Base.Encryption
{
    public class SHA512EncryptionProvider : IEncryptionProvider
    {
            public string Encrypt(string value, params object[] p)
            {
                SHA512 sha512 = new SHA512Managed();

                byte[] sha512Bytes = Encoding.Default.GetBytes(value);

                byte[] cryString = sha512.ComputeHash(sha512Bytes);

                string sha512Str = string.Empty;

                for (int i = 0; i < cryString.Length; i++)
                {

                    sha512Str += cryString[i].ToString("X");

                }

                return sha512Str;
            }

        public string Decrypt(string value, params object[] p)
        {
            throw new System.NotImplementedException();
        }
    }
}
