using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using Adf.Base.State;
using Adf.Core.Authorization;
using Adf.Core.Logging;
using Adf.Core.Objects;
using Adf.Core.State;
using Adf.Core.Tasks;

namespace Adf.Base.Tasks
{
    /// <summary>
    /// Represent the methods and properties to provide information about the processes or tasks running on, as well as the general status of the task.
    /// Also provide functionality to start, end or manage any process or task.
    /// </summary>
    public class TaskProvider : ITaskProvider
    {
        private ITask main;

        /// <summary>
        /// Gets the object builder for task handling of <see cref="System.Collections.Hashtable"/>.
        /// </summary>
        /// <returns>The expected object builder.</returns>
        private static Dictionary<Guid, ITask> tasks
        {
            get
            {
                return StateManager.Personal.GetOrCreate<Dictionary<Guid, ITask>>("TaskProvider.Tasks");
            }
        }
        /// <summary>
        /// Gets the object builder for <see cref="ITask"/> to activate the service for task handling.
        /// </summary>
        /// <returns>The activated service for task handling.</returns>
        private ITask Main
        {
            get
            {
                if (main == null)
                {
                    main = ObjectFactory.BuildUp<ITask>();
                }
                tasks[main.Id] = main;      // always set the main task in task list, tasks are stored in session
                return main;
            }
        }

        /// <summary>
        /// Provides a method to run or execute one or more tasks.
        /// </summary>
        /// <param name="origin">The <see cref="ITask"/> that defines the start point of task.</param>
        /// <param name="name">The <see cref="ApplicationTask"/> that defines the name of task.</param>
        /// <param name="p">The array of tasks, which will be executed.</param>
        public void Run(ITask origin, ApplicationTask name, params object[] p)
        {
            if (!AuthorizationManager.IsAllowed(name))
            {
                LogManager.Log("Not authorized for task " + name);
                return;
            }

            var mainType = Main.GetType();

            var typeName =
                string.Format(CultureInfo.InvariantCulture, "{0}{1}{2}Task",
                              mainType.Namespace,
                              Type.Delimiter,
                              name.Name.Replace('/', Type.Delimiter));

            var type = mainType.Assembly.GetType(typeName, true);

            object[] parms = { name, origin ?? Main };

            var task = Activator.CreateInstance(type, parms) as ITask;

            if (task == null) return;

            tasks[task.Id] = task;

            Debug.WriteLine("Starting task: " + task.Name);

            task.Run(p);
        }

        /// <summary>
        /// Provides a method to finish one or more task, after completion of supplied finshed task.
        /// </summary>
        /// <param name="finishedtask">The task which will check for cmpletion.</param>
        /// <param name="returns">The <see cref="TaskResult"/> that defines the current status of a task.</param>
        /// <param name="p">The array of tasks, which will be executed.</param>
        public void Return(ITask finishedtask, TaskResult returns, params object[] p)
        {
            if (finishedtask.Origin == null) return;

            // This only occurs when a task is started with Task.Empty as origin, that is, the starting task
            if (finishedtask.Origin == Task.Empty) finishedtask.Continue(finishedtask.Name, returns, p);

            tasks.Remove(finishedtask.Id);

            MethodInfo continuedirectmethod = finishedtask.Origin.GetType().GetMethod(string.Format("Continue{0}From{1}", returns.Name, finishedtask.Name), p.Select(param => param == null ? typeof(object) : param.GetType()).ToArray());

            if (continuedirectmethod == null)
            {
                finishedtask.Origin.Continue(finishedtask.Name, returns, p);
            }
            else
            {
                continuedirectmethod.Invoke(finishedtask.Origin, p);
            }

        }

        /// <summary>
        /// Find a task by a specified identifier from the <see cref="System.Collections.Hashtable"/> of tasks.
        /// </summary>
        /// <param name="id">The identifier of the task.</param>
        /// <returns>The task description if task founde in <see cref="System.Collections.Hashtable"/> of tasks; otherwise, an empty task.</returns>
        public ITask FindTaskById(Guid id)
        {
            ITask task;

            if (!tasks.TryGetValue(id, out task)) throw new TaskNotFoundException();

            return task ?? Task.Empty;
        }

        public void Home(params object[] p)
        {
            Main.Run(p);
        }
    }
}
