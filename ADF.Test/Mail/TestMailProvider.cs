using System;
using System.IO;
using System.Net.Mail;
using Adf.Core.Mail;
using Adf.Core.State;
using Adf.Core.Logging;
using Adf.Core.Test;
using Adf.Core.Validation;

namespace Adf.Test.Mail
{
    public class    TestMailProvider : IMailProvider
    {
        /// <summary>
        /// Send a message to the 'MailDumpLocation', which indicates the PickupDirectory
        /// </summary>
        /// <param name="message"></param>
        public void Send(MailMessage message)
        {
            try
            {
                var dumpLocation = StateManager.Settings["Mail.DumpLocation"].ToString();

                // Fallback in case the dir doesn't exist
                if (!Directory.Exists(dumpLocation))
                    dumpLocation = Path.GetTempPath();

                // This implementation dumps the message to the filesystem
                var client = new SmtpClient
                {
                    DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                    PickupDirectoryLocation = dumpLocation
                };

                client.Send(message);

                TestManager.Register(TestItemType.Mail, TestAction.MailSent);
            }
            catch (Exception ex)
            {
                LogManager.Log(ex);
                ValidationManager.AddError("Mail.Failed");
            }
        }
    }
}
