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
    public class TooltipColumn : BaseColumn
    {
        public TooltipColumn()
        {
            ColumnStyle = "TooltipColumn";
        }

        public string Image { get; set; }

        private bool _onlyShowWhenExists = true;
        public bool OnlyShowWhenExists
        {
            get { return _onlyShowWhenExists; }
            set { _onlyShowWhenExists = value; }
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

            if ((OnlyShowWhenExists && (value == null || value.ToString().IsNullOrEmpty()))) return;

            cell.Text = string.Format(@"<img src='{0}'>", Image ?? @"..\images\note.png");

            if (value is Enum) cell.ToolTip = (value as Enum).GetDescription();
            else cell.ToolTip = value.ToString();
        }
    }
}