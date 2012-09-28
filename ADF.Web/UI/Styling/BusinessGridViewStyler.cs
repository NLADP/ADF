using System.Web.UI;

namespace Adf.Web.UI.Styling
{
    /// <summary>
    /// Represents a styler for a <see cref="SmartView"/>.
    /// Provides method to set the styles defined in the cascading style sheets to a 
    /// <see cref="SmartView"/>.
    /// </summary>
	public class BusinessGridViewStyler : IStyler
	{
        /// <summary>
        /// Sets the CSS styles to the specified <see cref="SmartView"/>.
        /// </summary>
        /// <param name="c">The <see cref="SmartView"/> to give CSS styles to.</param>
        public void SetStyles(Control c)
		{
			SmartView.SmartView grid = c as SmartView.SmartView;
			if (grid == null) return;

			grid.CssClass = string.IsNullOrEmpty(grid.CssClass) ? "BusinessGrid" : grid.CssClass;

            grid.RowStyle.CssClass = string.IsNullOrEmpty(grid.RowStyle.CssClass) ? "BusinessGridItem" : grid.RowStyle.CssClass;
            grid.EditRowStyle.CssClass = string.IsNullOrEmpty(grid.EditRowStyle.CssClass) ? "BusinessGridEditItem" : grid.EditRowStyle.CssClass;
            grid.AlternatingRowStyle.CssClass = string.IsNullOrEmpty(grid.AlternatingRowStyle.CssClass) ? "BusinessGridAlternatingItem" : grid.AlternatingRowStyle.CssClass;
            grid.SelectedRowStyle.CssClass = string.IsNullOrEmpty(grid.SelectedRowStyle.CssClass) ? "BusinessGridSelectedItem" : grid.SelectedRowStyle.CssClass;
			grid.PagerStyle.CssClass = string.IsNullOrEmpty(grid.PagerStyle.CssClass) ? "BusinessGridPager" : grid.PagerStyle.CssClass;
			grid.HeaderStyle.CssClass = string.IsNullOrEmpty(grid.HeaderStyle.CssClass) ? "BusinessGridHeader" : grid.HeaderStyle.CssClass;
			grid.FooterStyle.CssClass = string.IsNullOrEmpty(grid.FooterStyle.CssClass) ? "BusinessGridFooter" : grid.FooterStyle.CssClass;
		}
	}
}