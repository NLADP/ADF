using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Adf.Core.Domain
{
    /// <summary>
    /// Defines generalized property parsing operations.    
    /// </summary>
    public interface IPropertyParser
    {
        /// <summary>
        /// Sets the new value for a specific property on an object.
        /// </summary>
        /// <param name="instance">Target object to set property at.</param>
        /// <param name="pi">Property info for the property to set.</param>
        /// <param name="newvalue">Value for the property.</param>
        /// <param name="culture"></param>
        void SetValue(object instance, PropertyInfo pi, object newvalue, CultureInfo culture = null);

        /// <summary>
        /// Returns the ValueItems of the supplied target object.
        /// An indicator is also supplied to indicate whether empty will be included or not.
        /// </summary>
        /// <param name="target">The object.</param>
        /// <param name="includeEmpty">The indicator to indicate whether empty will be included or not.</param>
        /// <param name="items"></param>
        /// <returns>The list of ValueItems.</returns>
        ICollection GetCollection(object target, bool includeEmpty, IEnumerable items = null);

//        ICollection GetCollection(Type targetType, bool includeEmpty, IEnumerable items = null);

        ICollection<ValueItem> GetValueItems(object target, ICollection items);

        /// <summary>
        /// Indicates whether the supplied object is empty or not.
        /// </summary>
        /// <param name="value">The supplied object.</param>
        /// <returns>True if the supplied object is empty, false otherwise.</returns>
        bool IsEmpty(object value);

        /// <summary>
        /// Indicates whether the two supplied objects are equal or not.
        /// </summary>
        /// <param name="compare">The first supplied object.</param>
        /// <param name="to">The second supplied object.</param>
        /// <returns>True if the two supplied objects are equal, false otherwise.</returns>
        bool IsEqual(object compare, object to);

        /// <summary>
        /// Indicates whether the supplied <see cref="Type"/> is parsable or not.
        /// </summary>
        /// <param name="type">The supplied <see cref="Type"/>.</param>
        /// <returns>True if the supplied <see cref="Type"/> is parsable, false otherwise.</returns>
        bool IsParsable(Type type);
    }
}
