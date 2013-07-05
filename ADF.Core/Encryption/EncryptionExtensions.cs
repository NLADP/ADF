using System;
using Adf.Core.Domain;

namespace Adf.Core.Encryption
{
    public static class EncryptionExtensions
    {
        public static string Encrypt(this string value, EncryptionType type, params object[] p)
        {
            return EncryptionManager.Encrypt(type, value, p);
        }

        public static string Decrypt(this string value, EncryptionType type, params object[] p)
        {
            return EncryptionManager.Decrypt(type, value, p);
        }

        public static string Encrypt(this object value, EncryptionType type)
        {
            return EncryptionManager.Encrypt(type, value.ToString());
        }

        public static string Encrypt(this IValueObject value, EncryptionType type)
        {
            return EncryptionManager.Encrypt(type, value.Value.ToString());
        }

        public static string Encrypt(this Enum value, EncryptionType type)
        {
            return EncryptionManager.Encrypt(type, value.ToString());
        }

        public static string Encrypt(this Descriptor value, EncryptionType type)
        {
            return EncryptionManager.Encrypt(type, value.Name);
        }
    }
}
