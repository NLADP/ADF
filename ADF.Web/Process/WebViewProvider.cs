using System.Globalization;
using System.Web;
using Adf.Core.Tasks;
using Adf.Core.Views;

namespace Adf.Web.Process
{
    /// <summary>
    /// Represents a manager for view i.e. web page in an web application related activities.
    /// Provides methods to activate a view i.e. to open a web page of an web application 
    /// corresponding to a <see cref="ITask"/> and deactivate a view.
    /// </summary>
    public class WebViewProvider : IViewProvider
    {
        /// <summary>
        /// Activates a view i.e. opens a web page of an web application corresponding to 
        /// the specified <see cref="ITask"/>.
        /// </summary>
        /// <param name="task">The <see cref="ITask"/>, the corresponding web page 
        /// of which is to open.</param>
        /// <param name="p">The parameters used to open a web page. Currently not being used.</param>
        public void ActivateView(ITask task, params object[] p)
        {
            string page = string.Format(CultureInfo.InvariantCulture, "~/Forms/{0}.aspx?ID={1}", task.Name, task.Id);

            HttpContext.Current.Response.Redirect(page, true);
        }

        /// <summary>
        /// Activates a view i.e. opens a web page of an web application corresponding to 
        /// the specified <see cref="ITask"/>.
        /// </summary>
        /// <param name="task">The <see cref="ITask"/>, the corresponding web page 
        /// of which is to open.</param>
        /// <param name="newView">The value to indicate whether a new web page is to open.
        /// Currently not being used.</param>
        /// <param name="p">The parameters used to open a web page. Currently not being used.</param>
        public void ActivateView(ITask task, bool newView, params object[] p)
        {
            ActivateView(task, p);
        }

        /// <summary>
        /// Deactivates a view.
        /// Currently nothing is being done.
        /// </summary>
        /// <param name="task">The <see cref="ITask"/>, the corresponding view 
        /// of which is to deactivate.</param>
        /// <param name="p">The parameters used to deactivate a view.</param>
        public void DeactivateView(ITask task, params object[] p)
        {
        }

//        public void DeactivateView(ITask task, bool closeView, params object[] p)
//        {
//        }
    }
}
