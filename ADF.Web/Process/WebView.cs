using System;
using System.Web;
using System.Web.UI;
using Adf.Core.Tasks;
using Adf.Core.Views;

namespace Adf.Web.Process
{
    /// <summary>
    /// Abstract class for the classes of the web pages.
    /// Used to redefine the events like 'OnPreInit', 'OnInit', 'Page_Load' etc of the 
    /// <see cref="System.Web.UI.Page"/> class.
    /// </summary>
    public abstract class WebView<T> : Page, IView where T : ITask
    {
        /// <summary>
        /// Gets or sets the Id of the task corresponding to this web page.
        /// </summary>
        /// <returns>
        /// A <see cref="System.Guid"/> representing the Id of the task corresponding to this web page.
        /// </returns>
        protected Guid Id
        {
            get
            {
                if (ViewState["TaskId"] == null) return Guid.Empty;
                return (Guid)ViewState["TaskId"];
            }
            
            set { ViewState["TaskId"] = value; }
        }

        /// <summary>
        /// Gets the task corresponding to this web page.
        /// </summary>
        /// <returns>
        /// The task corresponding to this web page.
        /// </returns>
        public T MyTask
        {
            get
            {
                return (T)Task;
            }
        }

        /// <summary>
        /// Gets the task corresponding to this web page.
        /// </summary>
        /// <returns>
        /// The task corresponding to this web page.
        /// </returns>
        public ITask Task
        {
            get { return TaskManager.FindTaskById(Id); }
        }

        /// <summary>
        /// Sets the cache properties and binds a business object to a web page.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> containing event data.</param>
        private void Page_Load(object sender, EventArgs e)
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoServerCaching();

            if (!IsPostBack)
                Bind();
        }

        /// <summary>
        /// Provided to get modified in the derived classes.
        /// </summary>
        protected virtual void Bind()
        {

        }

        /// <summary>
        /// Sets the Id of the task corresponding to the web page.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> containing event data.</param>
        protected override void OnPreInit(EventArgs e)
        {
            base.OnPreInit(e);

            if ((Id == Guid.Empty) && (Request.QueryString["ID"] != null))
            {
                Id = new Guid(Request.QueryString["ID"]);
            }

            //reading requestem theme from cookie
            HttpCookie cookie = Request.Cookies.Get("theme");

            if (cookie != null) Theme = cookie.Value;
        }

        /// <summary>
        /// Redefines the <see cref="System.Web.UI.Page"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> containing event data.</param>
        protected override void OnInit(EventArgs e)
        {
            InitializeComponent();

            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Load += Page_Load;
        }
    }
}
