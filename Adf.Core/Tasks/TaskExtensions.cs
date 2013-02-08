using System;
using System.Linq;
using System.Reflection;
using Adf.Core.Extensions;

namespace Adf.Core.Tasks
{
    public static class TaskExtensions
    {
        public static MethodInfo FindMethod(this ITask task, string name, params object[] p)
        {
            return task.GetType().FindMethod(name, p);
        }

        public static void HandleEvent(this ITask task, TaskEvent taskevent, params object[] p)
        {
            if (task == null) return;

            var method = task.FindMethod(string.Format("Handle{0}Event", taskevent.Name), p);

            if (method != null) method.Invoke(task, p);
        }
    }
}
