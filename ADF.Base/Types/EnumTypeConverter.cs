using System;
using Adf.Core.Extensions;
using Adf.Core.Types;

namespace Adf.Base.Types
{
    class EnumTypeConverter : ITypeConverter
    {
        /// <summary>
        /// Decides whether a specific converter can convert this type.
        /// </summary>
        /// <param name="type">Type to convert</param>
        /// <returns>True if converter can convert, false if it can't.</returns>
        public bool CanConvert(Type type)
        {
            return type.IsEnum();
        }

        /// <summary>
        /// Convert a type, usually primitive, to a rich type.
        /// </summary>
        /// <typeparam name="T">Type to convert to.</typeparam>
        /// <param name="value">Object to convert into type.</param>
        /// <returns>Object converted to rich type.</returns>
        public T To<T>(object value)
        {
            // TODO: check 
            return (T)Enum.Parse(typeof(T), value.ToString());
        }

        public object To(Type type, object value)
        {
            return Enum.Parse(type, value.ToString());
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
