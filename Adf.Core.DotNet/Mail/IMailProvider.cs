using System.Net.Mail;

namespace Adf.Core.Mail
{
    public interface IMailProvider
    {
        void Send(MailMessage message);
    }
}
