using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Adf.Core.Domain;

namespace Adf.Base.Domain
{
    /// <summary>
    /// Represents default property parsing operation.    
    /// </summary>
    class DefaultPropertyParser : IPropertyParser
    {
        #region IPropertyParser Members

        /// <summary>
        /// Sets the new value for a specific property on an object.
        /// </summary>
        /// <param name="instance">Target object to set the property. In most cases this will be a domain object.</param>
        /// <param name="pi">Property info for the property to set.</param>
        /// <param name="newvalue">Value for the property.</param>
        /// <param name="culture"></param>
        /// <exception cref="System.InvalidCastException">Unable to convert the new value to the specified property. </exception>
        /// <exception cref="System.ArgumentNullException">null reference is not accept as a valid argument.</exception>           
        public void SetValue(object instance, PropertyInfo pi, object newvalue, CultureInfo culture = null)
        {
            // else try to cast the new value to the correct type
            object o = Convert.ChangeType(newvalue, pi.PropertyType, culture ?? CultureInfo.CurrentCulture);

            pi.SetValue(instance, o, null);
        }

        /// <summary>
        /// Creates a new instance of ValueCollection class to get the collection of ValueItem.
        /// </summary>
        /// <param name="target">Not used.</param>
        /// <param name="includeEmpty">Not used.</param>
        /// <param name="items"></param>
        /// <returns>Returns a new instance of ValueCollection with no ValueItems.</returns>
        public IEnumerable<ValueItem> GetCollection(object target, bool includeEmpty, IEnumerable items = null)
        {
            var list = new List<ValueItem>();

            if (items != null)
            {
                list.AddRange(from object item in items select ValueItem.New(item.ToString(), item, target.Equals(item)));
            }

            return list;
        }

        /// <summary>
        /// Checks whether the specified Object is empty or not.
        /// </summary>
        /// <param name="value">The Object to be checked for null or empty.</param>
        /// <returns>True if the specified Object is empty, false otherwise.</returns>
        public bool IsEmpty(object value)
        {
            return (value == null) || string.IsNullOrEmpty(value.ToString());
        }

        /// <summary>
        /// Checks whether the two specified Objects are equal or not.
        /// </summary>
        /// <param name="compare">The Object to compare.</param>
        /// <param name="to">The Object to compare with.</param>
        /// <returns>True if both the specified Objects are equal, false otherwise.</returns>
        public bool IsEqual(object compare, object to)
        {
            return compare.Equals(to);
        }

        /// <summary>
        /// Indicates whether the specified <see cref="Type"/> is parsable or not.
        /// </summary>
        /// <param name="type">The specified <see cref="Type"/>.</param>
        /// <returns>True if the specified <see cref="Type"/> is parsable, false otherwise.</returns>
        public bool IsParsable(Type type)
        {
            return true;
        }

        #endregion
    }
}
