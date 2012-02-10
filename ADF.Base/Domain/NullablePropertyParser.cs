using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using Adf.Core.Domain;
using Adf.Core.Validation;

namespace Adf.Base.Domain
{
    /// <summary>
    /// Represents Nullable property parsing operations.
    /// </summary>
    class NullablePropertyParser : IPropertyParser
    {
        #region IPropertyParser Members

        /// <summary>
        /// Sets the new value for a specific property on an object.
        /// </summary>
        /// <param name="instance">Target object to set property at.</param>
        /// <param name="pi">Property info for the property to set.</param>
        /// <param name="newvalue">Value to try to parse.</param>
        /// <param name="culture"></param>
        public void SetValue(object instance, PropertyInfo pi, object newvalue, CultureInfo culture = null)
        {
            NullableConverter nullableConverter = new NullableConverter(pi.PropertyType);
            object obj;
            try
            {
                obj = nullableConverter.ConvertFrom(null, culture ?? CultureInfo.CurrentCulture, newvalue);
            }
            catch (Exception)
            {
                ValidationManager.AddError(pi, "Adf.Business.NotInstantiable", newvalue, pi.Name);
                return;
            }

            pi.SetValue(instance, obj, null);
        }

        /// <summary>
        /// Creates a new instance of ValueCollection class to get the collection of ValueItem. 
        /// An indicator is also supplied to indicate whether empty will be included or not.
        /// </summary>
        /// <param name="target">The object.</param>
        /// <param name="includeEmpty">The indicator to indicate whether empty will be included or not.</param>
        /// <param name="items"></param>
        /// <returns>The collection.</returns>
        public IEnumerable<ValueItem> GetCollection(object target, bool includeEmpty, IEnumerable items = null)
        {
            return new List<ValueItem>();
        }

        /// <summary>
        /// Checks whether the specified Object is empty or not.
        /// </summary>
        /// <param name="value">The Object to be checked for null or empty.</param>
        /// <returns>True if the specified object is empty, false otherwise.</returns>
        public bool IsEmpty(object value)
        {
            Type type = value.GetType();

            PropertyInfo pi = type.GetProperty("HasValue");

            return (bool)pi.GetValue(value, null);
        }

        /// <summary>
        /// Checks whether the two specified Objects are equal or not.
        /// </summary>
        /// <param name="compare">The Object to compare.</param>
        /// <param name="to">The Object to compare with.</param>
        /// <returns>True if both the specified Objects are equal, false otherwise.</returns>
        public bool IsEqual(object compare, object to)
        {
            return compare == to;
        }

        /// <summary>
        /// Indicates whether the specified <see cref="Type"/> is parsable or not.
        /// </summary>
        /// <param name="type">The specified <see cref="Type"/>.</param>
        /// <returns>True if the specified <see cref="Type"/> is parsable, false otherwise.</returns>
        public bool IsParsable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

        #endregion
    }
}
