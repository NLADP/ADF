using System;
using System.ComponentModel;
using Adf.Core.Extensions;
using Adf.Core.Types;

namespace Adf.Base.Types
{
    class NullableTypeConverter : ITypeConverter
    {
        /// <summary>
        /// Decides whether a specific converter can convert this type.
        /// </summary>
        /// <param name="type">Type to convert</param>
        /// <returns>True if converter can convert, false if it can't.</returns>
        public bool CanConvert(Type type)
        {
            return type.IsNullable();
        }

        /// <summary>
        /// Convert a type, usually primitive, to a rich type.
        /// </summary>
        /// <typeparam name="T">Type to convert to.</typeparam>
        /// <param name="value">Object to convert into type.</param>
        /// <returns>Object converted to rich type.</returns>
        public T To<T>(object value)
        {
            return (T)new NullableConverter(typeof(T)).ConvertFrom(value);
        }

        public object To(Type type, object value)
        {
            return new NullableConverter(type).ConvertFrom(value);
        }

        /// <summary>
        /// Extracts the primitive value of a rich type.
        /// </summary>
        /// <typeparam name="T">Type to extract value from.</typeparam>
        /// <param name="value"></param>
        /// <returns>Primitive type which is extract from rich type.</returns>
        public object ToPrimitive<T>(T value)
        {
            return value;
        }
    }
}
