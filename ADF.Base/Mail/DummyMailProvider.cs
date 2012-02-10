using System;
using System.Net.Mail;
using Adf.Core.Mail;
using Adf.Core.State;
using Adf.Core.Logging;
using Adf.Core.Validation;

namespace Adf.Base.Mail
{
    class DummyMailProvider : IMailProvider
    {
        /// <summary>
        /// Send a message to the 'MailDumpLocation', which indicates the PickupDirectory
        /// </summary>
        /// <param name="message"></param>
        public void Send(MailMessage message)
        {
            try
            {
                // This implementation dumps the message to the filesystem
                var client = new SmtpClient
                {
                    DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                    PickupDirectoryLocation = StateManager.Settings["MailDumpLocation"].ToString()
                };

                client.Send(message);
            }
            catch (Exception ex)
            {
                LogManager.Log(ex);
                ValidationManager.AddError("Mail.Failed");
            }
        }
    }
}
