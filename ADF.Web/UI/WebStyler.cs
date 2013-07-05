using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adf.Web.UI
{
	/// <summary>
	/// Applies styling to all controls in a container, or to a business datagrid.
	/// </summary>
	public static class WebStyler
	{
		/// <summary>
		/// Applies styling to all controls in the specified container.
		/// </summary>
		/// <param name="parent">The container control.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static void Style(Control parent)
		{
			foreach(Control c in parent.Controls)
			{
				Style(c);
			}
		}

		/// <summary>
		/// Applies styling to a business datagrid.
		/// </summary>
		/// <param name="grid">The business datagrid.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public static void Style(DataGrid grid)
		{
            grid.CssClass = string.IsNullOrEmpty(grid.CssClass ) ? "Grid" : grid.CssClass;
            grid.ItemStyle.CssClass = string.IsNullOrEmpty(grid.ItemStyle.CssClass ) ? "GridItem" : grid.ItemStyle.CssClass;
            grid.EditItemStyle.CssClass = string.IsNullOrEmpty(grid.EditItemStyle.CssClass ) ? "GridEditItem" : grid.EditItemStyle.CssClass;
            grid.AlternatingItemStyle.CssClass = string.IsNullOrEmpty(grid.AlternatingItemStyle.CssClass ) ? "GridAlternatingItem" : grid.AlternatingItemStyle.CssClass;
            grid.SelectedItemStyle.CssClass = string.IsNullOrEmpty(grid.SelectedItemStyle.CssClass ) ? "GridSelectedItem" : grid.SelectedItemStyle.CssClass;
            grid.PagerStyle.CssClass = string.IsNullOrEmpty(grid.PagerStyle.CssClass ) ? "GridPager" : grid.PagerStyle.CssClass;
            grid.HeaderStyle.CssClass = string.IsNullOrEmpty(grid.HeaderStyle.CssClass ) ? "GridHeader" : grid.HeaderStyle.CssClass;
            grid.FooterStyle.CssClass = string.IsNullOrEmpty(grid.FooterStyle.CssClass) ? "GridFooter" : grid.FooterStyle.CssClass;
		}

	}
}