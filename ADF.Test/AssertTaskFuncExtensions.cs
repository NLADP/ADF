using System;
using System.Collections.Generic;
using System.Linq;
using Adf.Base.Domain;
using Adf.Core.Domain;
using Adf.Core.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adf.Test
{
    public static class AssertTaskFuncExtensions
    {
        public static T IsSet<T>(this T task, Func<T, object> func, object origin) where T : ITask
        {
            var property = func.Invoke(task);

            Assert.AreEqual(property, origin, "Property [{0}] is not equal to [{1}] .", property.ToString(), origin.ToString());

            return task;
        }
        
        public static T IsElement<T, D>(this T task, Func<T, DomainCollection<D>> func, IDomainObject target) where D : DomainObject where T : ITask
        {
            var collection = func.Invoke(task);

            Assert.IsTrue(collection.Contains((D) target), "Domain object [{0}] is not an element in the collection.", target.Title);

            return task;
        }

        public static T IsEmpty<T>(this T task, Func<T, IDomainObject> func) where T : ITask
        {
            var property = func.Invoke(task);

            Assert.IsTrue(property.IsEmpty, "Domain object property [{0}] is not empty.", property);

            return task;
        }

        public static T IsEmpty<T>(this T task, Func<T, IValueObject> func) where T : ITask
        {
            var property = func.Invoke(task);

            Assert.IsTrue(property.IsEmpty, "Value object [{0}] is not empty.", property);

            return task;
        }

        public static T IsNotEmpty<T>(this T task, Func<T, IDomainObject> func) where T : ITask
        {
            var property = func.Invoke(task);

            Assert.IsNotNull(property, "Domain object [{0}] is null.", property);
            Assert.IsFalse(property.IsEmpty, "Domain object [{0}] is empty.", property);

            return task;
        }

        public static T IsNotNull<T>(this T task, Func<T, object> func) where T : ITask
        {
            var obj = func.Invoke(task);

            Assert.IsNotNull(obj, "Domain object [{0}] is null.", obj);

            return task;
        }

        public static T IsNotEmpty<T>(this T task, Func<T, IEnumerable<IDomainObject>> func) where T : ITask
        {
            var list = func.Invoke(task);

            Assert.IsNotNull(list, "Domain collection [{0}] is null.", list);
            Assert.IsTrue(list.Count() > 0, "Domain collection [{0}] is empty.", list);

            return task;
        }

        public static T IsNotEmpty<T>(this T task, Func<T, IValueObject> func) where T : ITask
        {
            var property = func.Invoke(task);

            Assert.IsNotNull(property, "Value object  [{0}] is null.", property);
            Assert.IsFalse(property.IsEmpty, "Value object  [{0}] is empty.", property);

            return task;
        }

        public static T IsNull<T>(this T task, Func<T, object> func) where T : ITask
        {
            var property = func.Invoke(task);

            Assert.IsNull(property, "Object is null.");

            return task;
        }

        public static T IsInitialized<T, D>(this T task, Func<T, DomainCollection<D>> func) where T : ITask where D : IDomainObject
        {
            var list = func.Invoke(task);

            Assert.IsTrue(list.IsNullOrEmpty(), "Domain collection [{0}] is not initialised.", list);

            return task;
        }

        public static T HasCount<T, D>(this T task, Func<T, DomainCollection<D>> func, int count) where T : ITask where D : IDomainObject
        {
            var list = func.Invoke(task);

            Assert.IsNotNull(list, "Domain collection [{0}] is null.", list);
            Assert.IsTrue(list.Count == count, "Domain collection [{0}] has {1} elements, but {2} elements were expected.", list, list.Count, count);

            return task;
        }

        public static T IsReturnSet<T, D>(this T task, Func<D, object> func, int index, object origin) where T : ITask
        {
            D returns = task.GetReturnParameter<D>(index);
            var property = func.Invoke(returns);

            Assert.AreEqual(property, origin, "Property [{0}] is not equal to [{1}] .", property.ToString(), origin.ToString());

            return task;
        }
    }
}
