using System;
using Adf.Base.Domain;
using Adf.Core.Domain;
using Adf.Core.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adf.Test.Domain
{
    public static class AssertDomainExtensions
    {
        public static T IsIn<T>(this T target, DomainCollection<T> collection) where T : class, IDomainObject
        {
            Assert.IsNotNull(target, "Domain object [{0}] is null, but should have been an element in the collection.", target);
            Assert.IsNotNull(collection, "Domain collection is null, but should contain element [{0}].", target);
            Assert.IsTrue(collection.Contains(target), "Domain collection does not contain element [{0}], but should have.", target);

            return target;
        }

        public static T IsNotIn<T>(this T target, DomainCollection<T> collection) where T : class, IDomainObject
        {
            if (target == null || collection == null) return null;

            Assert.IsFalse(collection.Contains(target), "Domain collection contains element [{0}], but should not have.", target);

            return target;
        }

        public static T IsValid<T>(this T target) where T : IDomainObject
        {
            Assert.IsNotNull(target, "Domain object [{0}] is null, but should have existed and been filled.", target);
            Assert.IsFalse(target.IsEmpty, "Domain object [{0}] is empty, but should have been filled.", target);

            return target;
        }

        public static DomainCollection<T> IsInitialised<T>(this DomainCollection<T> target) where T : class, IDomainObject
        {
            Assert.IsNotNull(target, "Domain collection [{0}] is null, but should have been initialised.", target);
            Assert.IsTrue(target.IsInitialised, "Domain collection [{0}] is not initialised, but should have been.", target);

            return target;
        }
        
        public static DomainCollection<T> IsNotInitialised<T>(this DomainCollection<T> target) where T : class, IDomainObject
        {
            if (target == null) return null;
                
            Assert.IsFalse(target.IsInitialised, "Domain collection [{0}] is initialised, but should not have been.", target);

            return target;
        }

        public static DomainCollection<T> HasCount<T>(this DomainCollection<T> target, int count) where T : class, IDomainObject
        {
            Assert.IsNotNull(target, "Domain collection is null, but should contain {0} elements.", count);
            Assert.IsTrue(target.Count == count, "Domain collection has {0} elements, but should have had {1} elements.", target.Count, count);

            return target;
        }
    }
}
