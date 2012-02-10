namespace Adf.Core.Encryption
{
    public class EncryptionType : Descriptor
    {
        public static readonly EncryptionType MD5 = new EncryptionType("MD5");
        public static readonly EncryptionType SHA512 = new EncryptionType("SHA512");

        public EncryptionType(string name) : base(name)
        {
        }
    }
}
