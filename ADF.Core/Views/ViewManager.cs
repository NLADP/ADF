using Adf.Core.Objects;
using Adf.Core.Tasks;

namespace Adf.Core.Views
{
    ///<summary>
    /// Manager that handles activation and de-activation of views, depending on the platform (web, Windows, Silverlight).
    ///</summary>
    public static class ViewManager
    {
        private static IViewProvider _provider;

        private static readonly object _lock = new object();

        internal static IViewProvider Provider
        {
            get { lock (_lock) return _provider ?? (_provider = ObjectFactory.BuildUp<IViewProvider>()); }
        }

        ///<summary>
        /// Activate view with task.
        ///</summary>
        ///<param name="task">Task to show view for</param>
        ///<param name="p">Optional parameters.</param>
        public static void ActivateView(ITask task, params object[] p)
        {
            Provider.ActivateView(task, p);
        }

        ///<summary>
        /// Activate view with task.
        ///</summary>
        ///<param name="task">Task to show view for</param>
        ///<param name="newView">Decide if new view is shown, or existing view is re-opened.</param>
        ///<param name="p">Optional parameters.</param>
        public static void ActivateView(ITask task, bool newView, params object[] p)
        {
            Provider.ActivateView(task, newView, p);
        }

        ///<summary>
        /// De-activate view with task.
        ///</summary>
        ///<param name="task">Task to show view for</param>
        ///<param name="p">Optional parameters.</param>
        public static void DeactivateView(ITask task, params object[] p)
        {
            Provider.DeactivateView(task, p);
        }
    }
}
