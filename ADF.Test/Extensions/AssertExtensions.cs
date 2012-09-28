using System;
using System.Collections.Generic;
using System.Linq;
using Adf.Base.Domain;
using Adf.Core.Domain;
using Adf.Test.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adf.Test.Extensions
{
    public static class AssertExtensions
    {
        #region Empty

        public static T IsEmpty<T>(this T target, Func<T, IDomainObject> func)
        {
            var property = func.Invoke(target);

            Assert.IsNotNull(property, "Domain object [{0}] is null, but should be empty.", property);
            Assert.IsTrue(property.IsEmpty, "Domain object [{0}] is filled, but should be empty.", property);

            return target;
        }

        public static T IsEmpty<T>(this T target, Func<T, IValueObject> func)
        {
            var property = func.Invoke(target);

            Assert.IsTrue(property.IsEmpty, "Value object [{0}] is not empty, but should be empty.", property);

            return target;
        }

        public static T IsEmpty<T>(this T target, Func<T, IEnumerable<IDomainObject>> func)
        {
            var list = func.Invoke(target);

            Assert.IsNotNull(list, "Domain collection [{0}] is null, but should be empty.", list);
            Assert.IsTrue(!list.Any(), "Domain collection [{0}] contains elements, but should be empty.", list);

            return target;
        }

        public static T IsNotEmpty<T>(this T target, Func<T, IDomainObject> func)
        {
            var property = func.Invoke(target);

            Assert.IsNotNull(property, "Domain object [{0}] is null, but should exist.", property);
            Assert.IsFalse(property.IsEmpty, "Domain object [{0}] is empty, but should be filled.", property);

            return target;
        }

        public static T IsNotEmpty<T>(this T target, Func<T, IEnumerable<IDomainObject>> func)
        {
            var list = func.Invoke(target);

            Assert.IsNotNull(list, "Domain collection [{0}] is null, but should exist.", list);
            Assert.IsTrue(list.Any(), "Domain collection [{0}] is empty, but should contain elements.", list);

            return target;
        }

        public static T IsNotEmpty<T>(this T task, Func<T, IValueObject> func)
        {
            var property = func.Invoke(task);

            Assert.IsFalse(property.IsEmpty, "Value object [{0}] is empty, but should be filled.", property);

            return task;
        }

        #endregion Empty

        #region Null

        public static T IsNull<T>(this T target, Func<T, object> func)
        {
            var property = func.Invoke(target);

            Assert.IsNull(property, "Object exists, but should be null.");

            return target;
        }

        public static T IsNotNull<T>(this T target, Func<T, object> func)
        {
            var property = func.Invoke(target);

            Assert.IsNotNull(property, "Object is null, but should exist.");

            return target;
        }

        #endregion Null

        #region Equal

        public static T IsEqual<T>(this T target, Func<T, object> func, object origin)
        {
            var property = func.Invoke(target);

            Assert.AreEqual(origin, property, "Property [{0}] is not equal to [{1}], but should have been the same.", property, origin);

            return target;
        }

        public static T IsNotEqual<T>(this T target, Func<T, object> func, object origin)
        {
            var property = func.Invoke(target);

            Assert.AreNotEqual(origin, property, "Property [{0}] is equal to [{1}], but should have been different.", property, origin);

            return target;
        }

        #endregion Equal

        #region True False

        public static T IsTrue<T>(this T target, Func<T, object> func)
        {
            var property = func.Invoke(target);

            Assert.AreEqual(true, property, "Property [{0}] is false. Expected was true", property.ToString());

            return target;
        }

        public static T IsFalse<T>(this T target, Func<T, object> func)
        {
            var property = func.Invoke(target);

            Assert.AreEqual(false, property, "Property [{0}] is true. Expected was false", property.ToString());

            return target;
        }

        #endregion True False

        #region In

        public static T IsIn<T, D>(this T target, Func<T, DomainCollection<D>> func, D element) where D : class, IDomainObject
        {
            element.IsIn(func.Invoke(target));

            return target;
        }

        public static T IsNotIn<T, D>(this T target, Func<T, DomainCollection<D>> func, D element) where D : class, IDomainObject
        {
            element.IsNotIn(func.Invoke(target));

            return target; 
        }
        
        #endregion In

        #region IsInitialized

        public static T IsInitialised<T, D>(this T target, Func<T, DomainCollection<D>> func) where D : class, IDomainObject
        {
            func.Invoke(target).IsInitialised();

            return target;
        }

        public static T IsNotInitialised<T, D>(this T target, Func<T, DomainCollection<D>> func) where D : class, IDomainObject
        {
            func.Invoke(target).IsNotInitialised();

            return target;
        }

        #endregion IsInitialized

        #region Count

        public static T HasCount<T, D>(this T target, Func<T, DomainCollection<D>> func, int count) where D : class, IDomainObject
        {
            func.Invoke(target).HasCount(count);

            return target;
        }

        #endregion Count
    }
}
