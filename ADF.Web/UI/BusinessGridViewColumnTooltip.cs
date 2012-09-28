using System;
using System.Globalization;
using System.Web.UI.WebControls;
using Adf.Base.Domain;
using Adf.Core.Domain;
using Adf.Core.Extensions;
using Adf.Core.State;
using Adf.Web.UI.SmartView;

namespace Adf.Web.UI
{
    /// <summary>
    /// Represents a customized DataControlField (Column) for use with the <see cref="BaseColumn" />
    ///   .
    /// </summary>
    public class BusinessGridViewColumnTooltip : DataControlField
    {
        #region DataField

        /// <summary>
        /// Gets or sets the field name from the data source to bind to the <see cref="BaseColumn"/>.
        /// </summary>
        /// <returns>The name of the data field.</returns>
        /// <remarks>Use the DataField property to specify the field to bind to the <see cref="BaseColumn"/>.
        /// </remarks>
        public string DataField
        {
            get;
            set;
        }

        public string DescriberField
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// Add text or controls to a cell's control collection.
        /// </summary>
        /// <param name="cell"><see cref="System.Web.UI.WebControls.DataControlFieldCell"/></param>
        /// <param name="cellType"><see cref="System.Web.UI.WebControls.DataControlCellType"/></param>
        /// <param name="rowState"><see cref="System.Web.UI.WebControls.DataControlRowState"/></param>
        /// <param name="rowIndex">The index of the row that the <see cref="System.Web.UI.WebControls.DataControlFieldCell"/> is contained in.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")]
        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType,
                                            DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);

            switch (cellType)
            {
                case DataControlCellType.DataCell:
                    cell.DataBinding += ItemDataBinding;
                    break;
            }
        }

        /// <summary>
        /// Provides the data binding to TableCell.
        /// </summary>
        /// <param name="sender"><see cref="System.Object"/></param>
        /// <param name="e"><see cref="System.EventArgs"/></param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        private void ItemDataBinding(object sender, EventArgs e)
        {
            var cell = sender as TableCell;
            if (cell == null)
                return;

            var item = cell.NamingContainer as GridViewRow;
            if (item == null || item.DataItem == null) return;

            //            PropertyInfo pi = item.DataItem.GetType().GetProperty(dataField);
            //            if (pi == null)
            //                return;
            //
            //            object val = pi.GetValue(item.DataItem, null);

            object val = PropertyHelper.GetValue(item.DataItem, DataField);

            if (val is Enum)
            {
                cell.Text = val.ToString();
                cell.ToolTip = (val as Enum).GetDescription();
            }
            else if (val is DateTime?)
            {
                var dateTime = val as DateTime?;

                var format = StateManager.Settings.Has("DateFormat")
                                 ? StateManager.Settings["DateFormat"].ToString()
                                 : CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern;

                cell.Text = dateTime.Value.ToString(format, CultureInfo.CurrentUICulture);
            }
            else if (val is bool?)
            {
                var value = val as bool?;

                cell.Text = (value.Value) ? "<input id='chkBool' type='checkbox' name='chkBool' checked disabled />" : "<input id='chkBool' type='checkbox' name='chkBool' disabled />";
            }
            else if (val is DomainObject)
            {
                cell.Text = val.ToString().BreakLongWords();
                if (!string.IsNullOrEmpty(DescriberField))
                {
                    object descriptionfield = PropertyHelper.GetValue(val, DescriberField);
                    if (descriptionfield != null) cell.ToolTip = descriptionfield.ToString();
                }
            }
            else if (val is IFormattable)
            {
                cell.Text = ((IFormattable)val).ToString(null, CultureInfo.CurrentUICulture);
            }
            else
            {
                cell.Text = (val == null) ? string.Empty : val.ToString().BreakLongWords();
            }
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