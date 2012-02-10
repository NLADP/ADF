using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adf.Web.UI
{
    /// <summary>
    /// Represent a panel that is responsible to display all the child controls in a tabular format.
    /// </summary>
    public class GridPanel : SmartPanel
    {
        /// <summary>
        /// Represent strongly typed list of <see cref="Adf.Web.UI.IPanelItem"/> that can be used to hold 
        /// the name of child controls within <see cref="Adf.Web.UI.SmartPanel"/>.
        /// </summary>
        protected List<IPanelItem[]> gridPanelItems = new List<IPanelItem[]>();


        /// <summary>
        /// Adds the specified <see cref="Adf.Web.UI.IPanelItem"/> object to the end of the list.
        /// </summary>
        /// <param name="panelItem">The <see cref="Adf.Web.UI.IPanelItem"/> to add to the end of the list. The value can be null for reference types.</param>
        public void Add(params IPanelItem[] panelItem)
        {
            gridPanelItems.Add(panelItem);
        }

        public override bool Editable
        {
            get
            {
                return base.Editable;
            }
            set
            {
                base.Editable = value;

                foreach (var item in gridPanelItems)
                {
                    foreach (var subitem in item)
                    {
                        subitem.Enabled = value;
                    }
                }
            }
        }

        /// <summary>
        /// Create the control hierarchy used to render the <see cref="Adf.Web.UI.SmartPanel"/> control.
        /// </summary>
        protected override void CreateChildControls()
        {
            Table htmlTable = new Table();

            int columnCount = 0;

            foreach (var gridPanelItem in gridPanelItems.Where(gridPanelItem => gridPanelItem.Length > columnCount))
            {
                columnCount = gridPanelItem.Length;
            }

            foreach (IPanelItem[] items in gridPanelItems)
            {
                var row = new TableRow();

                foreach (IPanelItem panelItem in items.Where(panelItem => panelItem.Visible))
                {
                    if (!DesignMode)
                        panelItem.Styler.SetStyles(this);
                    
                    row.CssClass = RowStyle;
                    
                    TableCell cell;

                    if (panelItem.LabelControls != null)
                    {
                        cell = new TableCell {CssClass = LabelCellStyle};
                        if (panelItem.Id != null) cell.ID = "panelLabelItem_" + panelItem.Id;

                        //                        panelItem.LabelControls.CssClass = LabelStyle;
                        foreach (WebControl labelControl in panelItem.LabelControls)
                        {
                            cell.Controls.Add(labelControl);
                        }

                        if (LabelWidth.HasValue)
                            cell.Width = new Unit(LabelWidth.Value, UnitType.Ex);

                        row.Cells.Add(cell);
                    }

                    cell = new TableCell {CssClass = ControlCellStyle};
                    if (panelItem.Id != null) cell.ID = "panelControlItem_" + panelItem.Id;

                    // ColumnSpan: if there are more columns in the grid than items in the row, span the last control over the resting columns
                    if (items[items.Length - 1] == panelItem && items.Length < columnCount)
                    {
                        cell.ColumnSpan = columnCount - items.Length + 2;
                    }
                    // if item has no label, span item over 2 columns
                    if (panelItem.LabelControls == null)
                    {
                        if (cell.ColumnSpan == 0) // not set
                            cell.ColumnSpan = 3;
                        else 
                            cell.ColumnSpan++;
                    }

                    foreach (Control control in panelItem.Controls)
                    {
                        cell.Controls.Add(control);
                    }
                    row.Cells.Add(cell);

                    htmlTable.Controls.Add(row);
                }
            }

            htmlTable.CssClass = PanelStyle;


            Controls.Add(htmlTable);
        }
    }
}
