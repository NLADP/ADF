using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Resources;

namespace Adf.Web.UI.SmartView
{
    public class BaseField : TemplateField
    {
        public string ColumnStyle { get; set; }
        public string Width { get; set; }
        public string ToolTipField { get; set; }

        public string Header
        {
            get { return HeaderText; }
            set { HeaderText = ResourceManager.GetString(value); }
        }

        private string _tooltip;
        public string ToolTip
        {
            get { return _tooltip; }
            set { _tooltip = ResourceManager.GetString(value); }
        }

        private string _datafield;
        /// <summary>
        /// Gets or sets the field name from the data source to bind to the <see cref="BaseColumn"/>.
        /// </summary>
        /// <returns>The name of the data field.</returns>
        /// <remarks>Use the DataField property to specify the field to bind to the <see cref="BaseColumn"/>.
        /// </remarks>
        public string DataField
        {
            get { return _datafield; }
            set { _datafield = value; if (string.IsNullOrEmpty(SortExpression)) SortExpression = value; }
        }

        /// <summary>
        /// Add text or controls to a cell's control collection.
        /// </summary>
        /// <param name="cell"><see cref="System.Web.UI.WebControls.DataControlFieldCell"/></param>
        /// <param name="cellType"><see cref="System.Web.UI.WebControls.DataControlCellType"/></param>
        /// <param name="rowState"><see cref="System.Web.UI.WebControls.DataControlRowState"/></param>
        /// <param name="rowIndex">The index of the row that the <see cref="System.Web.UI.WebControls.DataControlFieldCell"/> is contained in.</param>
        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);

            if (ColumnStyle != null)
            {
                if (HeaderStyle.CssClass.IsNullOrEmpty()) HeaderStyle.CssClass = ColumnStyle;
                if (ItemStyle.CssClass.IsNullOrEmpty()) ItemStyle.CssClass = ColumnStyle;
                if (FooterStyle.CssClass.IsNullOrEmpty()) FooterStyle.CssClass = ColumnStyle;
            }
            if (Width != null)
            {
                ItemStyle.Width = Unit.Parse(Width);
            }
            if (cellType == DataControlCellType.DataCell)
            {
                //ItemTemplate = 
                if (!_tooltip.IsNullOrEmpty()) cell.ToolTip = _tooltip;
            }
        }

    }
}
