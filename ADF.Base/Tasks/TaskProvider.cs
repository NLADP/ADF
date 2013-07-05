using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Adf.Core.Authorization;
using Adf.Core.Extensions;
using Adf.Core.Logging;
using Adf.Core.Objects;
using Adf.Core.Query;
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
        private ITask _main;
        private Dictionary<string, Type> _alltasks;
 
        private ITask Get(ApplicationTask name, ITask origin)
        {
            var taskname = string.Format("{0}Task", name);
            var type = AllTasks[taskname];

            object[] parms = { name, origin ?? Main };

            return type.New<ITask>(parms);
        }

        private static Dictionary<Guid, ITask> Tasks
        {
            get { return StateManager.Personal.GetOrCreate<Dictionary<Guid, ITask>>("TaskProvider.Tasks"); }
        }

        /// <summary>
        /// Gets the object builder for <see cref="ITask"/> to activate the service for task handling.
        /// </summary>
        /// <returns>The activated service for task handling.</returns>
        private ITask Main
        {
            get
            {
                if (_main == null)
                {
                    _main = ObjectFactory.BuildUp<ITask>();
                    Tasks[_main.Id] = _main;
                }
                return _main;
            }
        }

        private Dictionary<string, Type> AllTasks
        {
            get
            {
                if (_alltasks == null)
                {
                    _alltasks = new Dictionary<string, Type>();
                    var tasktype = typeof (ITask);

                    foreach (var type in Main.GetType().Assembly.GetTypes().Where(tasktype.IsAssignableFrom).Where(type => !_alltasks.ContainsKey(type.Name)))
                    {
                        _alltasks.Add(type.Name, type);
                    }
                }
                return _alltasks;
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

            var task = Get(name, origin);
            if (task == null) return;

            Tasks[task.Id] = task;

            LogManager.Log(LogLevel.Debug, "Starting task: " + task.Name);

            if (task.ValidatePreconditions(p) != TaskResult.ValidateTrue) return;

            MethodInfo method = task.FindMethod("Init", p);

            if (method == null && task.GetType().GetMethods().Any(m => m.Name == "Init"))
                throw new InvalidOperationException(string.Format("Could not find any matching Init method on {0} with parameters {1}",
                                                                  task.GetType().Name,
                                                                  string.Join(",", p.Select(t => t.GetType()))));

            using (new TracingScope("Start " + task.GetType().Name))
            {
                if (method == null) { task.Start(p); } else { method.Invoke(task, p); }
            }
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

            Tasks.Remove(finishedtask.Id);

            MethodInfo method = finishedtask.Origin.FindMethod(string.Format("Continue{0}From{1}", returns.Name, finishedtask.Name), p);

            if (method == null)
            {
                finishedtask.Origin.Continue(finishedtask.Name, returns, p);
            }
            else
            {
                method.Invoke(finishedtask.Origin, p);
            }

        }

        public ITask FindTaskById(Guid id)
        {
            ITask task;

            if (!Tasks.TryGetValue(id, out task))
            {
                if (id == Guid.Empty || Tasks.Count == 0)
                {
                    _main = null;
                    Home();
                }
                throw new TaskNotFoundException();
            }

            return task ?? Task.Empty;
        }

        public void Home(params object[] p)
        {
            Run(Main, Main.Name, p);
        }
    }
}
