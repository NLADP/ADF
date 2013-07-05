using System;

namespace Adf.Core.Tasks
{
    ///<summary>
    /// Interfaces that defines how tasks are executed. In general, tasks will be individual smart use cases, but can also be activities in a workflow oriented system.
    ///</summary>
    public interface ITaskProvider
    {
        ///<summary>
        ///  Running a task
        ///</summary>
        ///<param name="origin">Task to run the new task from.</param>
        ///<param name="name">Task to run</param>
        ///<param name="p">Optional parameters</param>
        void Run(ITask origin, ApplicationTask name, params object[] p);

        ///<summary>
        /// Return from executing a task
        ///</summary>
        ///<param name="finishedtask">Task that finishes</param>
        ///<param name="returns">Result from running the task. </param>
        ///<param name="p">Optional parameters.</param>
        void Return(ITask finishedtask, TaskResult returns, params object[] p);

        ///<summary>
        /// Identify task by its Id.
        ///</summary>
        ///<param name="id">Valid Guid Id  for tasks. Will return Task.Empty if not found.</param>
        ITask FindTaskById(Guid id);

        void Home(params object[] p);
    }
}
