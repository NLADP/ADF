using System.Security.Cryptography;
using System.Text;
using Adf.Core.Encryption;

namespace Adf.Base.Encryption
{
    public class MD5EncryptionProvider : IEncryptionProvider
    {
            public string Encrypt(string value)
            {
                MD5 md5Hasher = MD5.Create();
                byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(value));
                var sBuilder = new StringBuilder();
               
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                return sBuilder.ToString();
            }
    }
}
