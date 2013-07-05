using System.Collections;
using System.Data;
using System.Reflection;
using System.Web.UI.WebControls;
using Adf.Core.Binding;
using System.Linq;
using Adf.Core.Extensions;

namespace Adf.Web.Binding
{
	/// <summary>
    /// Represents a binder for a <see cref="System.Web.UI.WebControls.DataGrid"/>.
    /// Provides methods to bind the properties of a <see cref="System.Web.UI.WebControls.DataGrid"/> to values.
	/// </summary>
    public class DataGridBinder : IControlBinder
    {
        /// <summary>
        /// The default <see cref="PagerMode"/>. Here it is 'NumericPages'.
        /// </summary>
        private const PagerMode defaultPagingMode = PagerMode.NumericPages;

        /// <summary>
        /// Gets the array of <see cref="System.Web.UI.WebControls.DataGrid"/> id prefixes that support binding.
        /// </summary>
        /// <value>The array of <see cref="System.Web.UI.WebControls.DataGrid"/> id prefixes.</value>
        public IEnumerable Types
        {
            get { return new[] { "grd" }; }
        }

        /// <summary>
        /// Binds the 'DataSource' property of the specified <see cref="System.Web.UI.WebControls.DataGrid"/> 
        /// with the specified <see cref="System.Data.DataSet"/>.
        /// </summary>
        /// <remarks>The 'DataKeyField' property of the specified <see cref="System.Web.UI.WebControls.DataGrid"/> 
        /// is set to 'ID'.</remarks>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.DataGrid"/>, the 'DataSource' 
        /// property of which is to bind to.</param>
        /// <param name="value">The <see cref="System.Data.DataSet"/> to bind.</param>
        /// <param name="pi">The property of the <see cref="System.Web.UI.WebControls.DataGrid"/> 
        /// to bind to. Currently not being used.</param>
        /// <param name="p">The parameters used for binding. Currently not being used.</param>
        public virtual void Bind(object control, object value, PropertyInfo pi, params object[] p)
        {
            if (value == null) return;

            DataGrid grid = control as DataGrid;
            if (grid == null) return;

            DataSet set = value as DataSet;
            if (set == null) return;

            grid.DataSource = set;
            grid.DataKeyField = "ID";
            grid.DataBind();

            SetPaging(grid, set.Tables[0].Rows.Count);
        }

        /// <summary>
        /// Binds the 'DataSource' property of the specified <see cref="System.Web.UI.WebControls.DataGrid"/> 
        /// with the specified array of objects.
        /// </summary>
        /// <remarks>The 'DataKeyField' property of the specified <see cref="System.Web.UI.WebControls.DataGrid"/> 
        /// is set to 'ID'.</remarks>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.DataGrid"/>, the 'DataSource' 
        /// property of which is to bind to.</param>
        /// <param name="values">The array of objects to bind.</param>
        /// <param name="p">The parameters used for binding. Currently not being used.</param>
        public virtual void Bind(object control, object[] values, params object[] p)
        {
            if (values == null) return;

            DataGrid grid = control as DataGrid;
            if (grid == null) return;

            grid.DataSource = values;
            grid.DataKeyField = "ID";
            grid.DataBind();

            SetPaging(grid, values.Length);
        }

	    /// <summary>
	    /// Binds the 'DataSource' property of the specified <see cref="System.Web.UI.WebControls.DataGrid"/> 
	    /// with the specified list.
	    /// </summary>
	    /// <remarks>The 'DataKeyField' property of the specified <see cref="System.Web.UI.WebControls.DataGrid"/> 
	    /// is set to 'ID'.</remarks>
	    /// <param name="control">The <see cref="System.Web.UI.WebControls.DataGrid"/>, the 'DataSource' 
	    ///   property of which is to bind to.</param>
	    /// <param name="values">The list to bind.</param>
	    /// <param name="p">The parameters used for binding. Currently not being used.</param>
        public virtual void Bind(object control, IEnumerable values, params object[] p)
        {
            if (values == null) return;

            DataGrid grid = control as DataGrid;
            if (grid == null) return;

            grid.DataSource = values;
            grid.DataKeyField = "ID";
            grid.DataBind();

            SetPaging(grid, values.Count());
        }

        /// <summary>
        /// Sets the paging related properties of the specified <see cref="System.Web.UI.WebControls.DataGrid"/>.
        /// The specified number of records determines the visibility of the pager.
        /// </summary>
        /// <remarks>The 'DataKeyField' property of the specified <see cref="System.Web.UI.WebControls.DataGrid"/> 
        /// is set to 'ID'.</remarks>
        /// <param name="grid">The <see cref="System.Web.UI.WebControls.DataGrid"/>, the paging 
        /// related properties of which are to set.</param>
        /// <param name="rows">The number of records.</param>
        private static void SetPaging(DataGrid grid, int rows)
        {
            grid.PagerStyle.Visible = grid.PageSize < rows;
            grid.PagerStyle.Mode = defaultPagingMode;
            grid.CurrentPageIndex = 0;
        }
    }
}