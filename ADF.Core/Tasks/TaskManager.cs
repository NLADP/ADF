using System;
using Adf.Core.Objects;

namespace Adf.Core.Tasks
{
    /// <summary>
    /// Represent the methods and properties to provide information about the processes or tasks running on, as well as the general status of the task.
    /// Also provide functionality to start, end or manage any process or task.
    /// </summary>
    public static class TaskManager
    {
        private static ITaskProvider _provider;

        private static readonly object _lock = new object();

        internal static ITaskProvider Provider
        {
            get { lock (_lock) return _provider ?? (_provider = ObjectFactory.BuildUp<ITaskProvider>()); }
        }

        /// <summary>
        /// Run or execute an array of tasks.
        /// </summary>
        /// <param name="p">The array of tasks, which will be executed.</param>
        public static void Home(params object[] p)
        {
            Provider.Home(p);
        }

        /// <summary>
        /// Run or execute an array of tasks by the specified <see cref="ApplicationTask"/> name.
        /// </summary>
        /// <param name="name">The <see cref="ITask"/> that defines the start point of task.</param>
        /// <param name="p">The array of tasks, which will be executed.</param>
        public static void Run(ApplicationTask name, params object[] p)
        {
            Run(null, name, p);
        }

        /// <summary>
        /// Provides a method to run or execute one or more tasks.
        /// </summary>
        /// <param name="origin">The <see cref="ITask"/> that defines the start point of task.</param>
        /// <param name="name">The <see cref="ApplicationTask"/> that defines the name of task.</param>
        /// <param name="p">The array of tasks, which will be executed.</param>
        public static void Run(ITask origin, ApplicationTask name, params object[] p)
        {
            Provider.Run(origin, name, p);
        }

        /// <summary>
        /// Provides a method to finish one or more task, after completion of supplied finshed task.
        /// </summary>
        /// <param name="finishedtask">The task which will check for cmpletion.</param>
        /// <param name="returns">The <see cref="TaskResult"/> that defines the current status of a task.</param>
        /// <param name="p">The array of tasks, which will be executed.</param>
        public static void Return(ITask finishedtask, TaskResult returns, params object[] p)
        {
            Provider.Return(finishedtask, returns, p);
        }

        /// <summary>
        /// Find a task by a specified identifier from the <see cref="System.Collections.Hashtable"/> of tasks.
        /// </summary>
        /// <param name="id">The identifier of the task.</param>
        /// <returns>The task description if task founde in <see cref="System.Collections.Hashtable"/> of tasks; otherwise, an empty task.</returns>
        public static ITask FindTaskById(Guid id)
        {
            return Provider.FindTaskById(id);
        }
    }
}
