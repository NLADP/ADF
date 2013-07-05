using System.Reflection;
using Windows.UI.Xaml;

namespace Adf.WinRT.UI
{
    public static class FrameworkElementDependencyProperties
    {
        public static readonly DependencyProperty BindedMemberInfoProperty =
            DependencyProperty.RegisterAttached("BindedMemberInfo", typeof(PropertyInfo), typeof(FrameworkElement), new PropertyMetadata(null, null));

        public static void SetBindedMemberInfo(FrameworkElement element, PropertyInfo propertyInfo)
        {
            element.SetValue(BindedMemberInfoProperty, propertyInfo);
        }

        public static PropertyInfo GetBindedMemberInfo(FrameworkElement element)
        {
            return element.GetValue(BindedMemberInfoProperty) as PropertyInfo;
        }
    }
}
