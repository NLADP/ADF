using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Adf.Core.Domain;
using Adf.Core.Identity;

namespace Adf.Business.SmartReferences
{
    /// <summary>
    /// Represents SmartReference property parsing operations.
    /// </summary>
    public class SmartReferencePropertyParser : IPropertyParser
    {
        #region IPropertyParser Members

        /// <summary>
        /// Sets the new value for a specific property on an object.
        /// </summary>
        /// <param name="instance">Target object to set property at. </param>
        /// <param name="pi">Property info for the property to set.</param>
        /// <param name="newvalue">Value for the property.</param>
        /// <param name="culture"></param>
        /// <exception cref="System.ArgumentException">Not a valid argument.</exception>                    
        public void SetValue(object instance, PropertyInfo pi, object newvalue, CultureInfo culture = null)
        {
            var item = SmartReferenceFactory.Get(pi.PropertyType.GetGenericArguments()[0], (ID) newvalue);

            pi.SetValue(instance, item, null);
        }

        /// <summary>
        /// Gets the collection of the property info for the specified target object.
        /// </summary>
        /// <param name="target">Object to get collection for.</param>
        /// <param name="items"></param>
        /// <returns>Returns a collection of instances of the type of the target.</returns>
        private static List<ISmartReference> GetCollection(object target, IEnumerable items = null)
        {
            var value = target as ISmartReference;

            if (value == null) return new List<ISmartReference>();

            return items == null ? SmartReferenceFactory.GetAll(value.Type).ToList() : items.Cast<ISmartReference>().ToList();
        }

        /// <summary>
        /// Gets the collection of the property info for the specified target object.
        /// An indicator is also supplied to indicate whether empty will be included or not.
        /// </summary>
        /// <param name="target">Object to get collection for.</param>
        /// <param name="includeEmpty">The indicator to indicate whether empty will be included or not.</param>
        /// <param name="items"></param>
        /// <returns>Returns a collection of instances of the type of the target having an empty ValueItem</returns>
        public ICollection GetCollection(object target, bool includeEmpty, IEnumerable items = null)
        {
            var collection = GetCollection(target);

            if (includeEmpty)
            {
                collection.Insert(0, SmartReferenceFactory.Create(((ISmartReference) target).Type)); // ValueItem.New(string.Empty, IdManager.Empty(), ((ISmartReference)target).IsEmpty));
            }

            return collection;
        }

        public ICollection<ValueItem> GetValueItems(object target, ICollection items)
        {
            var value = target as ISmartReference;
            if (value == null) throw new ArgumentException("not a ISmartReference", "target");

            var collection = items.Cast<ISmartReference>().Select(smartReference => ValueItem.New(smartReference.ToString(), smartReference.Id, smartReference.Id == value.Id)).ToList();

            // if current value is not in list, add it
            if (!value.IsEmpty && !collection.Any(vi => vi.Selected))
            {
                collection.Insert(0, ValueItem.New(string.Format("<{0}>", value), value.Id, true));
            }
            return collection;
        }

//        public ICollection<ValueItem> GetCollection(Type targetType, bool includeEmpty, IEnumerable items = null)
//        {
//            return GetCollection(targetType.New<ISmartReference>(), includeEmpty, items);
//        }

        /// <summary>
        /// Indicates whether the supplied object is empty or not.
        /// </summary>
        /// <param name="value">The Object to be checked for null or empty.</param>
        /// <returns>true if the supplied object is empty, false otherwise.</returns>
        /// <exception cref="System.ArgumentException">Argument is not valid</exception>
        public bool IsEmpty(object value)
        {
            var smartReference = value as ISmartReference;
             
            if (smartReference == null) throw new ArgumentException("Value is not a valid smart reference.");

            return smartReference.IsEmpty;
        }

        /// <summary>
        /// Checks whether the two specified Objects are equal or not.
        /// </summary>
        /// <param name="compare">The Object to compare.</param>
        /// <param name="to">The Object to compare with.</param>
        /// <returns>True if both the specified Objects are equal, false otherwise.</returns>
        public bool IsEqual(object compare, object to)
        {
            var left = compare as ISmartReference;
            var right = to as string;

            if (left == null && right == null) return true;
            if (left == null || right == null) return false;

            return left.Name == right;
        }

        /// <summary>
        /// Indicates whether the supplied <see cref="Type"/> is parsable or not.
        /// </summary>
        /// <param name="type">The supplied <see cref="Type"/>.</param>
        /// <returns>True if the supplied <see cref="Type"/> is parsable, false otherwise.</returns>
        public bool IsParsable(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(SmartReference<>));
        }

        #endregion
    }
}
