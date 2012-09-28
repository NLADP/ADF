using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Adf.Web.UI
{
	/// <summary>
	/// Represents two column compound panel.
	/// </summary>
	public class TwoColumnPanel : CompoundPanel
	{
		/// <summary>
		/// A counter for the current line number.
		/// </summary>
		protected int linecount;

		/// <summary>
		/// The line number from which point the right panel is used.
		/// </summary>
		protected int split;

		HtmlTable left = new HtmlTable();
		HtmlTable right = new HtmlTable();
		
		/// <summary>
		/// Renders a row containing label and control cells to a table.
		/// </summary>
		/// <remarks>
		/// The rows are initially added to the left column and when it's
		/// full, they are added to the right column
		/// </remarks>
		/// <param name="table">The output table.</param>
		/// <param name="label">The label.</param>
		/// <param name="controls">The collection of controls.</param>
		protected override void RenderRow(HtmlTable table, Control label, Control[] controls)
		{
			HtmlTableRow labelrow = ComposeRow(LabelCellStyle, LabelCellWidth, label);
			HtmlTableRow controlrow = ComposeRow(ControlCellStyle, ControlCellWidth, controls);

			if (linecount < split)
			{
				left.Controls.Add(labelrow);
				left.Controls.Add(controlrow);
			}
			else
			{
				right.Controls.Add(labelrow);
				right.Controls.Add(controlrow);
			}

			linecount ++;
		}

		/// <summary>
		/// Renders a row containing control cells to a table.
		/// </summary>
		/// <remarks>
		/// The rows are initially added to the left column and when it's
		/// full, they are added to the right column
		/// </remarks>
		/// <param name="table">The output table.</param>
		/// <param name="controls">The collection of controls.</param>
		protected override void RenderRow(HtmlTable table, Control[] controls)
		{
			HtmlTableRow controlrow = ComposeRow(ControlCellStyle, ControlCellWidth, controls);

			if (linecount < split)
			{
				left.Controls.Add(controlrow);
			}
			else
			{
				right.Controls.Add(controlrow);
			}

			linecount ++;
		}

		/// <summary>
		/// Computes at which point the rows should be added to the right panel.
		/// </summary>
		/// <param name="table">The table.</param>
		protected override void InitRendering(HtmlTable table)
		{
			split = Count / 2;
		}
 
		
		/// <summary>
		/// Finalizes the rendering process.
		/// </summary>
		/// <param name="table">The table.</param>
		protected override void FinaliseRendering(HtmlTable table)
		{
			HtmlTableCell leftCell = new HtmlTableCell();
			HtmlTableCell rightCell = new HtmlTableCell();

			leftCell.Width = "50%";
			rightCell.Width = "50%";

			leftCell.Controls.Add(left);
			rightCell.Controls.Add(right);

			HtmlTableRow row = new HtmlTableRow();
			
			row.Controls.Add(leftCell);
			row.Controls.Add(rightCell);

			table.Controls.Add(row);
		}
	}
}
