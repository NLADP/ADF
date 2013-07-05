using System;
using Adf.Core;

namespace Adf.Base.Tasks
{
    [Serializable]
    public class TaskNotFoundException : Exception
    {
        public override string Message
        {
            get { return "Task not found"; }
        }
    }
}
