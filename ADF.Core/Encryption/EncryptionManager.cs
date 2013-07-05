using System;
using System.Collections.Generic;
using Adf.Core.Domain;
using Adf.Core.Objects;

namespace Adf.Core.Encryption
{
    public static class EncryptionManager
    {
        private static readonly Dictionary<EncryptionType, IEncryptionProvider> Providers = new Dictionary<EncryptionType, IEncryptionProvider>();

        private static IEncryptionProvider GetProvider(EncryptionType type)
        {
            if (!Providers.ContainsKey(type)) Providers[type] = ObjectFactory.BuildUp<IEncryptionProvider>(type.ToString());

            return Providers[type];
        }

        public static string Encrypt(EncryptionType type, string value, params object[] p)
        {
            return GetProvider(type).Encrypt(value, p);
        }

        public static string Decrypt(EncryptionType type, string value, params object[] p)
        {
            return GetProvider(type).Decrypt(value, p);
        }

//        public static string Encrypt(this string value, EncryptionType type)
//        {
//            return Encrypt(type, value);
//        }
//
//        public static string Encrypt(this object value, EncryptionType type)
//        {
//            return (value == null) ? string.Empty.Encrypt(type) : value.ToString().Encrypt(type);
//        }
//
//        public static string Encrypt(this IValueObject value, EncryptionType type)
//        {
//            return value.Value.Encrypt(type);
//        }
//
//        public static string Encrypt(this Enum value, EncryptionType type)
//        {
//            return value.ToString().Encrypt(type);
//        }
//
//        public static string Encrypt(this Descriptor value, EncryptionType type)
//        {
//            return value.Name.Encrypt(type);
//        }
    }
}
