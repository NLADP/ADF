using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Resources;
using Adf.Core.Styling;

namespace Adf.Web.UI
{
    /// <summary>
    /// Represent a panel that is responsible to display all the child controls in a tabular format.
    /// </summary>
    public class SmartPanel : WebControl, INamingContainer
    {
        public string PanelStyle = string.Empty;
        public string RowStyle = string.Empty;
        public string LabelCellStyle = string.Empty;
        public string ControlCellStyle = string.Empty;
        public string LabelStyle = string.Empty;

        public override ControlCollection Controls
        {
            get
            {
                EnsureChildControls();
                return base.Controls;
            }
        }

        /// <summary>
        /// Represent strongly typed list of <see cref="Adf.Web.UI.IPanelItem"/> that can be used to hold 
        /// the name of child controls within <see cref="Adf.Web.UI.SmartPanel"/>.
        /// </summary>
        protected List<IPanelItem> panelItems = new List<IPanelItem>();

        public List<IPanelItem> PanelItems { get { return panelItems; } }

        /// <summary>
        /// Gets or sets the width of <see cref="Adf.Web.UI.SmartPanel"/>.
        /// The width of <see cref="Adf.Web.UI.SmartPanel"/> is responsible for the column width of a table within <see cref="Adf.Web.UI.SmartPanel"/>.
        /// </summary>
        /// <returns>The width of <see cref="Adf.Web.UI.SmartPanel"/>.</returns>
        [Bindable(true), Category("Smart Panel")]
        public int? LabelWidth { get; set; }

        public readonly bool Optional;
        public string ButtonCssClass = "tinybutton";

        public LinkButton CreateButton(string label, bool visible = true, bool enabled = true)
        {
            return new LinkButton { Text = ResourceManager.GetString(label), CssClass = ButtonCssClass, Visible = visible, Enabled = enabled };
        }

        protected bool editable = true;

        /// <summary>
        /// Gets or sets the status to represent <see cref="Adf.Web.UI.SmartPanel"/> is editable or not.
        /// Default value is true.
        /// </summary>
        /// <returns>True if <see cref="Adf.Web.UI.SmartPanel"/> is editable; otherwise, false.</returns>
        [Bindable(true), Category("Smart Panel"), DefaultValue(true)]
        public virtual bool Editable
        {
            get { return editable; }
            set
            {
                editable = value;

                foreach (var item in PanelItems)
                {
                    item.Enabled = value;
                }
            }
        }

        /// <summary>
        /// Adds the specified <see cref="Adf.Web.UI.IPanelItem"/> object to the end of the list.
        /// </summary>
        /// <param name="panelItem">The <see cref="Adf.Web.UI.IPanelItem"/> to add to the end of the list. The value can be null for reference types.</param>
        public void Add(IPanelItem panelItem)
        {
            panelItems.Add(panelItem);
        }

        /// <summary>
        /// Create the control hierarchy used to render the <see cref="Adf.Web.UI.SmartPanel"/> control.
        /// </summary>
        protected override void CreateChildControls()
        {
            var htmlTable = new Table();

            foreach (IPanelItem panelItem in panelItems)
            {
                if (panelItem.Visible)
                {
                    if (!DesignMode) StyleManager.Style(StyleType.Panel, this);

                    var row = new TableRow {CssClass = RowStyle};

                    TableCell cell;

                    if (panelItem.LabelControls != null)
                    {
                        cell = new TableCell { CssClass = LabelCellStyle };
                        if (panelItem.Id != null) cell.ID = "panelLabelItem_" + panelItem.Id;
                        
                        foreach (WebControl labelControl in panelItem.LabelControls)
                        {
                            cell.Controls.Add(labelControl);
                        }

                        if (LabelWidth.HasValue)
                            cell.Width = new Unit(LabelWidth.Value, UnitType.Ex);

                        row.Cells.Add(cell);
                    }

                    cell = new TableCell { CssClass = ControlCellStyle };
                    if (panelItem.Id != null) cell.ID = "panelControlItem_" + panelItem.Id;

                    // if item has no label, span item over 2 columns
                    if (panelItem.LabelControls == null)
                        cell.ColumnSpan = 2;

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

        /// <summary>
        /// Initializes the <see cref="System.Web.UI.HtmlTextWriter"/> object and calls on the child controls of the <see cref="Adf.Web.UI.SmartPanel"/> to render.
        /// </summary>
        /// <param name="writer">The <see cref="System.Web.UI.HtmlTextWriter"/> that receives the content of <see cref="Adf.Web.UI.SmartPanel"/>.</param>
        protected override void Render(HtmlTextWriter writer)
        {
            EnsureChildControls();

            base.Render(writer);
        }
    }
}
