using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Adf.Core.Domain;
using Adf.Core.Extensions;

namespace Adf.Base.Domain
{
    /// <summary>
    /// Represents Enum property parsing operations.
    /// </summary>
    class EnumPropertyParser : IPropertyParser
    {
        #region IPropertyParser Members

        /// <summary>
        /// Sets the new value for a specific property on an object.
        /// </summary>
        /// <param name="instance">Target object to set property at.</param>
        /// <param name="pi">Property info for the property to set.</param>
        /// <param name="newvalue">Value for the property.</param>
        /// <param name="culture"></param>
        /// <exception cref="System.ArgumentNullException">null reference is not accept as a valid argument.</exception>
        /// <exception cref="System.ArgumentException">Not a valid argument.</exception> 
        /// <exception cref="System.Reflection.TargetInvocationException"></exception> 
        /// <exception cref="System.MethodAccessException"></exception>
        public void SetValue(object instance, PropertyInfo pi, object newvalue, CultureInfo culture = null)
        {
            string value = null;
            
            if (newvalue != null) value = newvalue.ToString();

            if (string.IsNullOrEmpty(value))
            {
                value = "Empty";
            }
            Object e = Enum.Parse(pi.PropertyType, value);

            pi.SetValue(instance, e, null);
        }

        /// <summary>
        /// Gets the collection of the property info for the specified target object.
        /// </summary>
        /// <param name="target">Object to get default value from.</param>
        /// <returns>Collection of values for type of property, using the value on the target object 
        /// for this property as default value.</returns>
        public List<ValueItem> GetCollection(object target, IEnumerable items = null)
        {
            var enumValue = target as Enum;
            if (enumValue == null)
            {
                return new List<ValueItem>();
            }

            var values = items ?? Enum.GetValues(enumValue.GetType());

            var collection = new List<ValueItem>();

            // Note: do not convert the foreach to a Linq expression, the list will not be ordered as defined anymore.
            foreach (Enum value in values)
            {
                if (!value.IsExcluded())
                {
                    var item = ValueItem.New(value.GetDescription(), value.ToString(), IsEqual(value, enumValue));

                    // Always insert empty item at index 0
                    if(IsEmpty(value))
                        collection.Insert(0,item);
                    else
                        collection.Add(item);
                }
            }

            // if current value is not in list, add it
            if (!IsEmpty(enumValue) && !collection.Any(vi => vi.Selected))
            {
                collection.Insert(0, ValueItem.New(string.Format("<{0}>", enumValue.GetDescription()), enumValue.ToString(), true));
            }

            return collection;
        }

        /// <summary>
        /// Gets the collection of the property info for the specified target object.
        /// An indicator is also supplied to indicate whether empty will be included or not.
        /// </summary>
        /// <param name="target">The object.</param>
        /// <param name="includeEmpty">The indicator to indicate whether empty will be included or not.</param>
        /// <param name="items"></param>
        /// <returns>The collection.</returns>
        public IEnumerable<ValueItem> GetCollection(object target, bool includeEmpty, IEnumerable items = null)
        {
            List<ValueItem> collection = GetCollection(target, items).ToList();

            if (!includeEmpty)
            {
                foreach (ValueItem item in collection)
                {
                    if (IsEmpty(item.Value))
                    {
                        collection.Remove(item);
                        // assume theres only one empty item
                        break;
                    }
                }
            }

            return collection;
        }

        /// <summary>
        /// Checks whether the supplied object is empty or not.
        /// </summary>
        /// <param name="value">The supplied object.</param>
        /// <returns>True if the supplied object is empty, false otherwise.</returns>
        public bool IsEmpty(object value)
        {
            if (string.IsNullOrEmpty(value.ToString()))
            {
                throw new ArgumentException("This is not a valid enumeration");
            }

            return (value.ToString().ToUpper() == "EMPTY");
        }

        /// <summary>
        /// Checks whether the two specified Objects are equal or not.
        /// </summary>
        /// <param name="compare">The Object to compare.</param>
        /// <param name="to">The Object to compare with.</param>
        /// <returns>True if both the specified Objects are equal, false otherwise.</returns>
        public bool IsEqual(object compare, object to)
        {
            var left = compare as Enum;
            var right = to as Enum;

            if (left == null && right == null) return true;
            if (left == null || right == null) return false;

            return left.Equals(right);
            //            return left == right;
        }

        /// <summary>
        /// Indicates whether the specified <see cref="Type"/> is parsable or not.
        /// </summary>
        /// <param name="type">The specified <see cref="Type"/>.</param>
        /// <returns>True if the specified <see cref="Type"/> is parsable, false otherwise.</returns>
        public bool IsParsable(Type type)
        {
            return type.IsEnum;
        }

        #endregion
    }
}
