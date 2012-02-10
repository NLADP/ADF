using System;
using System.ComponentModel;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Base.Formatting;
using Adf.Core.Domain;
using Adf.Core.Extensions;
using Adf.Core.Resources;
using System.Linq;

namespace Adf.Web.UI.SmartView
{
    public class BaseButtonTemplate : ITemplate
{
        public void InstantiateIn(Control container)
        {
            throw new NotImplementedException();
        }
}
    public abstract class BaseButton : ButtonField
    {
        public string ColumnStyle { get; set; }
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

        public string Width { get; set; }

        /// <summary>
        /// Gets or sets the field name from the data source to bind to the <see cref="BaseColumn"/>.
        /// </summary>
        /// <returns>The name of the data field.</returns>
        /// <remarks>Use the DataField property to specify the field to bind to the <see cref="BaseColumn"/>.
        /// </remarks>
        public string DataField
        {
            get { return DataTextField; }
            set { DataTextField = value; if (SortExpression.IsNullOrEmpty()) SortExpression = value; }
        }

        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);

            if (ColumnStyle != null)
            {
                if (HeaderStyle.CssClass.IsNullOrEmpty()) HeaderStyle.CssClass = ColumnStyle;
                if (ItemStyle.CssClass.IsNullOrEmpty()) ItemStyle.CssClass = ColumnStyle;
                if (FooterStyle.CssClass.IsNullOrEmpty()) FooterStyle.CssClass = ColumnStyle;
            }
            if (Width != null) ItemStyle.Width = Unit.Parse(Width);
            if (CommandName.IsNullOrEmpty()) CommandName = "Select";

            if (cellType == DataControlCellType.DataCell && !tooltip.IsNullOrEmpty()) cell.ToolTip = tooltip;

            var control = cell.Controls.Cast<Control>().OfType<IButtonControl>().OfType<Control>().FirstOrDefault();

            if (control != null)
            {
                // workaround: ButtonField.InitializeCell add handler OnDataBindField which tries to get DataField from DataItem
                RemoveEventHandlers(control, "DataBinding");
            }
            cell.DataBinding += ItemDataBinding;
        }

        private static void RemoveEventHandlers(Control control, string eventName)
        {
            // a bit ugly but it does the job..
            FieldInfo f1 = typeof (Control).GetField("Event" + eventName, BindingFlags.Static | BindingFlags.NonPublic);
            object obj = f1.GetValue(control);
            PropertyInfo pi = control.GetType().GetProperty("Events", BindingFlags.NonPublic | BindingFlags.Instance);
            var list = (EventHandlerList) pi.GetValue(control, null);
            list.RemoveHandler(obj, list[obj]);
        }

        /// <summary>
        /// Provides the data binding to TableCell.
        /// </summary>
        /// <param name="sender"><see cref="System.Object"/></param>
        /// <param name="e"><see cref="System.EventArgs"/></param>
        protected virtual void ItemDataBinding(object sender, EventArgs e)
        {
            if (DataField.IsNullOrEmpty()) return;

            var cell = sender as TableCell;
            if (cell == null) return;

            var item = cell.NamingContainer as GridViewRow;
            if (item == null || item.DataItem == null) return;

            var value = PropertyHelper.GetValue(item.DataItem, DataField);

            var control = cell.Controls.Cast<Control>().OfType<IButtonControl>().FirstOrDefault();
            if (control != null) control.Text = FormatHelper.ToString(value);
//            cell.Controls.Add(new LinkButton { Text = FormatHelper.ToString(value) });

            if (value is Enum) cell.ToolTip = (value as Enum).GetDescription();
            else if (!ToolTipField.IsNullOrEmpty()) cell.ToolTip = FormatHelper.ToString(PropertyHelper.GetValue(item.DataItem, ToolTipField));
        }
    }
}
