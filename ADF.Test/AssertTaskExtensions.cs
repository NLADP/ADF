using System;
using System.Linq;
using Adf.Base.Domain;
using Adf.Core;
using Adf.Core.Domain;
using Adf.Core.Tasks;
using Adf.Core.Test;
using Adf.Core.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adf.Test
{
    public static class AssertTaskExtensions
    {
        public static T ViewIsActivated<T>(this T task) where T : ITask
        {
            TestManager.IsPresent(TestItemType.View, task.Name, TestAction.ViewActivated);

            return task;
        }

        public static T OtherViewIsActivated<T>(this T task, ApplicationTask othertask) where T : ITask
        {
            TestManager.IsPresent(TestItemType.View, othertask.Name, TestAction.ViewActivated);

            return task;
        }

        public static T ViewIsDeactivated<T>(this T task) where T : ITask
        {
            TestManager.IsPresent(TestItemType.View, task.Name, TestAction.ViewDeactivated);

            return task;
        }

        public static T OtherViewIsDeactivated<T>(this T task, ApplicationTask othertask) where T : ITask
        {
            TestManager.IsPresent(TestItemType.View, othertask.Name, TestAction.ViewDeactivated);

            return task;
        }

        public static T OtherTaskIsStarted<T>(this T task, ApplicationTask startedtask) where T : ITask
        {
            TestManager.IsPresent(TestItemType.Task, startedtask, TestAction.TaskStarted);

            return task;
        }

        public static T OtherTaskIsStarted<T>(this T task, ApplicationTask startedtask, params object[] p) where T : ITask
        {
            var item =
                TestManager.FindItems(TestItemType.Task, startedtask, TestAction.TaskStarted)
                    .FirstOrDefault();

            Assert.IsNotNull(item, "Task {0} was not started", startedtask);
            CollectionAssert.AreEqual(p, item.Parameters, "Parameters are not equal.");

            return task;
        }

        public static T IsCancelled<T>(this T task) where T : ITask
        {
            TestManager.IsPresent(TestItemType.Task, task.Name, new TestAction(TaskResult.Cancel.ToString()));

            return task;
        }

        public static T IsOk<T>(this T task) where T : ITask
        {
            TestManager.IsPresent(TestItemType.Task, task.Name, new TestAction(TaskResult.Ok.ToString()));

            return task;
        }

        public static T ValidationFailed<T>(this T task) where T : ITask
        {
            ValidationManager.Handle();

            TestManager.IsPresent(TestItemType.ValidationResult, TestAction.ValidationFailed);
            //Assert.IsFalse(ValidationManager.IsSucceeded, ValidationManager.ValidationResults.ConvertToString(", "));

            return task;
        }

        public static T ValidationSucceeded<T>(this T task) where T : ITask
        {
            ValidationManager.Handle();

//            Assert.IsTrue(ValidationManager.IsSucceeded, ValidationManager.ValidationResults.ConvertToString(", "));

            // test if ValidationFailed does not exist instead of querying for ValidationSucceeded. Could have several results.
            TestManager.IsNotPresent(TestItemType.ValidationResult, TestAction.ValidationFailed);

            return task;
        }

//        [Obsolete("Use IsEmpty<T>(this T task, Func<T, IDomainObject> func) instead.")]
        public static T IsEmpty<T>(this T task, IDomainObject domainObject) where T : ITask
        {
            Assert.IsTrue(domainObject.IsEmpty, "Domain object property [{0}] is not empty.", domainObject);

            return task;
        }

//        [Obsolete("Use IsNotEmpty<T>(this T task, Func<T, IDomainObject> func) instead.")]
        public static T IsNotEmpty<T>(this T task, IDomainObject domainObject) where T : ITask
        {
            Assert.IsNotNull(domainObject, "Domain object [{0}] is null.", domainObject);
            Assert.IsFalse(domainObject.IsEmpty, "Domain object [{0}] is empty.", domainObject);

            return task;
        }

        /// <summary>
        /// Check whether a use case returns a valid object as part of its post-conditions.
        /// </summary>
        /// <typeparam name="T">Type of the object passed back</typeparam>
        /// <param name="task">Task to check return for</param>
        /// <param name="type"></param>
        /// <param name="index">Index in parameters passed back from use case</param>
        /// <returns></returns>
        public static T ReturnsValid<T>(this T task, Type type, int index) where T : ITask
        {
            var item = TestManager.FindItems(TestItemType.Task, task.Name, new TestAction(TaskResult.Ok.ToString())).FirstOrDefault();

            Assert.IsNotNull(item, "Requested item cannot be found");
            Assert.IsNotNull(item.Parameters, "Parameter list is null");
            Assert.IsTrue(item.Parameters.Count > index, "Required index [{}] is out of bounds of parameter list.", index);
            Assert.IsInstanceOfType(item.Parameters[index], type, "Parameter [{0}] is not of the required type [{1}]", item.Parameters[index], type);

            return task;
        }

        /// <summary>
        /// Check whether a use case returns a valid object as part of its post-conditions.
        /// </summary>
        /// <typeparam name="T">Type of the object passed back</typeparam>
        /// <param name="task">Task to check return for</param>
        /// <param name="type"></param>
        /// <param name="index">Index in parameters passed back from use case</param>
        /// <param name="expectedCount">The expected number of items in the collection</param>
        /// <returns></returns>
        public static T ReturnsCollectionOfLength<T>(this T task, Type type, int index, int expectedCount) where T : ITask
        {
            var item = TestManager.FindItems(TestItemType.Task, task.Name, new TestAction(TaskResult.Ok.ToString())).FirstOrDefault();

            Assert.IsNotNull(item, "Requested item cannot be found");
            Assert.IsNotNull(item.Parameters, "Parameter list is null");
            Assert.IsTrue(item.Parameters.Count > index, "Required index [{}] is out of bounds of parameter list.", index);

            Type generic = typeof(DomainCollection<>);
            Type domainCollection = generic.MakeGenericType(type);
            Assert.IsInstanceOfType(item.Parameters[index], domainCollection, "Parameter [{0}] is not of the required type [{1}]", item.Parameters[index], domainCollection);

            Assert.AreEqual(expectedCount, ((IDomainCollection) item.Parameters[index]).Count, "The number of items in the collections does not match the expected value");

            return task;
        }

        /// <summary>
        /// Check whether a use case returns a valid object as part of its post-conditions.
        /// </summary>
        /// <param name="task">Task to check return for</param>
        /// <param name="index">Index in parameters passed back from use case</param>
        /// <returns></returns>
        public static R GetReturnParameter<R>(this ITask task, int index)
        {
            var item = TestManager.FindItems(TestItemType.Task, task.Name, new TestAction(TaskResult.Ok.ToString())).FirstOrDefault();

            if (item == null || item.Parameters == null) return default(R);
            if (item.Parameters.Count <= index) return default(R);

            return (R) item.Parameters[index];
        }

        public static T ValidationFailedWith<T>(this T task, ValidationResultSeverity severity, string code) where T : ITask
        {
            var items = TestManager.FindItems<ValidationResultCollection>(TestItemType.ValidationResult, TestAction.ValidationFailed);
            var result = items.Select(item => (ValidationResultCollection) item.Subject).Any(rc => rc.Any(r => r.Severity == severity && r.Message.IndexOf(code, StringComparison.OrdinalIgnoreCase) != -1));

            if (!result)
            {
                Assert.Fail(severity > ValidationResultSeverity.Informational ? "Validation did not fail with the expected code '{0}'" : "Validation did not succeeed with the expected code '{0}'", code);
            }

            return task;
        }

        public static T ValidationFailedWithError<T>(this T task, string code) where T : ITask
        {
            return ValidationFailedWith(task, ValidationResultSeverity.Error, code);
        }
        
        public static T ValidationSucceededWithInfo<T>(this T task, string code) where T : ITask
        {
            return ValidationFailedWith(task, ValidationResultSeverity.Informational, code);
        }
        
        public static T ValidationFailedWithWarning<T>(this T task, string code) where T : ITask
        {
            return ValidationFailedWith(task, ValidationResultSeverity.Warning, code);
        }

        public static T ValidationFailedWith<T>(this T task, ValidationResultSeverity severity, Descriptor code) where T : ITask
        {
            return ValidationFailedWith(task, severity, code.Name);
        }
        
        public static T ValidationFailedWithError<T>(this T task, Descriptor code) where T : ITask
        {
            return ValidationFailedWith(task, ValidationResultSeverity.Error, code);
        }
        
        public static T ValidationSucceededWithInfo<T>(this T task, Descriptor code) where T : ITask
        {
            return ValidationFailedWith(task, ValidationResultSeverity.Informational, code);
        }
        
        public static T ValidationFailedWithWarning<T>(this T task, Descriptor code) where T : ITask
        {
            return ValidationFailedWith(task, ValidationResultSeverity.Warning, code);
        }
    }
}
