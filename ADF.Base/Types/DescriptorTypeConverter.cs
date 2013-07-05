using System;
using Adf.Core;
using Adf.Core.Types;

namespace Adf.Base.Types
{
    public class DescriptorTypeConverter : ITypeConverter
    {
        /// <summary>
        /// Decides whether a specific converter can convert this type.
        /// </summary>
        /// <param name="type">Type to convert</param>
        /// <returns>True if converter can convert, false if it can't.</returns>
        public bool CanConvert(Type type)
        {
            return typeof (Descriptor).IsAssignableFrom(type);
        }

        /// <summary>
        /// Convert a type, usually primitive, to a rich type.
        /// </summary>
        /// <typeparam name="T">Type to convert to.</typeparam>
        /// <param name="value">Object to convert into type.</param>
        /// <returns>Object converted to rich type.</returns>
        public T To<T>(object value)
        {
            if (value == null) return default(T);

            var result = To(typeof (T), value);

            return result != null ? (T) result : default(T);
        }

        public object To(Type type, object value)
        {
            if (value == null) return null;

            return Descriptor.Get(type, value.ToString());
        }

        /// <summary>
        /// Extracts the primitive value of a rich type.
        /// </summary>
        /// <typeparam name="T">Type to extract value from.</typeparam>
        /// <param name="value"></param>
        /// <returns>Primitive type which is extract from rich type.</returns>
        public object ToPrimitive<T>(T value)
        {
            return value.ToString();
        }
    }
}
