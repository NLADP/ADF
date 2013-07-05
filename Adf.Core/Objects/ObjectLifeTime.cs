using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Adf.Core.Objects
{
    public class ObjectLifeTime : Descriptor
    {
        public ObjectLifeTime(string name) : base(name)
        {
        }

        public static readonly ObjectLifeTime SharedInstance = new ObjectLifeTime("SharedInstance");
        public static readonly ObjectLifeTime InstancePerThread = new ObjectLifeTime("InstancePerThread");
        public static readonly ObjectLifeTime Shared = new ObjectLifeTime("Shared");

    }
}
