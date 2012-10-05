using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Adf.Core.Resources;

namespace Adf.Core.Extensions
{
    ///<summary>
    /// Extensions for working with enums
    ///</summary>
    public static class EnumExtensions
    {
        ///<summary>
        /// Gets the description for a single enum value.
        ///</summary>
        ///<param name="value">Single enum value</param>
        ///<returns>If the value if marked with a DescriptionAttribute, this method returns the value for this DescriptionAttribute, otherwise return the value itself.</returns>
        public static string GetDescription(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attribute = (DescriptionAttribute) fi.GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();

            return ResourceManager.GetString(key: (attribute == null) ? value.ToString() : attribute.Description);
        }

        /// <summary>
        /// Returns a value indicating whether the value of the specified field will be exclded from a list.
        /// </summary>
        /// <param name="value">The enum value to check.</param>
        /// <returns>
        /// true if the value of the specified enum needs to be excluded from a list; otherwise, false.
        /// </returns>
        /// <exception cref="System.NullReferenceException">
        /// Object reference not set to an instance of an object.
        /// </exception>
        public static bool IsExcluded(this Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            return !(fi == null) && (fi.GetCustomAttributes(typeof(ExcludeAttribute), false).Length > 0) ;
        }

        public static bool IsIn(this Enum value, params Enum[] values)
        {
            return values.Contains(value);
        }

        public static bool IsEqual(this Enum item, uint? value)
        {
            return item != null && Convert.ToInt32(item) == value;
        }

        public static bool IsEqual(this Enum item, int value)
        {
            return item != null && Convert.ToInt32(item) == value;
        }

        public static object[] GetValues(this Enum value)
        {
            Type enumType = value.GetType();

            return (from field in enumType.GetFields()
                    where field.IsLiteral
                    select field.GetValue(enumType)).ToArray();
        }
    }
}
