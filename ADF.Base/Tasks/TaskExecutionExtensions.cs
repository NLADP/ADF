using System;
using Adf.Core.Tasks;
using Adf.Core.Validation;

namespace Adf.Base.Tasks
{
    public static class TaskExecutionExtensions
    {
        public static bool Execute(this ITask task, params Action[] actions)
        {
            return ValidationManager.Execute(actions);
        }

        public static bool Execute(this ITask task, bool handleErrors, params Action[] actions)
        {
            return ValidationManager.Execute(handleErrors, actions);
        }

        public static void ExecuteAndOk(this ITask task, params Action[] actions)
        {
            if (task.Execute(actions)) task.OK();
        }

        public static void ExecuteAndActivate(this ITask task, params Action[] actions)
        {
            if (task.Execute(actions)) task.ActivateView();
        }

        public static void ExecuteAndCancel(this ITask task, params Action[] actions)
        {
            if (task.Execute(actions)) task.Cancel();
        }

        public static void ExecuteAndRun(this ITask task, ApplicationTask name, params Action[] actions)
        {
            if (task.Execute(actions)) task.RunTask(name);
        }

        /// <summary>
        /// Provides a method to run or execute one or more tasks.
        /// </summary>
        /// <param name="origin">The <see cref="ITask"/> that defines the start point of task.</param>
        /// <param name="name">The <see cref="ApplicationTask"/> that defines the name of task.</param>
        /// <param name="p">The array of tasks, which will be executed.</param>
        public static void RunTask(this ITask origin, ApplicationTask name, params object[] p)
        {
            TaskManager.Run(origin, name, p);
        }

        public static void Validate(this ITask task)
        {
            ValidationManager.Validate(task);
        }
    }
}
