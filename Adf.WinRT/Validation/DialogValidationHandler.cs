using System.Linq;
using Adf.Core.Validation;
using Windows.UI.Popups;

namespace Adf.WinRT.Validation
{
    public class DialogValidationHandler : IValidationHandler
    {
        public void Handle(ValidationResultCollection validationResults, params object[] p)
        {
            string text = ValidationManager.ValidationResults.Aggregate(string.Empty, (current, result) => current + (result.ToString() + "\n"));

            var dialog = new MessageDialog(text, "Validation");

            dialog.ShowAsync();
        }
    }
}
