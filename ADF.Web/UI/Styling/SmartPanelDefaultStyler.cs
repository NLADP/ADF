using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adf.Web.UI.Styling
{
    /// <summary>
    /// 
    /// </summary>
    public class SmartPanelDefaultStyler : IStyler
    {
        #region IStyler Members

        /// <summary>
        /// 
        /// </summary>
        /// <param name="c"></param>
        public void SetStyles(Control c)
        {
            SmartPanel panel = c as SmartPanel;

            if (panel != null)
            {
                panel.RowStyle = "SmartPanelRow";
                panel.LabelCellStyle = "SmartPanelLabelCell";
                panel.ControlCellStyle = "SmartPanelControlCell";
                panel.LabelStyle = "SmartPanelLabel";
                panel.PanelStyle = "SmartPanel";

                return;
            }

            var cell = c as TableCell;

            if (cell != null)
            {
                foreach (Control control in cell.Controls)
                {
                    if (control is Label)
                        ((Label)control).CssClass = "SmartPanelLabelCell";
                }
            }
        }

        #endregion
    }
}