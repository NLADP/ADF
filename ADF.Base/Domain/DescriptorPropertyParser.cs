using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Adf.Core;
using Adf.Core.Domain;
using Adf.Core.Extensions;

namespace Adf.Base.Domain
{
    /// <summary>
    /// This class represents parsing operations for properties of type Descriptor.
    /// </summary>
    public class DescriptorPropertyParser : IPropertyParser
    {
        #region IPropertyParser Members

        /// <summary>
        /// Sets the new value for a specific property on an object.
        /// </summary>
        /// <param name="instance">Target object to set property at.</param>
        /// <param name="pi">Property info for the property to set.</param>
        /// <param name="newvalue">Value for the property.</param>
        /// <param name="culture"></param>
        /// <exception cref="System.InvalidCastException">Unable to convert the new value to the specified property. </exception>
        /// <exception cref="System.ArgumentNullException">null reference is not accept as a valid argument.</exception>
        public void SetValue(object instance, PropertyInfo pi, object newvalue, CultureInfo culture)
        {
            var descriptor = Descriptor.Get(pi.PropertyType, newvalue.ToString());

            pi.SetValue(instance, descriptor, null);
        }

        /// <summary>
        /// Gets the collection of the property info for the specified target object.
        /// </summary>
        /// <param name="target">Object to get default value from.</param>
        /// <param name="items"></param>
        /// <returns>Collection of values for type of property, using the value on the target object 
        /// for this property as default value.</returns>
        private static List<Descriptor> GetCollection(object target, IEnumerable items = null)
        {
            var item = target as Descriptor;
            if (item == null)
            {
                return new List<Descriptor>();
            }

            var list = items == null ?  Descriptor.GetValues(target.GetType()) : items.Cast<Descriptor>();

            return list.ToList();
        }

        /// <summary>
        /// Gets the collection of the property info for the specified target object.
        /// An indicator is also supplied to indicate whether a empty descriptor will be included or not.
        /// </summary>
        /// <param name="target">The object.</param>
        /// <param name="includeEmpty">The indicator to indicate whether empty will be included or not.</param>
        /// <param name="items"></param>
        /// <returns>The collection.</returns>
        public ICollection GetCollection(object target, bool includeEmpty, IEnumerable items = null)
        {
            var col = GetCollection(target, items);

            if (includeEmpty)
            {
                col.Insert(0, Descriptor.Empty);
            }

            return col;
        }

        public ICollection<ValueItem> GetValueItems(object target, ICollection items)
        {
            var item = target as Descriptor;
            var list = items.Cast<Descriptor>().ToList();

            var collection = list.Select(d => ValueItem.New(d.Name, d.Name, d == item)).ToList();


            // if current value is not in list, add it
            if (!item.IsEmpty && !collection.Any(vi => vi.Selected))
            {
                collection.Insert(0, ValueItem.New(string.Format("<{0}>", item.Name), item.Name, true));
            }
            return collection;
        }

//        public ICollection<ValueItem> GetCollection(Type targetType, bool includeEmpty, IEnumerable items = null)
//        {
//            return GetCollection(Descriptor.GetValues(targetType).First(), includeEmpty, items);
//        }

        /// <summary>
        /// Checks whether the specified object is empty or not.
        /// </summary>
        /// <param name="target">The Object to be checked for null or empty.</param>
        /// <returns>True if the supplied object is empty, false otherwise.</returns>
        public bool IsEmpty(object target)
        {
            var item = target as Descriptor;
            if (item == null || string.IsNullOrEmpty(item.Name))
            {
                return true;
            }

            return (item.Name.ToUpper() == "EMPTY");
        }

        /// <summary>
        /// Checks whether the two specified Objects are equal or not.
        /// </summary>
        /// <param name="compare">The first supplied object.</param>
        /// <param name="to">The second supplied object.</param>
        /// <returns>True if the two supplied objects are equal, false otherwise.</returns>
        public bool IsEqual(object compare, object to)
        {
            var left = compare as Descriptor;
            var right = to as Descriptor;

            return left == right;
        }

        /// <summary>
        /// Indicates whether the specified <see cref="Type"/> is parsable or not.
        /// </summary>
        /// <param name="type">The supplied <see cref="Type"/>.</param>
        /// <returns>True if the supplied <see cref="Type"/> is parsable, false otherwise.</returns>
        public bool IsParsable(Type type)
        {
            return typeof(Descriptor).IsAssignableFrom(type);
        }

        #endregion
    }
}
