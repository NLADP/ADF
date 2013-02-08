using Adf.Core.Validation;
using Adf.WinRT.UI;
using Adf.WinRT.UI.Styling;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Adf.WinRT.Validation
{
    public class PanelValidationHandler : IValidationHandler
    {
        public static readonly DependencyProperty OriginalStyleProperty =
            DependencyProperty.RegisterAttached("OriginalStyle", typeof(Style), typeof(FrameworkElement), new PropertyMetadata(null, null));

        public static void SetOriginalStyle(FrameworkElement element, PropertyInfo propertyInfo)
        {
            element.SetValue(OriginalStyleProperty, propertyInfo);
        }

        public static PropertyInfo GetOriginalStyle(FrameworkElement element)
        {
            return element.GetValue(OriginalStyleProperty) as PropertyInfo;
        }

        public void Handle(ValidationResultCollection validationResults, params object[] p)
        {
            if(validationResults.Count == 0) return;

            var frame = Window.Current.Content as Frame;

            if (frame == null) return;

            var page = frame.Content as Page;

            if (page == null) return;

            var controls = page.FindControlsByType<Control>();

            foreach (var control in controls)
            {
                var prop = control.GetValue(FrameworkElementDependencyProperties.BindedMemberInfoProperty) as PropertyInfo;

                if (prop == null) continue;

                if (validationResults.Any(vr => vr.AffectedProperty != null && vr.AffectedProperty.Name == prop.Name && vr.AffectedProperty.DeclaringType == prop.DeclaringType))
                {
                    if(!control.Style.IsEqual("PanelItemError")) control.SetValue(OriginalStyleProperty, control.Style);
                    control.Style("PanelItemError");
                }
                else if(control.Style.IsEqual("PanelItemError") && control.GetValue(OriginalStyleProperty) != null)
                {
                    control.Style = control.GetValue(OriginalStyleProperty) as Style;
                }
            }
        }
    }
}
