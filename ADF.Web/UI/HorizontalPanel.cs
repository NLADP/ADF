using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Adf.Web.UI
{
	/// <summary>
	/// Represents horizontal compound panel.
	/// </summary>
	public class HorizontalPanel : CompoundPanel
	{
		/// <summary>
		/// Renders a row containing label and control cells to a table.
		/// </summary>
		/// <param name="table">The output table.</param>
		/// <param name="label">The label.</param>
		/// <param name="controls">The collection of controls.</param>
		protected override void RenderRow(HtmlTable table, Control label, Control[] controls)
		{
			HtmlTableRow tr = new HtmlTableRow();

			HtmlTableCell labelcell = ComposeCell(LabelCellStyle, LabelCellWidth, label);
			tr.Controls.Add(labelcell);

			HtmlTableCell controlcell = ComposeCell(ControlCellStyle, ControlCellWidth, controls);
			tr.Controls.Add(controlcell);

			table.Controls.Add(tr);
		}

		/// <summary>
		/// Renders a row containing control cells to a table.
		/// </summary>
		/// <param name="table">The output table.</param>
		/// <param name="controls">The collection of controls.</param>
		protected override void RenderRow(HtmlTable table, Control[] controls)
		{
			HtmlTableRow tr = new HtmlTableRow();

			HtmlTableCell controlcell = ComposeCell(ControlCellStyle, ControlCellWidth, controls);
			tr.Controls.Add(controlcell);

			table.Controls.Add(tr);
		}
	}
}
