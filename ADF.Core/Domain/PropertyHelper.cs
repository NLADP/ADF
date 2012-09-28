using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Adf.Core.Logging;
using Adf.Core.Objects;

namespace Adf.Core.Domain
{
    /// <summary>
    /// Represents PropertyHelper. It's a facade class with functions for properties.
    /// It organizes the plugin for all configured property parsers. 
    /// </summary>
    public static class PropertyHelper
    {
        /// <summary>
        /// Saves the parsers in this list for caching.
        /// </summary>
        private static IEnumerable<IPropertyParser> _propertyParsers;

        private static readonly object Lock = new object();

        /// <summary>
        /// Gets the list of <see cref="IPropertyParser"/>s.
        /// </summary>
        private static IEnumerable<IPropertyParser> PropertyParsers
        {
            get { lock (Lock) return _propertyParsers ?? (_propertyParsers = ObjectFactory.BuildAll<IPropertyParser>().ToList()); }
        }

        /// <summary>
        /// Gets the correct parser for the specified <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The specified <see cref="Type"/>.</param>
        /// <returns>The corresponding parser.</returns>
        /// <exception cref="Exception">No property parser configured for this type</exception>
        public static IPropertyParser GetParser(Type type)
        {
            return PropertyParsers.FirstOrDefault(parser => parser.IsParsable(type));
        }

        #region IPropertyParser Members

        /// <summary>
        /// Sets the new value for a specific property on an object. 
        /// </summary>
        /// <param name="instance">Target object to set property at. In most cases this will be a 
        /// domain object.</param>
        /// <param name="pi">Property info for the property to set.</param>
        /// <param name="newvalue">Value to try to parse.</param>
        /// <param name="culture"></param>
        public static void SetValue(object instance, PropertyInfo pi, object newvalue, CultureInfo culture = null)
        {
            var parser = GetParser(pi.PropertyType);

            parser.SetValue(instance, pi, newvalue, culture);
        }

//        /// <summary>
//        /// Gets the <see cref="ValueCollection"/> of the property info for the specified target object.For example, if a Customer 
//        /// domain object is presented, this method will return all customers. If a value from an enum 
//        /// is presented, it will return all possible values for this enum.
//        /// </summary>
//        /// <param name="value">Value of a type for which a value collection can be fetched.</param>
//        /// <returns>A collection of values of the same type as the value parameter.</returns>
//        public static ValueCollection GetCollectionWithDefault(object value)
//        {
//            var parser = GetParser(value.GetType());
//
//            return parser.GetCollection(value);
//        }

        /// <summary>
        /// Gets the collection of the property info for the specified target object.
        /// An indicator is also supplied to indicate whether empty will be included or not.
        /// </summary>
        /// <param name="value">The object.</param>
        /// <param name="includeEmpty">The indicator to indicate whether empty will be included or not.</param>
        /// <param name="items"></param>
        /// <returns>The collection.</returns>
        public static ICollection<ValueItem> GetCollectionWithDefault(object value, bool includeEmpty, IEnumerable items = null)
        {
            var parser = GetParser(value.GetType());

            return parser.GetCollection(value, includeEmpty, items);
        }

        public static ICollection<ValueItem> GetCollectionWithDefault(Type type, bool includeEmpty, IEnumerable items = null)
        {
            var parser = GetParser(type);

            return parser.GetCollection(type, includeEmpty, items);
        }

        /// <summary>
        /// Returns if the instance presented as parameter is empty or not, dependent on its type. 
        /// Emptyness is implemented differently in different types of objects. For example, a 
        /// domain object is considered empty if it's Id property is empty. A enumeration can be empty
        /// if the value presented equals the string "Empty".
        /// </summary>
        /// <param name="target">Target to check if empty.</param>
        /// <returns>Return true if the object is empty, false if it is not empty.</returns>
        public static bool IsEmpty(object target)
        {
            if (target == null)
                return true;

            var parser = GetParser(target.GetType());

            return parser.IsEmpty(target);
        }

        public static bool IsEqual(object compare, object to)
        {
            if (ReferenceEquals(compare, to)) return true;

            if (compare == null) return false;

            return GetParser(compare.GetType()).IsEqual(compare, to);
        }

        #endregion

        /// <summary>
        /// Returns the value of the supplied property of the supplied object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pi">The property.</param>
        /// <param name="fromObject">The object.</param>
        /// <returns>The value of the supplied property of the supplied object.</returns>
        public static T GetValue<T>(PropertyInfo pi, object fromObject)
        {
            return (T)pi.GetValue(fromObject, null);
        }

        ///<summary>
        /// Returns value of the supplied property of the supplied object. 
        ///</summary>
        ///<param name="entity"></param>
        ///<param name="propertyPath">Can contain dots ('.') and will follow the property path</param>
        ///<returns></returns>
        public static object GetValue(object entity, string propertyPath)
        {
            if (entity == null) throw new ArgumentNullException("entity");
            if (string.IsNullOrEmpty(propertyPath)) throw new ArgumentNullException("propertyPath");

            try
            {
                var index = propertyPath.IndexOf('.');

                if (index > 0)
                {
                    var property = propertyPath.Substring(0, index);
                    var rest = propertyPath.Substring(index + 1);

                    return GetValue(GetValue(entity, property), rest);
                }

                // else
                var propertyInfo = entity.GetType().GetProperty(propertyPath);

                return propertyInfo != null ? propertyInfo.GetValue(entity, null) : null;
            }
            catch (Exception ex)
            {
                LogManager.Log(string.Format("Could not find property {0} in entity {1} of type {2}", propertyPath, entity, entity.GetType()));
                LogManager.Log(ex);
                throw;
            }
        }
    }
}
