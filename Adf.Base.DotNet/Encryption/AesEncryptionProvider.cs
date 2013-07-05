using System;
using System.IO;
using System.Security.Cryptography;
using Adf.Core.Encryption;

namespace Adf.Base.Encryption
{
    public class AesEncryptionProvider : IEncryptionProvider
    {
        public string Encrypt(string value, params object[] p)
        {
            var key = Convert.FromBase64String((string) p[0]);
            var iv = Convert.FromBase64String((string) p[1]);

            byte[] encrypted;
            // Create an AesManaged object with the specified key and IV.
            using (var aesAlg = new AesManaged())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (var msEncrypt = new MemoryStream())
                {
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(value);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        public string Decrypt(string value, params object[] p)
        {
            var key = Convert.FromBase64String((string)p[0]);
            var iv = Convert.FromBase64String((string)p[1]);

            string plaintext;

            // Create an AesManaged object
            // with the specified key and IV.
            using (var aesAlg = new AesManaged())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (var msDecrypt = new MemoryStream(Convert.FromBase64String(value)))
                {
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (var srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }
            return plaintext;
        }
    }
}
