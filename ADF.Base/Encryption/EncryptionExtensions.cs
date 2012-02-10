using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Adf.Core;
using Adf.Core.Domain;
using Adf.Core.Encryption;

namespace Adf.Base.Encryption
{
    public static class EncryptionExtensions
    {
        public static string Encrypt(this string value, EncryptionType type)
        {
            return EncryptionManager.Encrypt(type, value);
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
