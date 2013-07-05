namespace Adf.Core.Authorization
{
    public class LoginResult : Descriptor
    {
        public LoginResult(string name) : base(name) {}

        public static readonly LoginResult Success = new LoginResult("Success");
        public static readonly LoginResult Failed = new LoginResult("Failed");
        public static readonly LoginResult Expired = new LoginResult("Expired");
        public static readonly LoginResult Inconclusive = new LoginResult("Inconclusive");
    }
}
