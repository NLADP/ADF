using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Adf.Core.Resources;
using Adf.Core.Validation;
using Windows.UI.Notifications;

namespace Adf.WinRT.Validation
{
    public class ToastValidationHandler : IValidationHandler
    {
        public void Handle(ValidationResultCollection validationResults, params object[] p)
        {
            foreach (var validationResult in validationResults.Where(r => r.AffectedProperty == null))
            {
                Toast(validationResult.ToString());
            }
        }

        private void Toast(string text)
        {
            var template = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

            var elements = template.GetElementsByTagName("text");
//            elements[0].AppendChild(template.CreateTextNode(property ?? string.Empty));
            elements[1].AppendChild(template.CreateTextNode(text));

            var toast = new ToastNotification(template);

            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
