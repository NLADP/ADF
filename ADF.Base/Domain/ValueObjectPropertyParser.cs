using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Adf.Core.Domain;
using Adf.Core.Validation;

namespace Adf.Base.Domain
{
    /// <summary>
    /// Represents ValueObject property parsing operations.
    /// </summary>
    class ValueObjectPropertyParser : IPropertyParser
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
            culture = culture ?? CultureInfo.CurrentCulture;

            Type propertyType = pi.PropertyType;

            IValueObject parsedValue = null;

            var args = new object[] {newvalue.ToString(), culture, parsedValue};
            var isValid = (bool) propertyType.InvokeMember("TryParse", BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.Public, null, null, args, culture);

            parsedValue = args[2] as IValueObject;  // get returned out param from args array

            if (isValid)
            {
                pi.SetValue(instance, parsedValue, null);
            }
            else
            {
                ValidationManager.AddError(pi, "Adf.Business.NotInstantiable", newvalue, pi.Name);
            }
        }

        /// <summary>
        /// Gets the collection of the property info for the specified target object.
        /// An indicator is also supplied to indicate whether empty will be included or not.
        /// </summary>
        /// <param name="target">Object to get collection for.</param>
        /// <param name="includeEmpty">The indicator to indicate whether empty will be included or not.</param>
        /// <param name="items"></param>
        /// <returns>Returns a new instance of ValueCollection with no ValueItems.</returns>
        public ICollection<ValueItem> GetCollection(object target, bool includeEmpty, IEnumerable items = null)
        {
            return new List<ValueItem>();
        }

        public ICollection<ValueItem> GetCollection(Type targetType, bool includeEmpty, IEnumerable items = null)
        {
            return GetCollection((object) null, includeEmpty, items);
        }

        /// <summary>
        /// Checks whether the specified object is empty or not.
        /// </summary>
        /// <param name="value">The Object to be checked for null or empty.</param>
        /// <returns>True if the specified object is empty, false otherwise.</returns>
        public bool IsEmpty(object value)
        {
            var valueobject = value as IValueObject;

            if (valueobject == null)
            {
                throw new ArgumentException("Argument is not a valid value object");
            }

            return valueobject.IsEmpty;
        }

        /// <summary>
        /// Checks whether the two specified Objects are equal or not.
        /// </summary>
        /// <param name="compare">The Object to compare.</param>
        /// <param name="to">The Object to compare with.</param>
        /// <returns>True if both the specified Objects are equal, false otherwise.</returns>
        /// <exception cref="System.ArgumentException">Not a valid value object.</exception> 
        public bool IsEqual(object compare, object to)
        {
            var left = compare as IValueObject;
            var right = to as IValueObject;

            if (left == null) throw new ArgumentException("Argument is not a valid value object");
            if (right == null) throw new ArgumentException("Argument is not a valid value object");

            if (left.GetType() != right.GetType()) return false;

            if (left.IsEmpty && right.IsEmpty) return true;

            if (left.IsEmpty || right.IsEmpty) return false;

            return left.Equals(right);
        }

        /// <summary>
        /// Indicates whether the specified <see cref="Type"/> is parsable or not.
        /// </summary>
        /// <param name="type">The specified <see cref="Type"/>.</param>
        /// <returns>True if the specified <see cref="Type"/> is parsable, false otherwise.</returns>
        public bool IsParsable(Type type)
        {
            return type.GetInterface("IValueObject") != null;
        }

        #endregion
    }
}
