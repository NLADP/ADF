using System;
using System.Reflection;
using Adf.Base.Domain;
using Adf.Core.Domain;
using Adf.Core.Identity;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Adf.WinRT.Converters
{
    public class DomainObjectToValueItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null || !typeof(IDomainObject).GetTypeInfo().IsAssignableFrom(value.GetType().GetTypeInfo()))
                return DependencyProperty.UnsetValue;

            return ValueItem.New(value.ToString(), ((IDomainObject)value).Id);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var valueItem = value as ValueItem;
            var type = parameter as Type;

            if (valueItem == null || type == null) return DependencyProperty.UnsetValue;

            var factoryType = type.GetFactoryType();
            var getMethod = factoryType.GetRuntimeMethod("Get", new[] { typeof(ID) });

            return getMethod.Invoke(null, new object[] { IdManager.New(valueItem.Value) });
        }
    }
}
