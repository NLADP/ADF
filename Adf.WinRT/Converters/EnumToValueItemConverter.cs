using System;
using Adf.Core.Extensions;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Adf.Core.Domain;
using System.Reflection;

namespace Adf.WinRT.Converters
{
    public class EnumToValueItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if(value == null || !value.GetType().GetTypeInfo().IsEnum) return DependencyProperty.UnsetValue;

            var enumValue = (Enum)value;

            return ValueItem.New(enumValue.GetDescription(), enumValue.ToString());
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var item = value as ValueItem;
            var type = parameter as Type;

            if(type == null) return DependencyProperty.UnsetValue;

            var enumValue = (item == null || item.Value == null || item.Value.ToString().IsNullOrEmpty()) ? "Empty" : item.Value.ToString();

            return Enum.Parse(type, enumValue);
        }
    }
}
