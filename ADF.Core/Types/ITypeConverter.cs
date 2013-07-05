using System;

namespace Adf.Core.Types
{
    public interface ITypeConverter
    {
        /// <summary>
        /// Decides whether a specific converter can convert this type.
        /// </summary>
        /// <param name="type">Type to convert</param>
        /// <returns>True if converter can convert, false if it can't.</returns>
        bool CanConvert(Type type);

        /// <summary>
        /// Convert a type, usually primitive, to a rich type.
        /// </summary>
        /// <typeparam name="T">Type to convert to.</typeparam>
        /// <param name="value">Object to convert into type.</param>
        /// <returns>Object converted to rich type.</returns>
        T To<T>(object value);

        object To(Type type, object value);

        /// <summary>
        /// Extracts the primitive value of a rich type.
        /// </summary>
        /// <typeparam name="T">Type to extract value from.</typeparam>
        /// <param name="value"></param>
        /// <returns>Primitive type which is extract from rich type.</returns>
        object ToPrimitive<T>(T value);
    }
}
