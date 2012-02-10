using System;
using Adf.Core.Authorization;

namespace Adf.Core.Tasks
{
    /// <summary>
    /// Provides some predefined tasks (Login, Logout, Help etc), which may be used for a newly created application.
    /// </summary>
    [Serializable]
    public class ApplicationTask : Descriptor, IAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationTask"/> class with the specified task name.
        /// </summary>
        /// <param name="name">The <see cref="System.String"/> that will set as name of a task.</param>
        public ApplicationTask(string name) : base(name)
        {
        }

        private static readonly ApplicationTask main = new ApplicationTask("Main");
        /// <summary>
        /// Provides a task, which describes the functionality of Main menu for a newly created application.
        /// </summary>
        public static ApplicationTask Main { get { return main; } } 
    }
}