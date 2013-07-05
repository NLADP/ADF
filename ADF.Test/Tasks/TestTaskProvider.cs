using System;
using Adf.Core.Tasks;
using Adf.Core.Test;

namespace Adf.Test.Tasks
{
    public class TestTaskProvider : ITaskProvider
    {
        #region Implementation of ITaskProvider

        ///<summary>
        /// Get the main task for the application.
        ///</summary>
        public ITask Main
        {
            get { throw new NotImplementedException(); }
        }

        ///<summary>
        ///  Running a task
        ///</summary>
        ///<param name="origin">Task to run the new task from.</param>
        ///<param name="name">Task to run</param>
        ///<param name="p">Optional parameters</param>
        public void Run(ITask origin, ApplicationTask name, params object[] p)
        {
            TestManager.Register(TestItemType.Task, name, TestAction.TaskStarted, p);
        }

        ///<summary>
        /// Return from executing a task
        ///</summary>
        ///<param name="finishedtask">Task that finishes</param>
        ///<param name="returns">Result from running the task. </param>
        ///<param name="p">Optional parameters.</param>
        public void Return(ITask finishedtask, TaskResult returns, params object[] p)
        {
            TestManager.Register(TestItemType.Task, finishedtask.Name, new TestAction(returns.ToString()), p);
        }

        ///<summary>
        /// Identify task by its Id.
        ///</summary>
        ///<param name="id">Valid Guid Id  for tasks. Will return Task.Empty if not found.</param>
        public ITask FindTaskById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Home(params object[] p)
        {
            TestManager.Register(TestItemType.Task, ApplicationTask.Main, TestAction.TaskStarted, p);
        }

        #endregion
    }
}
