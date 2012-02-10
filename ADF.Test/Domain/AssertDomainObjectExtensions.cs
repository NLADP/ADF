using System;
using System.Collections.Generic;
using System.Linq;
using Adf.Base.Domain;
using Adf.Core.Domain;
using Adf.Core.Test;
using Adf.Core.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adf.Test.Domain
{
    public static class AssertDomainObjectExtensions
    {
        public static T ValidationFailed<T>(this T domainobject) where T : IDomainObject
        {
            ValidationManager.Handle();

            TestManager.IsPresent(TestItemType.ValidationResult, TestAction.ValidationFailed);
 
            return domainobject;
        }

        public static T ValidationSucceeded<T>(this T domainobject) where T : IDomainObject
        {
            ValidationManager.Handle();

            TestManager.IsNotPresent(TestItemType.ValidationResult, TestAction.ValidationFailed);

            return domainobject;
        }

        public static T IsSet<T>(this T domainobject, Func<T, object> func, object origin) where T : IDomainObject
        {
            var property = func.Invoke(domainobject);

            Assert.AreEqual(origin, property, "Property [{0}] is not equal to [{1}] .", property.ToString(), origin.ToString());

            return domainobject;
        }

        public static T IsNotSet<T>(this T domainobject, Func<T, object> func, object origin) where T : IDomainObject
        {
            var property = func.Invoke(domainobject);

            Assert.AreNotEqual(origin, property, "Property [{0}] is equal to [{1}] .", property.ToString(), origin.ToString());

            return domainobject;
        }

        public static T IsElement<T>(this T domainobject, Func<T, DomainCollection<T>> func) where T : IDomainObject
        {
            var collection = func.Invoke(domainobject);

            Assert.IsTrue(collection.Contains(domainobject), "Domain object [{0}] is not an element in the collection.", domainobject.Title);

            return domainobject;
        }

        public static T IsEmpty<T>(this T domainobject, Func<T, IDomainObject> func) where T : IDomainObject
        {
            var property = func.Invoke(domainobject);

            Assert.IsTrue(property.IsEmpty, "Domain object property [{0}] is not empty.", property);

            return domainobject;
        }

        public static T IsEmpty<T>(this T domainobject, Func<T, IValueObject> func) where T : IDomainObject
        {
            var property = func.Invoke(domainobject);

            Assert.IsTrue(property.IsEmpty, "Value object [{0}] is not empty.", property);

            return domainobject;
        }

        public static T IsNotEmpty<T>(this T domainobject, Func<T, IDomainObject> func) where T : IDomainObject
        {
            var property = func.Invoke(domainobject);

            Assert.IsNotNull(property, "Domain object [{0}] is null.", property);
            Assert.IsFalse(property.IsEmpty, "Domain object [{0}] is empty.", property);

            return domainobject;
        }

        public static T IsNotNull<T>(this T domainobject, Func<T, object> func) where T : IDomainObject
        {
            var obj = func.Invoke(domainobject);

            Assert.IsNotNull(obj, "Domain object [{0}] is null.", obj);

            return domainobject;
        }

        public static T IsNotEmpty<T>(this T domainobject, Func<T, IEnumerable<IDomainObject>> func) where T : IDomainObject
        {
            var list = func.Invoke(domainobject);

            Assert.IsNotNull(list, "Domain collection [{0}] is null.", list);
            Assert.IsTrue(list.Any(), "Domain collection [{0}] is empty.", list);

            return domainobject;
        }

        public static T IsInitialized<T, D>(this T domainobject, Func<T, DomainCollection<D>> func)
            where T : IDomainObject
            where D : IDomainObject
        {
            var list = func.Invoke(domainobject);

            Assert.IsTrue(list.IsNullOrEmpty(), "Domain collection [{0}] is not initialised.", list);

            return domainobject;
        }

        public static T IsNotEmpty<T>(this T domainobject, Func<T, IValueObject> func) where T : IDomainObject
        {
            var property = func.Invoke(domainobject);

            Assert.IsNotNull(property, "Value object  [{0}] is null.", property);
            Assert.IsFalse(property.IsEmpty, "Value object  [{0}] is empty.", property);

            return domainobject;
        }

        public static T IsNull<T>(this T domainobject, Func<T, object> func) where T : IDomainObject
        {
            var property = func.Invoke(domainobject);

            Assert.IsNull(property, "Object is null.");

            return domainobject;
        }
        
        public static T DoesNotExist<T>(this T domainobject) where T : IDomainObject
        {
            Assert.IsTrue(domainobject.IsNullOrEmpty(), "Object [{0}] does exist", domainobject);

            return domainobject;
        }
    }
}
