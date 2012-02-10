using System.Collections.Generic;

namespace Adf.Core.Test
{
    /// <summary>
    /// Interface that defines registering test items and asserting to them
    /// </summary>
    public interface ITestProvider
    {
        /// <summary>
        /// Register a test item.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="subject">Subject</param>
        /// <param name="action">Actions to be passed</param>
        void Register(TestItemType type, object subject, TestAction action, params object[] p);

        /// <summary>
        /// Register a test item.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="subject">Subject</param>
        void Register(TestItemType type, object subject, params object[] p);

        /// <summary>
        /// Register a test item.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="action">Test action to be passed</param>
        void Register(TestItemType type, TestAction action, params object[] p);

        /// <summary>
        /// Running a test, by checking if the requested item is in the registry.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="subject">Subject</param>
        /// <param name="action">Message to be passed</param>
        /// <returns>True if item is in registry, false if not.</returns>
        bool IsPresent(TestItemType type, object subject, TestAction action);

        /// <summary>
        /// Running a test, by checking if the requested item is in the registry.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="action">Message to be passed</param>
        /// <returns>True if item is in registry, false if not.</returns>
        bool IsPresent(TestItemType type, TestAction action);
        
        /// <summary>
        /// Running a test, by checking if the requested item is in the registry.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="subject">Subject</param>
        /// <returns>True if item is in registry, false if not.</returns>
        bool IsPresent(TestItemType type, object subject);

        /// <summary>
        /// Running a test, by checking if the requested item is not in the registry.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="action">Message to be passed</param>
        /// <returns>False if item is in registry, false if not.</returns>
        bool IsNotPresent(TestItemType type, TestAction action);

        /// <summary>
        /// Running a test, by checking if the requested item is not in the registry.
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="subject">Subject</param>
        /// <param name="action">Message to be passed</param>
        /// <returns>False if item is in registry, true if not.</returns>
        bool IsNotPresent(TestItemType type, object subject, TestAction action);

        /// <summary>
        /// Clear registry.
        /// </summary>
        void Clear();

        /// <summary>
        /// Find test items that match the criteria
        /// </summary>
        /// <param name="type">Type of test item</param>
        /// <param name="subject">Subject</param>
        /// <param name="action">Message to be passed</param>
        /// <returns>True if item is in registry, false if not.</returns>
        /// <returns>List of items matching criteria</returns>
        IEnumerable<TestItem> FindItems(TestItemType type, object subject, TestAction action);
    }
}
