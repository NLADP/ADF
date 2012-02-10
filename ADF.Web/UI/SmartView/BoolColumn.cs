using System;
using System.Web.UI.WebControls;
using Adf.Core.Domain;

namespace Adf.Web.UI.SmartView
{
    public class BoolColumn : BaseColumn
    {
        public BoolColumn()
        {
            ColumnStyle = "BoolColumn";
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

            bool? value = PropertyHelper.GetValue(item.DataItem, DataField) as bool?;

            cell.Text = (value != null && value.Value)  ?  @"<img src='../images/true.png'>" :  @"<img src='../images/false.png'>";
        }
    }
}