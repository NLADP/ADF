using Adf.Core.Styling;
using Adf.Core.Extensions;
using Adf.Web.UI.SmartView;

namespace Adf.Web.Styling
{
    /// <summary>
    /// Represents a styler for a <see cref="SmartView"/>.
    /// Provides method to set the styles defined in the cascading style sheets to a 
    /// <see cref="SmartView"/>.
    /// </summary>
	public class BusinessGridViewStyler : IStyler
    {
        public string SetStyle(string style, string value)
        {
            return !style.IsNullOrEmpty() ? style : "SmartView" + value;
        }

        /// <summary>
        /// Sets the CSS styles to the specified <see cref="SmartView"/>.
        /// </summary>
        /// <param name="target">The <see cref="SmartView"/> to give CSS styles to.</param>
        public void Style(object target)
		{
			var grid = target as SmartView;
			if (grid == null) return;

            grid.CssClass = SetStyle(grid.CssClass, ""); 
            grid.RowStyle.CssClass = SetStyle(grid.RowStyle.CssClass, "Row");
            grid.EditRowStyle.CssClass = SetStyle(grid.EditRowStyle.CssClass, "EditRow");
            grid.AlternatingRowStyle.CssClass = SetStyle(grid.AlternatingRowStyle.CssClass, "AlternatingRow");
            grid.SelectedRowStyle.CssClass = SetStyle(grid.SelectedRowStyle.CssClass, "SelectedRow");
            grid.PagerStyle.CssClass = SetStyle(grid.PagerStyle.CssClass, "Pager");
            grid.FooterStyle.CssClass = SetStyle(grid.FooterStyle.CssClass, "Footer");
            grid.HeaderStyle.CssClass = SetStyle(grid.HeaderStyle.CssClass, "Header");
		}
	}
}