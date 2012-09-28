using System.Linq;
using System.Web.UI.WebControls;
using Adf.Core.Styling;
using Adf.Web.Panels;
using Adf.Web.UI;

namespace Adf.Web.Styling
{
    /// <summary>
    /// 
    /// </summary>
    public class SmartPanelErrorStyler : IStyler
    {
        public void Style(object target)
        {
            StylePanel(target as SmartPanel);
            StylePanel(target as BaseRenderer);
            StyleTableCell(target as TableCell);
            StyleControl(target as WebControl);
        }

        private static void StyleTableCell(TableCell cell)
        {
            if (cell == null) return;

            foreach (var control in cell.Controls.OfType<WebControl>())
            {
                StyleControl(control);
            }
        }

        private static void StyleControl(WebControl control)
        {
            if (control == null || control is TableCell) return;

            control.CssClass = (control is Label) ? "SmartPanelErrorLabel" : !(control is LinkButton) ? "SmartPanelErrorControl" : control.CssClass;
        }

        private static void StylePanel(SmartPanel panel)
        {
            if (panel == null) return;

            panel.RowStyle = "SmartPanelRow";
            panel.LabelCellStyle = "SmartPanelLabelCell";
            panel.ControlCellStyle = "SmartPanelControlCell";
        }

        private static void StylePanel(BaseRenderer renderer)
        {
            if (renderer == null) return;

            renderer.PanelStyle = "AdfPanelPanel";
            renderer.RowStyle = "AdfPanelRow";
            renderer.LabelCellStyle = "AdfPanelLabelCell";
            renderer.LabelStyle = "AdfPanelLabel";
            renderer.ItemCellStyle = "AdfPanelItemCell";
            renderer.ItemStyle = "AdfPanelItem";
        }
    }
}