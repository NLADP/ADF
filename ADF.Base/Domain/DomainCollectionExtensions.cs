using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Adf.Core.Domain;
using Adf.Core.Extensions;
using Adf.Core.Validation;

namespace Adf.Base.Domain
{
    public static class DomainCollectionExtensions
    {
        public static DomainCollection<T> SortAscending<T>(this DomainCollection<T> collection, string sortProperty) where T : IDomainObject
        {
            collection.Items.Sort(new ListSorter<T>(sortProperty, SortOrder.Ascending));

            return collection;
        }

        public static DomainCollection<T> SortDescending<T>(this DomainCollection<T> collection, string sortProperty) where T : IDomainObject
        {
            collection.Items.Sort(new ListSorter<T>(sortProperty, SortOrder.Descending));

            return collection;
        }

        public static DomainCollection<T> SortAscending<T>(this DomainCollection<T> collection, Expression<Func<T, object>> sortProperty) where T : IDomainObject
        {
            return collection.SortAscending(sortProperty.GetPropertyPath());
        }

        public static DomainCollection<T> SortDescending<T>(this DomainCollection<T> collection, Expression<Func<T, object>> sortProperty) where T : IDomainObject
        {
            return collection.SortDescending(sortProperty.GetPropertyPath());
        }

        public static DomainCollection<T> Set<T>(this DomainCollection<T> collection, Expression<Func<T, object>> property, IDomainObject value) where T : IDomainObject
        {
            var info = property.GetExpressionMember() as PropertyInfo;
            foreach (var c in collection)
            {
                info.SetValue(c, value, new object[0]);
            }

            return collection;
        }

        public static DomainCollection<T> InitializeProperty<T>(this DomainCollection<T> collection, Expression<Func<T, object>> property) where T : IDomainObject
        {
            var info = property.GetExpressionMember() as PropertyInfo;

            foreach (var item in collection)
            {
                info.GetValue(item, null);
            }

            return collection;
        }

        public static bool IsNullOrEmpty<T>(this DomainCollection<T> collection) where T : IDomainObject
        {
            return collection == null || collection.IsInitialised;
        }

        public static DomainCollection<T> Where<T>(this DomainCollection<T> source, Func<T, bool> predicate) where T : IDomainObject
        {
            return new DomainCollection<T>((source as IEnumerable<T>).Where(predicate));
        }

        public static bool Validate<T>(this DomainCollection<T> collection) where T : IDomainObject
        {
            if (collection.IsNullOrEmpty()) return true;

            foreach (var c in collection)
            {
                c.Validate();
            }

            return ValidationManager.IsSucceeded;
        }
    }
}
