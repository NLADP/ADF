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
    /// Represents Guid property parsing operation.
    /// </summary>
    public class GuidPropertyParser : IPropertyParser
    {
        #region IPropertyParser Members

        /// <summary>
        /// Sets the new value for a specific property on an object.
        /// </summary>
        /// <param name="target">Target object to set the property.</param>
        /// <param name="pi">Property info for the property to set.</param>
        /// <param name="newvalue">Value for the property.</param>
        /// <param name="culture"></param>
        /// <exception cref="System.InvalidCastException">Unable to convert the new value to the specified property. </exception>
        /// <exception cref="System.ArgumentNullException">null reference is not accept as a valid argument.</exception>           
        public void SetValue(object target, PropertyInfo pi, object newvalue, CultureInfo culture = null)
        {
            Guid key = new Guid(newvalue.ToString());

            pi.SetValue(target, key, null);
        }

        /// <summary>
        /// Creates a new instance of ValueCollection class to get the collection of ValueItem.
        /// </summary>
        /// <param name="target">Not used</param>
        /// <param name="includeEmpty">Not used</param>
        /// <param name="items"></param>
        /// <returns>Returns a new instance of ValueCollection with no ValueItems.</returns>
        public ICollection GetCollection(object target, bool includeEmpty, IEnumerable items = null)
        {
            return new List<Guid>();
        }

        public ICollection<ValueItem> GetValueItems(object target, ICollection items)
        {
            var list = items.Cast<Guid>();

            return (from i in list select ValueItem.New(i.ToString(), i, i.Equals(target))).ToList();
        }

//        public ICollection<ValueItem> GetCollection(Type targetType, bool includeEmpty, IEnumerable items = null)
//        {
//            return GetCollection((object)null, includeEmpty, items);
//        }

        /// <summary>
        /// Checks whether the specified object is empty or not.
        /// </summary>
        /// <param name="value">The Object to be checked</param>
        /// <returns>True if the specified object is empty, false otherwise.</returns>
        public bool IsEmpty(object value)
        {
            Guid guid = new Guid(value.ToString());

            return (guid == Guid.Empty);
        }

        /// <summary>
        /// Checks whether the two specified Objects are equal or not.
        /// </summary>
        /// <param name="compare">The Object to compare.</param>
        /// <param name="to">The Object to compare with.</param>
        /// <returns>True if both the specified Objects are equal, false otherwise.</returns>
        public bool IsEqual(object compare, object to)
        {
            if (compare is Guid && to is Guid)
            {
                Guid left = (Guid)compare;
                Guid right = (Guid)to;

                return left == right;
            }

            return false;
        }

        /// <summary>
        /// Indicates whether the specified <see cref="Type"/> is parsable or not.
        /// </summary>
        /// <param name="type">The specified <see cref="Type"/>.</param>
        /// <returns>True if the specified <see cref="Type"/> is parsable, false otherwise.</returns>
        public bool IsParsable(Type type)
        {
            return type.Equals(typeof(Guid));
        }

        #endregion
    }
}
