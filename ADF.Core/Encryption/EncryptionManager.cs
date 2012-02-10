using System;
using System.Collections.Generic;
using Adf.Core.Domain;
using Adf.Core.Objects;

namespace Adf.Core.Encryption
{
    public static class EncryptionManager
    {
        private static readonly Dictionary<EncryptionType, IEncryptionProvider> providers = new Dictionary<EncryptionType, IEncryptionProvider>();

        private static IEncryptionProvider GetProvider(EncryptionType type)
        {
            if (!providers.ContainsKey(type)) providers[type] = ObjectFactory.BuildUp<IEncryptionProvider>(type.ToString());

            return providers[type];
        }

        public static string Encrypt(EncryptionType type, string value)
        {
            return GetProvider(type).Encrypt(value);
        }

        public static string Encrypt(EncryptionType type, object value)
        {
            return GetProvider(type).Encrypt(value.ToString());
        }

        public static string Encrypt(EncryptionType type, IValueObject value)
        {
            return GetProvider(type).Encrypt(value.Value.ToString());
        }

        public static string Encrypt(EncryptionType type, Enum value)
        {
            return GetProvider(type).Encrypt(value.ToString());
        }

        public static string Encrypt(EncryptionType type, Descriptor value)
        {
            return GetProvider(type).Encrypt(value.Name);
        }
    }
}
