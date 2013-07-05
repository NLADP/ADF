using System;
using System.Collections.Generic;
using System.Linq;
using Adf.Core.Test;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Adf.Test
{
    public class TestProvider : ITestProvider 
    {
        private List<TestItem> _items = new List<TestItem>();

        #region Implementation of ITestProvider

        /// <summary>
        /// Register a test item.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="subject">Subject</param>
        /// <param name="action">Action to be passed</param>
        /// <param name="p">Parameters passed from task</param>
        public virtual void Register(TestItemType type, object subject, TestAction action, params object[] p)
        {
           _items.Add(new TestItem {Type = type, Subject = subject, Action = action, Parameters = p});
        }

        /// <summary>
        /// Register a test item.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="subject">Subject</param>
        /// <param name="p">Parameters passed from task</param>
        public virtual void Register(TestItemType type, object subject, params object[] p)
        {
            Register(type, subject, new TestAction(string.Empty), p);
        }

        /// <summary>
        /// Register a test item.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="action">Action to be passed</param>
        /// <param name="p">Parameters passed from task</param>
        public virtual void Register(TestItemType type, TestAction action, params object[] p)
        {
            Register(type, new object(), action, p);
        }

        /// <summary>
        /// Running a test, by checking if the requested item is in the registry.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="subject">Subject</param>
        /// <param name="action">Action to be passed</param>
        /// <returns>True if item is in registry, false if not.</returns>
        public virtual bool IsPresent(TestItemType type, object subject, TestAction action)
        {
            var present = _items
                .Where(item => item.Type == type)
                .Where(item => item.Subject == subject)
                .Where(item => item.Action == action)
                .Count() > 0;

            var result = String.Format("[{0}] {1} : {2}", type, subject, action);
            
            Assert.IsTrue(present, result);

            return present;
        }

        /// <summary>
        /// Running a test, by checking if the requested item is in the registry.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="action">Action to be passed</param>
        /// <returns>True if item is in registry, false if not.</returns>
        public virtual bool IsPresent(TestItemType type, TestAction action)
        {
            bool present = IsActionPresent(type, action);

            var result = String.Format("[{0}] {1}", type, action);

            Assert.IsTrue(present, result);

            return present;
        }

        private bool IsActionPresent(TestItemType type, TestAction action)
        {
            return _items
                       .Where(item => item.Type == type)
                       .Where(item => item.Action == action)
                       .Count() > 0;
        }

        /// <summary>
        /// Running a test, by checking if the requested item is in the registry.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="subject">Subject</param>
        /// <returns>True if item is in registry, false if not.</returns>
        public virtual bool IsPresent(TestItemType type, object subject)
        {
            var present = _items
                .Where(item => item.Type == type)
                .Where(item => item.Subject == subject)
                .Count() > 0;

            var result = String.Format("[{0}] {1}", type, subject);

            Assert.IsTrue(present, result);

            return present;
        }

        public virtual bool IsNotPresent(TestItemType type, TestAction action)
        {
            bool present = IsActionPresent(type, action);

            var result = String.Format("[{0}] {1}", type, action);

            Assert.IsFalse(present, result);

            return !present;
        }

        /// <summary>
        /// Running a test, by checking if the requested item is not in the registry.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="subject">Subject</param>
        /// <param name="action">Action to be passed</param>
        /// <returns>False if item is in registry, true if not.</returns>
        public virtual bool IsNotPresent(TestItemType type, object subject, TestAction action)
        {
            var present = _items
                .Where(item => item.Type == type)
                .Where(item => item.Subject == subject)
                .Where(item => item.Action == action)
                .Count() > 0;

            var result = String.Format("[{0}] {1} : {2}", type, subject, action);

            Assert.IsFalse(present, result);

            return present;
        }

        /// <summary>
        /// Clear registry.
        /// </summary>
        public virtual void Clear()
        {
            _items.Clear();
        }

        public virtual IEnumerable<TestItem> FindItems(TestItemType type, object subject, TestAction action)
        {
            return _items
               .Where(item => item.Type == type)
               .Where(item => item.Subject == subject)
               .Where(item => item.Action == action);
        }

        public virtual IEnumerable<TestItem> FindItems(TestItemType type, TestAction action)
        {
            return _items
               .Where(item => item.Type == type)
               .Where(item => item.Action == action);
        }

        #endregion
    }
}
