using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Adf.Core.Domain;
using Adf.Core.Identity;

namespace Adf.Base.Domain
{
    /// <summary>    
    /// This class represents parsing operations for properties of DomainObjects.
    /// </summary>
    class DomainObjectPropertyParser : IPropertyParser
    {
      
        #region IPropertyParser Members

        /// <summary>
        /// Sets the new value for a specific property on an object.
        /// </summary>
        /// <param name="instance">Target DomainObject to set property at.</param>
        /// <param name="pi">Property info for the property to set.</param>
        /// <param name="newvalue">Value for the property.</param>
        /// <param name="culture"></param>
        /// <exception cref="System.ArgumentNullException">null reference is not accept as a valid argument.</exception>
        /// <exception cref="System.ArgumentException">Not a valid argument.</exception> 
        /// <exception cref="System.Reflection.TargetInvocationException"></exception> 
        /// <exception cref="System.MethodAccessException"></exception>
        public void SetValue(object instance, PropertyInfo pi, object newvalue, CultureInfo culture = null)
        {
            if (pi.PropertyType.FullName == null) throw new ArgumentException("pi.PropertyType.FullName cannot be null.");
            if (pi.PropertyType.AssemblyQualifiedName == null) throw new ArgumentException("pi.PropertyType.AssemblyQualifiedName cannot be null.");

            var type = Type.GetType(pi.PropertyType.AssemblyQualifiedName.Replace(pi.PropertyType.FullName, pi.PropertyType.FullName + "Factory"));
            var method = type.GetMethod("Get", BindingFlags.Static | BindingFlags.Public, null, new[] { typeof(ID) }, null);
            var domainobject = method.Invoke(null, new object[] { IdManager.New(newvalue) });

            pi.SetValue(instance, domainobject, null);
        }

        /// <summary>
        /// Gets the collection of the property info for the specified target object.
        /// </summary>
        /// <param name="target">Object to get collection for.</param>
        /// <returns>Collection of values for type of property, using the value on the target object 
        /// for this property as default value.</returns>
        protected virtual List<ValueItem> GetCollection(object target, IEnumerable items = null)
        {
            var collection = new List<ValueItem>();

            var domainobject = target as IDomainObject;
            if (domainobject == null) return collection;

            var list = items ?? domainobject.GetAll();
            if (list == null) return collection;

            collection.AddRange(from IDomainObject o in list
                                select ValueItem.New(o.ToString(), o.Id, o.Equals(domainobject)));

            // if current value is not in list, add it
            if (!domainobject.IsEmpty && !collection.Any(vi => vi.Selected))
            {
                collection.Insert(0, ValueItem.New(string.Format("<{0}>", domainobject), domainobject.Id, true));
            }

            return collection;
        }

        /// <summary>
        /// Gets the collection of the property info for the specified target object.
        /// An indicator is also supplied to indicate whether an empty DomainObject will be included or not.
        /// </summary>
        /// <param name="target">Object to get default value from.</param>
        /// <param name="includeEmpty">The indicator to indicate whether empty will be included or not.</param>
        /// <param name="items"></param>
        /// <returns>Returns a ValueCollection of instances of the type of the target having an empty ValueItem.</returns>
        public virtual ICollection<ValueItem> GetCollection(object target, bool includeEmpty, IEnumerable items = null)
        {
            var collection = GetCollection(target, items);

            if (includeEmpty)
            {
                var domainobject = target as IDomainObject;
                if (domainobject == null) return collection;

                var empty = domainobject.Empty();                

                collection.Insert(0, ValueItem.New(empty.ToString(), empty.Id, empty.Equals(target)));
            }

            return collection;
        }

        public ICollection<ValueItem> GetCollection(Type targetType, bool includeEmpty, IEnumerable items = null)
        {
            return GetCollection(DomainObjectExtensions.Empty(targetType), includeEmpty, items);
        }

        /// <summary>
        /// Indicates whether the supplied object is empty or not.
        /// </summary>
        /// <param name="value">The Object to be checked for null or empty.</param>
        /// <returns>True if the supplied object is empty, false otherwise.</returns>
        /// <exception cref="System.ArgumentException">Argument is not valid</exception>
        public bool IsEmpty(object value)
        {
            var domainobject = value as IDomainObject;
            if (domainobject == null)
            {
                throw new ArgumentException("Argument is not a valid domain object");
            }

            return domainobject.IsEmpty;
        }

        /// <summary>
        /// Checks whether the two specified Objects are equal or not.
        /// </summary>
        /// <param name="compare">The Object to compare.</param>
        /// <param name="to">The Object to compare with.</param>
        /// <returns>True if both the specified Objects are equal, false otherwise.</returns>
        public bool IsEqual(object compare, object to)
        {
            var left = compare as DomainObject;
            var right = to as DomainObject;

            return left == right;
        }

        /// <summary>
        /// Indicates whether the specified <see cref="Type"/> is parsable or not.
        /// </summary>
        /// <param name="type">The specified <see cref="Type"/>.</param>
        /// <returns>True if the specified <see cref="Type"/> is parsable, false otherwise.</returns>
        public bool IsParsable(Type type)
        {
            return typeof(IDomainObject).IsAssignableFrom(type);
        }

        #endregion
    }
}
