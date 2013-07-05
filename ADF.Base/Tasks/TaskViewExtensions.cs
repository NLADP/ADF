using Adf.Core.Tasks;
using Adf.Core.Views;

namespace Adf.Base.Tasks
{
    /// <summary>
    /// Extensions to <see cref="Task"/> to show and hide accompanying view.
    /// </summary>
    public static class TaskViewExtensions
    {
        ///<summary>
        /// Activate view using the ViewManager
        ///</summary>
        ///<param name="task">Task to show view for</param>
        ///<param name="p">Optional parameters.</param>
        public static void ActivateView(this ITask task, params object[] p)
        {
            ViewManager.ActivateView(task, p);
        }

        ///<summary>
        /// Activate view using the ViewManager
        ///</summary>
        ///<param name="task">Task to show view for</param>
        ///<param name="newView">Initiate an existing view or a new view.</param>
        ///<param name="p">Optional parameters.</param>
        public static void ActivateView(this ITask task, bool newView, params object[] p)
        {
            ViewManager.ActivateView(task, newView, p);
        }

        ///<summary>
        /// De-activate view using the ViewManager
        ///</summary>
        ///<param name="task">Task to hide view for</param>
        ///<param name="p">Optional parameters.</param>
        public static void DeactivateView(this ITask task, params object[] p)
        {
            ViewManager.DeactivateView(task, p);
        }
    }
}
