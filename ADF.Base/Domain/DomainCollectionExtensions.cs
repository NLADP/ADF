using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Adf.Core.Domain;
using Adf.Core.Extensions;
using Adf.Core.Identity;
using Adf.Core.Validation;

namespace Adf.Base.Domain
{
    public static class DomainCollectionExtensions
    {
        public static DomainCollection<T> SortAscending<T>(this DomainCollection<T> collection, string sortProperty) where T : class, IDomainObject
        {
            return (DomainCollection<T>) collection.Sort(sortProperty, SortOrder.Ascending);
        }

        public static DomainCollection<T> SortDescending<T>(this DomainCollection<T> collection, string sortProperty) where T : class, IDomainObject
        {
            return (DomainCollection<T>) collection.Sort(sortProperty, SortOrder.Descending);
        }

        public static DomainCollection<T> SortAscending<T>(this DomainCollection<T> collection, Expression<Func<T, object>> sortProperty) where T : class, IDomainObject
        {
            return collection.Sort(sortProperty, SortOrder.Ascending);
        }

        public static DomainCollection<T> SortDescending<T>(this DomainCollection<T> collection, Expression<Func<T, object>> sortProperty) where T : class, IDomainObject
        {
            return collection.Sort(sortProperty, SortOrder.Descending);
        }

        public static DomainCollection<T> Set<T>(this DomainCollection<T> collection, Expression<Func<T, object>> expression, IDomainObject value) where T : class, IDomainObject
        {
            var info = expression.GetPropertyInfo();

            foreach (var c in collection)
            {
                info.SetValue(c, value, new object[0]);
            }

            return collection;
        }

        public static DomainCollection<T> InitOrReturn<T>(this DomainCollection<T> collection, Func<DomainCollection<T>> expression) where T : class, IDomainObject
        {
            if (collection == null) collection = new DomainCollection<T>();

            return collection.IsInitialised ? collection : collection.Add(expression.Invoke());
        }

        /// <summary>
        /// Initializes a property by getting the value. This could be a heavy operation depending on the size of the collection.
        /// Note: Make sure the expression is optimized!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static DomainCollection<T> InitializeProperty<T>(this DomainCollection<T> collection, Expression<Func<T, object>> expression) where T : class, IDomainObject
        {
            var info = expression.GetPropertyInfo();

            foreach (var item in collection)
            {
                info.GetValue(item, null);
            }

            return collection;
        }

        public static bool IsNullOrEmpty<T>(this DomainCollection<T> collection) where T : class, IDomainObject
        {
            return collection == null || collection.Count == 0;
        }

        public static DomainCollection<T> Where<T>(this DomainCollection<T> source, Func<T, bool> predicate) where T : class, IDomainObject
        {
            return new DomainCollection<T>((source as IEnumerable<T>).Where(predicate));
        }

        public static bool Validate<T>(this DomainCollection<T> collection) where T : class, IDomainObject
        {
            if (collection.IsNullOrEmpty()) return true;

            foreach (var c in collection)
            {
                c.Validate();
            }

            return ValidationManager.IsSucceeded;
        }

        public static bool IsNotInitialized<T>(this DomainCollection<T> collection) where T : class, IDomainObject
        {
            return collection == null || !collection.IsInitialised;
        }

        public static DomainCollection<T> ToDomainCollection<T>(this IEnumerable<T> enumerable) where T : class, IDomainObject
        {
            return new DomainCollection<T>(enumerable);
        }

        public static T Find<T>(this IEnumerable<T> collection, ID id) where T : class, IDomainObject
        {
            return collection.FirstOrDefault(item => item.Id == id);
        }

        public static T Find<T>(this IEnumerable<T> collection, string id) where T : class, IDomainObject
        {
            return collection.Find(new ID(id));
        }

        public static T Find<T>(this IEnumerable<T> collection, object id) where T : class, IDomainObject
        {
            return collection.Find(new ID(id.ToString()));
        }

    }
}
