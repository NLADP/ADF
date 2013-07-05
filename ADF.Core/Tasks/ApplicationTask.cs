using System;
using Adf.Core.Authorization;
using Adf.Core.Resources;
using Adf.Core.Extensions;

namespace Adf.Core.Tasks
{
    /// <summary>
    /// Provides some predefined tasks (Login, Logout, Help etc), which may be used for a newly created application.
    /// </summary>
    [Serializable]
    public class ApplicationTask : Descriptor, IAction
    {
        public Type Subject { get; protected set; }
        public string Label { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationTask"/> class with the specified task name.
        /// </summary>
        /// <param name="name">The <see cref="System.String"/> that will set as name of a task.</param>
        /// <param name="label">A label that can be used to show the tasks's name on the view. If it's empty than name is used. </param>
        public ApplicationTask(string name, string label = null) : base(name)
        {
            if (label.IsNullOrEmpty()) label = ResourceManager.GetString(name);

            Label = label.IsNullOrEmpty() ? name : label;
        }

        private static readonly ApplicationTask main = new ApplicationTask("Main");
        /// <summary>
        /// Provides a task, which describes the functionality of Main menu for a newly created application.
        /// </summary>
        public static ApplicationTask Main { get { return main; } } 
    }
}