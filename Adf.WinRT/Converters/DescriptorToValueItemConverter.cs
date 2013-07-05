using System;
using System.Reflection;
using Adf.Core;
using Adf.Core.Domain;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Adf.WinRT.Converters
{
    public class DescriptorToValueItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || !typeof(Descriptor).GetTypeInfo().IsAssignableFrom(value.GetType().GetTypeInfo()))
                return DependencyProperty.UnsetValue;

            return ValueItem.New(((Descriptor)value).Name, ((Descriptor)value).Name);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var valueItem = value as ValueItem;
            var type = parameter as Type;

            if (valueItem == null || type == null) return DependencyProperty.UnsetValue;

            return Descriptor.Get(targetType, valueItem.Value.ToString());
        }
    }
}
