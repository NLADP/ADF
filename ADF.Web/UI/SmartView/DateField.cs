using System;
using System.Globalization;
using System.Web.UI.WebControls;
using Adf.Base.Formatting;
using Adf.Core.Domain;
using Adf.Core.Extensions;
using Adf.Core.State;

namespace Adf.Web.UI.SmartView
{
	public class DateField : TextField
	{
        public string DateTimeFormat { get; set; }

        public DateField()
        {
            FieldStyle = "DateTimeColumn";
            HeadStyle = "DateTimeColumnHeader";
            DateTimeFormat = StateManager.Settings.GetOrDefault("DateFormat", CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);
        }

        public string Compose(object entity, string field, string format, string value = null)
        {
            var result = (entity != null && !field.IsNullOrEmpty()) ? FormatHelper.ToString(PropertyHelper.GetValue(entity, field), DateTimeFormat) : (value ?? string.Empty);
            if (!format.IsNullOrEmpty()) result = String.Format(format, result);

            return result;
        }

        protected override void ItemDataBinding(object sender, EventArgs e)
        {
            var cell = sender as TableCell;
            if (cell == null) return;

            var entity = cell.GetDataItem();
            var text = Compose(entity, DataField, DataFormat);

            if (HideOnEmpty && text.IsNullOrEmpty()) return;

            label.Text = text.Left(MaxCharacters);
            label.ToolTip = Compose(entity, ToolTipField, ToolTipFormat);
            label.Visible = tooltiplabel.Visible = this.IsEnabled(entity, text);

            if (text.Length <= MaxCharacters) return;

            tooltiplabel.Text = " ...";
            tooltiplabel.ToolTip = text;
        }
	}
}
