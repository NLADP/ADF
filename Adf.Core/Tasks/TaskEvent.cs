using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adf.Core.Tasks
{
    public class TaskEvent : Descriptor
    {
        public static readonly TaskEvent Search = new TaskEvent("Search");
        public static readonly TaskEvent Share = new TaskEvent("Share");
        public static readonly TaskEvent Back = new TaskEvent("Back");

        public TaskEvent(string name) : base(name) {}
    }
}
