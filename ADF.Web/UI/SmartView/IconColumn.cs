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
    public class IconColumn : BaseColumn
    {
        public IconColumn()
        {
            ColumnStyle = "IconColumn";
        }

        /// <summary>
        /// Provides the data binding to TableCell.
        /// </summary>
        /// <param name="sender"><see cref="System.Object"/></param>
        /// <param name="e"><see cref="System.EventArgs"/></param>
        protected override void ItemDataBinding(object sender, EventArgs e)
        {
            var cell = sender as TableCell;
            if (cell == null) return;

            var item = cell.NamingContainer as GridViewRow;
            if (item == null || item.DataItem == null) return;

            var value = PropertyHelper.GetValue(item.DataItem, DataField);

            cell.Text = string.Format(@"<img src='{0}'>", value);

            if (value is Enum) cell.ToolTip = (value as Enum).GetDescription();
            else if (!ToolTipField.IsNullOrEmpty()) cell.ToolTip = FormatHelper.ToString(PropertyHelper.GetValue(item.DataItem, ToolTipField));
        }
    }
}