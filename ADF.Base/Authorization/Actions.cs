using Adf.Core;
using Adf.Core.Authorization;

namespace Adf.Base.Authorization
{
    /// <summary>
    /// Provides some predefined tasks (Login, Logout, Help etc), which may be used for a newly created application.
    /// </summary>
    public class Actions : Descriptor, IAction
    {
        [Exclude]
        public static readonly Actions Any = new Actions("Any");

        public static readonly Actions All = new Actions("All");
        public static readonly Actions Remove = new Actions("Remove");
        public static readonly Actions Edit = new Actions("Edit");
        public static readonly Actions View = new Actions("View");
        public static readonly Actions Approve = new Actions("Approve");
        public static readonly Actions Process = new Actions("Process");

        public Actions(string name) : base(name)
        {
        }
    }
}
