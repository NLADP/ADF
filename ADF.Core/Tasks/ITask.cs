using System;

namespace Adf.Core.Tasks
{
    /// <summary>
    /// Represent a task, which is a unit of executable code.
    /// Perform the operations like start, run, continue, cancel and finish for tasks.
    /// </summary>
    public interface ITask
    {
        /// <summary>
        /// Get the identifier of a particular task.
        /// </summary>
        /// <returns>The identifier of a particular task.</returns>
        Guid Id { get; }

        /// <summary>
        /// Get the name of a particular task.
        /// </summary>
        /// <returns>The name of a particular task.</returns>
        ApplicationTask Name { get; }

        /// <summary>
        /// Get the start point of a particular task.
        /// </summary>
        /// <returns>The start point of a particular task.</returns>
        ITask Origin { get; }

        bool IsEmpty { get; }

        /// <summary>
        /// Provide a method to run or execute one or more tasks.
        /// </summary>
        /// <param name="p">The array of tasks, which will be executed.</param>
        void Run(params object[] p);

        /// <summary>
        /// Provide method to validate the coditions of tasks.
        /// </summary>
        /// <param name="p">The array of tasks, which will be executed.</param>
        /// <returns>True status of <see cref="TaskResult"/>.</returns>
        TaskResult ValidatePreconditions(params object[] p);

        /// <summary>
        /// Provide method to start one or more tasks.
        /// </summary>
        /// <param name="p">The array of tasks, which will be executed.</param>
        void Start(params object[] p);

        /// <summary>
        /// Provide a method to continue one or more tasks after completion of specified task.
        /// </summary>
        /// <param name="finishedtask">The task which will check for cmpletion.</param>
        /// <param name="returns">The <see cref="TaskResult"/> that defines the current status of a task.</param>
        /// <param name="p">The array of tasks, which will be executed.</param>
        void Continue(ApplicationTask finishedtask, TaskResult returns, params object[] p);

        /// <summary>
        /// Provide a method to finish one or more tasks with success.
        /// </summary>
        /// <param name="p">The array of tasks, which will be executed.</param>
        void OK(params object[] p);

        /// <summary>
        /// Provides the method to cancel one or more tasks.
        /// </summary>
        /// <param name="p">The array of tasks, which will be executed.</param>
        void Cancel(params object[] p);

        /// <summary>
        /// Provides the method to finish one or more tasks.
        /// </summary>
        /// <param name="returntype">The <see cref="TaskResult"/> that defines the current status of a task.</param>
        /// <param name="p">The array of tasks, which will be executed.</param>
        void Finish(TaskResult returntype, params object[] p);
    }
}
