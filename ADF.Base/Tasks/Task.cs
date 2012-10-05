using System;
using System.Linq;
using System.Reflection;
using Adf.Core;
using Adf.Core.Logging;
using Adf.Core.Query;
using Adf.Core.Tasks;

namespace Adf.Base.Tasks
{
    /// <summary>
    /// Represents specific piece of work that is undertaken or attempted
    /// Provides the methods and properties to execute task, manage task view and complete task.
    /// </summary>
    [Serializable]
    public class Task : ITask
    {
        #region Empty

        private static readonly Task empty = new Task(Descriptor.Null as ApplicationTask, null);

        /// <summary>
        /// Initializes a new object of the <see cref="Task"/> class with empty value.
        /// </summary>
        public static Task Empty
        {
            get { return empty; }
        }

        /// <summary>
        /// Gets the starting point of a task is assigned or not.
        /// </summary>
        /// <returns>True if the starting point of a task is not assigned; otherwise, false.</returns>
        public bool IsEmpty
        {
            get { return (origin == null); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Task"/> class with the specified predefined task name of <see cref="ApplicationTask"/> class object.
        /// </summary>
        /// <param name="name">The <see cref="ApplicationTask"/> that defines the task name whose value will set.</param>
        public Task(ApplicationTask name)
        {
            origin = Empty;
            this.name = name;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Task"/> class with the specified predefined task name of <see cref="ApplicationTask"/> and starting point of task.
        /// </summary>
        /// <param name="name">The <see cref="ApplicationTask"/> that defines the task name whose value will set.</param>
        /// <param name="origin">The <see cref="ITask"/> that defines the start point of task.</param>
        public Task(ApplicationTask name, ITask origin)
        {
            id = Guid.NewGuid();
            this.name = name;
            this.origin = origin;
        }

        #endregion

        #region Internals

        private readonly Guid id;

        /// <summary>
        /// Gets the identifier value of a task.
        /// </summary>
        /// <returns>The identifier value of a task.</returns>
        public Guid Id
        {
            get { return id; }
        }

        private ApplicationTask name = Descriptor.Null as ApplicationTask;

        /// <summary>
        /// Gets or sets the task name.
        /// </summary>
        /// <returns>The predefined task name of <see cref="ApplicationTask"/> class.</returns>
        public ApplicationTask Name
        {
            get { return name; }
            protected set { name = value; }
        }

        private readonly ITask origin;

        /// <summary>
        /// Gets the start point of a particular task.
        /// </summary>
        /// <returns>The start point of a particular task.</returns>
        public ITask Origin
        {
            get { return origin; }
        }

        #endregion


        #region Running Task

        /// <summary>
        /// Provides a method to run or execute one or more tasks.
        /// </summary>
        /// <param name="p">The array of tasks, which will be executed.</param>
        public void Run(params object[] p)
        {
            TaskResult returns = ValidatePreconditions(p);

            if (returns == TaskResult.ValidateTrue)
            {
                Type[] types = p.Select(param => param == null ? typeof(object) : param.GetType()).ToArray();
                MethodInfo method = GetType().GetMethod("Init", types);

                if (method == null && GetType().GetMethods().Any(m => m.Name == "Init"))
                    throw new InvalidOperationException(string.Format("Could not find any matching Init method on {0} with parameter types {1}",
                                                                      GetType().Name,
                                                                      string.Join(",", types.Select(t => t.Name))));

                using (new TracingScope("Start " + GetType().Name))
                {
                    if (method == null) { Start(p); } else { method.Invoke(this, p); }
                }
            }
        }

        /// <summary>
        /// Provide a method to continue one or more tasks after completion of specified task.
        /// </summary>
        /// <param name="finishedtask">The task which will check for cmpletion.</param>
        /// <param name="returns">The <see cref="TaskResult"/> that defines the current status of a task.</param>
        /// <param name="p">The array of tasks, which will be executed.</param>
        public virtual void Continue(ApplicationTask finishedtask, TaskResult returns, params object[] p)
        {
            Finish(returns, p);
        }

        /// <summary>
        /// Provide method to validate the coditions of tasks.
        /// </summary>
        /// <param name="p">The array of tasks, which will be executed.</param>
        /// <returns>True status of <see cref="TaskResult"/>.</returns>
        public virtual TaskResult ValidatePreconditions(params object[] p)
        {
            return TaskResult.ValidateTrue;
        }

        /// <summary>
        /// Provides a method to start one or more tasks.
        /// </summary>
        /// <param name="p">The array of tasks, which will be executed.</param>
        public virtual void Start(params object[] p)
        {
            LogManager.Log("Adf.Task.NoStartOrInit", null, Name);

            Cancel();
        }

        #endregion

        #region Finishing Task

        /// <summary>
        /// Provide a method to finish one or more tasks with success by the value of <see cref="TaskResult"/>.
        /// </summary>
        /// <param name="p">The array of tasks, which will be executed.</param>
        public virtual void OK(params object[] p)
        {
            Finish(TaskResult.Ok, p);
        }

        /// <summary>
        /// Provides the method to cancel one or more tasks by the value of <see cref="TaskResult"/>.
        /// </summary>
        /// <param name="p">The array of tasks, which will be executed.</param>
        public virtual void Cancel(params object[] p)
        {
            Finish(TaskResult.Cancel, p);
        }

        /// <summary>
        /// Provides the method to cancel one or more tasks by the value of <see cref="TaskResult"/>.
        /// </summary>
        /// <param name="p">The array of tasks, which will be executed.</param>
        public virtual void Error(params object[] p)
        {
            Finish(TaskResult.Error, p);
        }

        /// <summary>
        /// Provides the method to finish one or more tasks. Also deactivate the task view of <see cref="Task"/> class object.
        /// </summary>
        /// <param name="returntype">The <see cref="TaskResult"/> that defines the current status of a task.</param>
        /// <param name="p">The array of tasks, which will be executed.</param>
        public void Finish(TaskResult returntype, params object[] p)
        {
            this.DeactivateView();

            TaskManager.Return(this, returntype, p);
        }

        #endregion
    }
}
