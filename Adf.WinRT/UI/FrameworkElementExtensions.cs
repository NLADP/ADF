using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Adf.WinRT.UI.Styling;

namespace Adf.WinRT.UI
{
    public static class FrameworkElementExtensions
    {
        public static IEnumerable<T> FindControlsByType<T>(this DependencyObject control) where T : FrameworkElement
        {
            if(control == null) return new List<T>();

            var listOfChildren = new List<T>();
            var numberOfChildren = VisualTreeHelper.GetChildrenCount(control);

            for (var childIndex = 0; childIndex < numberOfChildren; childIndex++)
            {
                var child = VisualTreeHelper.GetChild(control, childIndex);
                var childControl = child as T;

                if (childControl != null)
                    listOfChildren.Add(childControl);

                listOfChildren.AddRange(FindControlsByType<T>(child));
            }

            return listOfChildren;
        }

        public static ToolTip SetErrorToolTip(this FrameworkElement element, string text)
        {
            if (element == null) throw new ArgumentNullException("element");

            var toolTip = new ToolTip
            {
                PlacementTarget = element,
                Content = text
            };

            toolTip.Style("ToolTipError");

            ToolTipService.SetToolTip(element, toolTip);
            
            return toolTip;
        }
    }
}
