using Adf.Business.ValueObject;
using Adf.Core.Encryption;

namespace Adf.Business.Extentions
{
    public static class GravatarEmailExtentions
    {
        private const string GravatarBase = "http://www.gravatar.com/avatar";
        private const string GravatarUrlFormat = "{0}/{1}";
        private const string GravatarSizeUrlFormat = "{0}/{1}?s={2}";
        private const string NotFound = "~/Images/noavatar.png";

        public static string GetGravatar(this Email email)
        {
            if (email.IsEmpty) return NotFound;

            string input = email.ToString().ToLower().Trim();
            string hash = EncryptionManager.Encrypt(EncryptionType.MD5, input);

            return string.Format(GravatarUrlFormat, GravatarBase, hash);
        }
        
        public static string GetGravatar(this Email email, int size = 40)
        {
            if (email.IsEmpty) return NotFound;

            string input = email.ToString().ToLower().Trim();
            string hash = EncryptionManager.Encrypt(EncryptionType.MD5, input);

            return string.Format(GravatarSizeUrlFormat, GravatarBase, hash, size);
        }

    }
}
