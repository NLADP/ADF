using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adf.Web.UI.Styling
{
    /// <summary>
    /// 
    /// </summary>
    public class SmartPanelErrorStyler : IStyler
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

                return;
            }

            var cell = c as TableCell;

            if(cell != null)
            {
                foreach (Control control in cell.Controls)
                {
                    if (control is Label) 
                         ((Label) control).CssClass = "SmartPanelErrorLabel";
                    else if( !(control is LinkButton) && control is WebControl) // Don't change the style for any linkbuttons on the SmartPanel
                        ((WebControl)control).CssClass = "SmartPanelErrorControl";
                }
            }
        }

        #endregion
    }
}