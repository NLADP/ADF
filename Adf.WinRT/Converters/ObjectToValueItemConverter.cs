using System.Reflection;
using Adf.Base.Domain;
using Adf.Core.Domain;
using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Adf.Core;
using Adf.Core.Resources;
using Adf.Core.Identity;

namespace Adf.WinRT.Converters
{
    public class ObjectToValueItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null) return DependencyProperty.UnsetValue;

            return ValueItem.New(ResourceManager.GetString(value.ToString()), value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var valueItem = value as ValueItem;

            if (valueItem == null) return DependencyProperty.UnsetValue;

            return valueItem.Value;
        }
    }
}
