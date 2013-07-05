using System.Net.Mail;
using Adf.Core.Objects;

namespace Adf.Core.Mail
{
    /// <summary>
    /// Represents manager for logging a message or <see cref="System.Exception"/>.
    /// Provides methods for logging message or <see cref="System.Exception"/>s.
    /// </summary>
    public static class MailManager
    {
        private static IMailProvider _provider;

        private static IMailProvider Provider
        {
            get { return _provider ?? (_provider = ObjectFactory.BuildUp<IMailProvider>()); }
        }

        /// <summary>
        /// Send an email using the plugged-in mail provider.
        /// </summary>
        /// <param name="message"></param>
        public static void Send(MailMessage message)
        {
            Provider.Send(message);
        }
    }
}