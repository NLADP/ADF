using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Adf.Web.UI
{
	/// <summary>
	/// Represents vertical compound panel.
	/// </summary>
	public class VerticalPanel : CompoundPanel
	{
		/// <summary>
		/// Renders a row containing label and control cells to a table.
		/// </summary>
		/// <param name="table">The output table.</param>
		/// <param name="label">The label.</param>
		/// <param name="controls">The collection of controls.</param>
		protected override void RenderRow(HtmlTable table, Control label, Control[] controls)
		{
			HtmlTableRow labelrow = ComposeRow(LabelCellStyle, LabelCellWidth, label);
			table.Controls.Add(labelrow);

			HtmlTableRow controlrow = ComposeRow(ControlCellStyle, ControlCellWidth, controls);
			table.Controls.Add(controlrow);
		}

		/// <summary>
		/// Renders a row containing control cells to a table.
		/// </summary>
		/// <param name="table">The output table.</param>
		/// <param name="controls">The collection of controls.</param>
		protected override void RenderRow(HtmlTable table, Control[] controls)
		{
			HtmlTableRow controlrow = ComposeRow(ControlCellStyle, ControlCellWidth, controls);
			table.Controls.Add(controlrow);
		}
	}
}
