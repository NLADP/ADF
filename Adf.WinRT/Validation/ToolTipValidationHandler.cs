using Adf.Core.Validation;
using Adf.WinRT.UI;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Adf.WinRT.Validation
{
    public class ToolTipValidationHandler : IValidationHandler
    {
        public void Handle(ValidationResultCollection validationResults, params object[] p)
        {
            if (validationResults.Count == 0) return;

            var frame = Window.Current.Content as Frame;

            if (frame == null) return;

            var page = frame.Content as Page;

            if (page == null) return;

            var controls = page.FindControlsByType<Control>();

            foreach (var control in controls)
            {
                // Remove event handlers in case they are specified
                control.GotFocus -= ControlGotFocus;
                control.LostFocus -= ControlLostFocus;

                var prop = control.GetValue(FrameworkElementDependencyProperties.BindedMemberInfoProperty) as PropertyInfo;
                
                if (prop == null) continue;

                ToolTipService.SetToolTip(control, null);

                if (validationResults.Any(vr => vr.AffectedProperty != null && vr.AffectedProperty.Name == prop.Name && vr.AffectedProperty.DeclaringType == prop.DeclaringType))
                {
                    var validationError = ValidationManager.ValidationResults.Where(vr => vr.AffectedProperty.Name == prop.Name &&
                                                                                    vr.AffectedProperty.DeclaringType == prop.DeclaringType)
                                                           .Aggregate(string.Empty, (current, result) => current + (result.ToString() + "\n"));

                    control.SetErrorToolTip(validationError.TrimEnd());

                    control.GotFocus += ControlGotFocus;
                    control.LostFocus += ControlLostFocus;
                }
            }
        }

        private void ControlGotFocus(object sender, RoutedEventArgs e)
        {
            var control = sender as Control;

            if (control == null) return;

            var toolTip = ToolTipService.GetToolTip(control) as ToolTip;

            if (toolTip != null) toolTip.IsOpen = true;
        }

        private void ControlLostFocus(object sender, RoutedEventArgs e)
        {
            var control = sender as Control;

            if (control == null) return;

            var toolTip = ToolTipService.GetToolTip(control) as ToolTip;

            var combo = control as ComboBox;

            if (toolTip != null && (combo == null || !combo.IsDropDownOpen))
                toolTip.IsOpen = false;
        }
    }
}
