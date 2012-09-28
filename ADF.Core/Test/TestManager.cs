using System.Collections.Generic;
using Adf.Core.Objects;

namespace Adf.Core.Test
{
    public static class TestManager
    {
        private static ITestProvider _provider;
        private static readonly object _lock = new object();

        internal static ITestProvider Provider
        {
            get { lock (_lock) return _provider ?? (_provider = ObjectFactory.BuildUp<ITestProvider>()); }
        }
        #region Implementation of ITestProvider

        /// <summary>
        /// Register a test item.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="subject">Subject</param>
        /// <param name="action">Message to be passed</param>
        public static void Register(TestItemType type, object subject, TestAction action, params object[] p)
        {
            Provider.Register(type, subject, action, p);
        }

        /// <summary>
        /// Register a test item.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="subject">Subject</param>
        public static void Register(TestItemType type, object subject, params object[] p)
        {
            Provider.Register(type, subject, p);
        }

        /// <summary>
        /// Register a test item.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="action">Message to be passed</param>
        public static void Register(TestItemType type, TestAction action, params object[] p)
        {
            Provider.Register(type, action, p);
        }

        /// <summary>
        /// Running a test, by checking if the requested item is in the registry.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="subject">Subject</param>
        /// <param name="action">Action to be passed</param>
        /// <returns>True if item is in registry, false if not.</returns>
        public static bool IsPresent(TestItemType type, object subject, TestAction action)
        {
            return Provider.IsPresent(type, subject, action);
        }

         /// <summary>
        /// Running a test, by checking if the requested item is in the registry.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="action">Message to be passed</param>
        /// <returns>True if item is in registry, false if not.</returns>
        public static bool IsPresent(TestItemType type, TestAction action)
        {
            return Provider.IsPresent(type, action);
        }
        
        /// <summary>
        /// Running a test, by checking if the requested item is in the registry.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="subject">Message to be passed</param>
        /// <returns>True if item is in registry, false if not.</returns>
        public static bool IsPresent(TestItemType type, object subject)
        {
            return Provider.IsPresent(type, subject);
        }

        /// <summary>
        /// Running a test, by checking if the requested item is not in the registry.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="action">Message to be passed</param>
        /// <returns>True if item is in registry, false if not.</returns>
        public static bool IsNotPresent(TestItemType type, TestAction action)
        {
            return Provider.IsNotPresent(type, action);
        }

        /// <summary>
        /// Running a test, by checking if the requested item is not in the registry.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="subject">Subject</param>
        /// <param name="action">Action to be passed</param>
        /// <returns>False if item is in registry, true if not.</returns>
        public static bool IsNotPresent(TestItemType type, object subject, TestAction action)
        {
            return Provider.IsNotPresent(type, subject, action);
        }

        /// <summary>
        /// Clear registry.
        /// </summary>
        public static void Clear()
        {
            Provider.Clear();
        }

        #endregion

        public static IEnumerable<TestItem> FindItems(TestItemType type, object subject, TestAction action)
        {
            return Provider.FindItems(type, subject, action);
        }

        public static IEnumerable<TestItem> FindItems(TestItemType type, TestAction action)
        {
            return Provider.FindItems(type, action);
        }
    }
}
