using Adf.Core.Extensions;
using Adf.Core.Tasks;

namespace Adf.Test.Tasks
{
    public class TaskTest<T> : BaseTest where T : ITask
    {
        protected T Task;

        private static ApplicationTask GetName<TTask>()
        {
            var name = (typeof(TTask).Name.EndsWith("Task")) ? typeof(TTask).Name.Substring(0, typeof(TTask).Name.Length - "Task".Length) : typeof(TTask).Name;
            
            return new ApplicationTask(name);
        }

        protected override void OnInit()
        {
            base.OnInit();

            Task = CreateTask();
        }

        protected virtual T CreateTask()
        {
            return typeof(T).New<T>(GetName<T>(), null);    
        }

        protected T CreateTaskWithOrigin<TOrigin>() where TOrigin : ITask
        {
            var originaltask = typeof(TOrigin).New<TOrigin>(GetName<TOrigin>(), null);

            return typeof(T).New<T>(GetName<T>(), originaltask);
        }
    }
}