using System;
using System.Collections.Generic;
using System.Linq;
using Adf.Core.Objects;

namespace Adf.Core.Types
{
    public static class Converter
    {
        private static IEnumerable<ITypeConverter> _converters;
        private static readonly object _lock = new object();

        private static IEnumerable<ITypeConverter> Converters
        {
            get { lock (_lock) return _converters ?? (_converters = ObjectFactory.BuildAll<ITypeConverter>().ToList()); }
        }

        public static T To<T>(object value)
        {
            Type type = typeof(T);

            if (value != null && type.IsInstanceOfType(value)) return (T) value;

            var converter = Converters.FirstOrDefault(c => c.CanConvert(type));

            return (converter == null) ? (T) value : converter.To<T>(value);
        }

        public static object ToPrimitive<T>(T value)
        {
            Type type = typeof(T);

            var converter = Converters.FirstOrDefault(c => c.CanConvert(type));

            return (converter == null) ? value : converter.ToPrimitive(value);
        }

        public static object To(Type type, object value)
        {
            if (value == null || type == value.GetType()) return value;

            return Converters.First(c => c.CanConvert(type)).To(type, value);
        }
    }
}
