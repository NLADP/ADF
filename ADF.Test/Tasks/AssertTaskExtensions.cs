using System;
using System.Linq;
using Adf.Core.Domain;
using Adf.Core.Tasks;
using Adf.Core.Test;
using Adf.Core.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adf.Test.Tasks
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

        public static R GetTaskStartedParameter<R>(this ITask task, ApplicationTask startedTask, int index)
        {
            var item = TestManager.FindItems(TestItemType.Task, startedTask, TestAction.TaskStarted).FirstOrDefault();

            Assert.IsNotNull(item, "Task {0} was not started", startedTask);

            if (item.Parameters == null) return default(R);
            if (item.Parameters.Count <= index) return default(R);

            return (R)item.Parameters[index];
        }

        public static T OtherTaskIsNotStarted<T>(this T task, ApplicationTask startedtask) where T : ITask
        {
            TestManager.IsNotPresent(TestItemType.Task, startedtask, TestAction.TaskStarted);
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

        public static T ValidationSucceeded<T>(this T task, string message = null) where T : ITask
        {
            ValidationManager.Handle();

//            Assert.IsTrue(ValidationManager.IsSucceeded, ValidationManager.ValidationResults.ConvertToString(", "));

            // test if ValidationFailed does not exist instead of querying for ValidationSucceeded. Could have several results.
            TestManager.IsNotPresent(TestItemType.ValidationResult, TestAction.ValidationFailed);

            if (message != null)
            {
                Assert.IsTrue(
                    TestManager.FindItems(TestItemType.ValidationResult, TestAction.ValidationSucceeded)
                        .Any(item => ((ValidationResultCollection) item.Subject)
                                         .Any(r => r.Title.Replace("?", "") == message)));
            }

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

            Assert.IsNotNull(item.Parameters, "Parameter list is null");
           Assert.IsTrue(item.Parameters.Count > index, "Required index [{0}] is out of bounds of parameter list.", index);
            Assert.IsInstanceOfType(item.Parameters[index], type, "Parameter [{0}] is not of the required type [{1}]", item.Parameters[index], type);

            return task;
        }

        public static T ReturnsAt<T>(this T task, object returns, int index) where T : ITask
        {
            var item = TestManager.FindItems(TestItemType.Task, task.Name, new TestAction(TaskResult.Ok.ToString())).FirstOrDefault();

            if (item != null)
            {
                Assert.IsNotNull(item.Parameters, "Parameter list is null");
                Assert.IsTrue(item.Parameters.Count > index, "Required index [{0}] is out of bounds of parameter list.", index);
                Assert.IsTrue(item.Parameters[index].Equals(returns), "Parameter [{0}] is not of the required type [{1}]", item.Parameters[index], returns);
            }

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

            Assert.IsNotNull(item, "No return items");

            if (item.Parameters == null) return default(R);
            if (item.Parameters.Count <= index) return default(R);

            return (R) item.Parameters[index];
        }
    }
}
