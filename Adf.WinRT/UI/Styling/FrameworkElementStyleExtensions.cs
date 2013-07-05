using System;
using Windows.UI.Xaml;
using Adf.Core.State;
using Windows.UI.Xaml.Controls;

namespace Adf.WinRT.UI.Styling
{
    public static class FrameworkElementStyleExtensions
    {
        public static FrameworkElement Style(this FrameworkElement element, string stylename)
        {
            if (stylename == null) return element;

            var style = StateManager.Settings[stylename] as Style;

            if (style != null) element.Style = style;

            return element;
        }

        public static DataTemplate GetTemplate(string templatename)
        {
            if (templatename == null) throw new ArgumentException();

            return StateManager.Settings[templatename] as DataTemplate;
        }

        public static bool IsEqual(this Style style, string otherStyleName)
        {
            if (style == null) return false;
            if (string.IsNullOrEmpty(otherStyleName)) return false;

            var otherStyle = StateManager.Settings[otherStyleName] as Style;

            if (otherStyle == null) return false;

            return style == otherStyle;
        }
    }
}
