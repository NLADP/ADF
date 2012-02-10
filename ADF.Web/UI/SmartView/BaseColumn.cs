using System;
using System.Web.UI.WebControls;
using Adf.Base.Formatting;
using Adf.Core.Domain;
using Adf.Core.Extensions;
using Adf.Core.Resources;

namespace Adf.Web.UI.SmartView
{
    /// <summary>
    /// Represents a customized DataControlField (Column) for use with the <see cref="SmartView"/>.
    /// </summary>
    public abstract class BaseColumn : DataControlField
    {
        public string ColumnStyle { get; set; }
        public string Width { get; set; }
        public string ToolTipField { get; set; }

        public string Header 
        { 
            get { return HeaderText; }
            set { HeaderText = ResourceManager.GetString(value); } 
        }

        protected string tooltip;
        public string ToolTip
        {
            get { return tooltip; }
            set { tooltip = ResourceManager.GetString(value); }
        }


        private string datafield;
        /// <summary>
        /// Gets or sets the field name from the data source to bind to the <see cref="BaseColumn"/>.
        /// </summary>
        /// <returns>The name of the data field.</returns>
        /// <remarks>Use the DataField property to specify the field to bind to the <see cref="BaseColumn"/>.
        /// </remarks>
        public string DataField
        {
            get { return datafield; } 
            set {  datafield = value;  if (SortExpression.IsNullOrEmpty()) SortExpression = value; }
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
                if (HeaderStyle.CssClass.IsNullOrEmpty())  HeaderStyle.CssClass = ColumnStyle;
                if (ItemStyle.CssClass.IsNullOrEmpty())  ItemStyle.CssClass = ColumnStyle;
                if (FooterStyle.CssClass.IsNullOrEmpty())  FooterStyle.CssClass = ColumnStyle;
            }
            if (Width != null)
            {
                ItemStyle.Width = Unit.Parse(Width);
            }
            if (cellType == DataControlCellType.DataCell) cell.DataBinding += ItemDataBinding;
            if (cellType == DataControlCellType.DataCell && !tooltip.IsNullOrEmpty()) cell.ToolTip = tooltip;    
        }

        /// <summary>
        /// Provides the data binding to TableCell.
        /// </summary>
        /// <param name="sender"><see cref="System.Object"/></param>
        /// <param name="e"><see cref="System.EventArgs"/></param>
        protected virtual void ItemDataBinding(object sender, EventArgs e)
        {
            if (DataField == null) return;

            var cell = sender as TableCell;
            if (cell == null) return;

            var item = cell.NamingContainer as GridViewRow;
            if (item == null || item.DataItem == null) return;

            var value = PropertyHelper.GetValue(item.DataItem, DataField);
            cell.Text = FormatHelper.ToString(value);

            if (value is Enum) cell.ToolTip = (value as Enum).GetDescription();
            else if (!ToolTipField.IsNullOrEmpty()) cell.ToolTip = FormatHelper.ToString(PropertyHelper.GetValue(item.DataItem, ToolTipField));
        }

        /// <summary>
        /// Creates an empty data control field
        /// </summary>
        /// <returns><see cref="BoundField"/></returns>
        protected override DataControlField CreateField()
        {
            return new BoundField();
        }
    }
}