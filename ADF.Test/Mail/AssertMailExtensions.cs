using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adf.Core.Tasks;
using Adf.Core.Test;

namespace Adf.Test.Mail
{
    public static class AssertMailExtensions
    {
        public static T MailIsSent<T>(this T task) where T : ITask
        {
            TestManager.IsPresent(TestItemType.Mail, TestAction.MailSent);

            return task;
        }
        
        public static T MailIsNotSent<T>(this T task) where T : ITask
        {
            TestManager.IsNotPresent(TestItemType.Mail, TestAction.MailSent);

            return task;
        }
    }
}
