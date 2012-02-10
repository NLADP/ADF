using System;
using System.Net.Mail;
using Adf.Core.Mail;
using Adf.Core.State;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Adf.Core.Validation;
using Adf.Core.Logging;

namespace Adf.Base.Mail
{
    public class SmtpMailProvider : IMailProvider
    {
        public void Send(MailMessage message)
        {
            try
            {
                new SmtpClient(StateManager.Settings["SmtpHost"].ToString()).Send(message);
            }            
            catch (Exception ex)
            {
                LogManager.Log(ex);
                ValidationManager.AddError("Mail.Failed");
            }
        }
    }
}
