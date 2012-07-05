using System.Collections;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Web.UI.WebControls;
using Adf.Core.Binding;

namespace Adf.Web.Binding
{
    /// <summary>
    /// Represents a binder for a <see cref="System.Web.UI.WebControls.GridView"/>.
    /// Provides methods to bind the values to the properties of a 
    /// <see cref="System.Web.UI.WebControls.GridView"/>.
    /// </summary>
    public class GridViewBinder : IControlBinder
    {
        /// <summary>
        /// The default <see cref="PagerButtons"/>. Here it is 'NumericFirstLast'.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible")]
        protected static PagerButtons DefaultPagingMode = PagerButtons.NumericFirstLast;

        /// <summary>
        /// Gets the array of <see cref="System.Web.UI.WebControls.GridView"/> id prefixes that support binding.
        /// </summary>
        /// <value>The array of <see cref="System.Web.UI.WebControls.GridView"/> id prefixes.</value>
        public IEnumerable Types
        {
            get { return new[] { "grd" }; }
        }

        /// <summary>
        /// Binds the 'DataSource' property of the specified <see cref="System.Web.UI.WebControls.GridView"/> 
        /// with the specified <see cref="System.Data.DataSet"/> object.
        /// </summary>
        /// <remarks>The 'DataKeyNames' property of the specified <see cref="System.Web.UI.WebControls.GridView"/> 
        /// is set to 'ID'.</remarks>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.GridView"/> to bind to.</param>
        /// <param name="value">The <see cref="System.Data.DataSet"/> to bind.</param>
        /// <param name="pi">Currently not being used.</param>
        /// <param name="p">The parameters used for binding. Currently not being used.</param>
        public virtual void Bind(object control, object value, PropertyInfo pi, params object[] p)
        {
            var grid = control as GridView;
            if (grid == null) return;

            var set = value as DataSet;
            if (set == null) return;

            grid.DataSource = set;
            grid.DataKeyNames = new[] { "ID" };
            grid.DataBind();

            //            SetPaging(grid, set.Tables[0].Rows.Count);
        }

        /// <summary>
        /// Binds the 'DataSource' property of the specified <see cref="System.Web.UI.WebControls.GridView"/> 
        /// with the specified array of objects.
        /// </summary>
        /// <remarks>The 'DataKeyNames' property of the specified <see cref="System.Web.UI.WebControls.GridView"/> 
        /// is set to 'ID'.</remarks>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.GridView"/>, the 'DataSource' 
        /// property of which is to bind to.</param>
        /// <param name="values">The array of objects to bind.</param>
        /// <param name="p">The parameters used for binding. Currently not being used.</param>
        public virtual void Bind(object control, object[] values, params object[] p)
        {
            if (values == null) return;

            var grid = control as GridView;
            if (grid == null) return;

            grid.DataSource = values;
            grid.DataKeyNames = new[] { "ID" };
            grid.DataBind();

            //            SetPaging(grid, values.Length);
        }

        /// <summary>
        /// Binds the 'DataSource' property of the specified <see cref="System.Web.UI.WebControls.GridView"/> 
        /// with the specified list.
        /// </summary>
        /// <remarks>The 'DataKeyNames' property of the specified <see cref="System.Web.UI.WebControls.GridView"/> 
        /// is set to 'ID'.</remarks>
        /// <param name="control">>The <see cref="System.Web.UI.WebControls.GridView"/>, the 'DataSource' 
        ///   property of which is to bind to.</param>
        /// <param name="values">The list to bind.</param>
        /// <param name="p">The parameters used for binding. Currently not being used.</param>
        public virtual void Bind(object control, IEnumerable values, params object[] p)
        {
            var grid = control as GridView;
            if (grid == null) return;

            grid.DataSource = values;

            if (values == null) return;

            grid.DataKeyNames = new[] { "ID" };
            grid.DataBind();
        }
    }
}