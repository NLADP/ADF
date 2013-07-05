namespace Adf.Core.Encryption
{
    public class EncryptionType : Descriptor
    {
        public static readonly EncryptionType MD5 = new EncryptionType("MD5");
        public static readonly EncryptionType SHA512 = new EncryptionType("SHA512");
        public static readonly EncryptionType Aes = new EncryptionType("Aes");
        public static readonly EncryptionType Passwords = new EncryptionType("Passwords");
        public static readonly EncryptionType Gravatar = new EncryptionType("Gravatar");

        public EncryptionType(string name) : base(name)
        {
        }
    }
}
