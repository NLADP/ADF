using System;
using System.Web.UI.WebControls;
using Adf.Core.Domain;
using Adf.Core.Extensions;

namespace Adf.Web.UI.SmartView
{
    public class BoolField : SmartField, IDisposable
    {
        private Image image;

        public BoolField()
        {
            FieldStyle = "BoolColumn";
            IconFormat = "~/images/{0}.png";
            Width = "16px";
        }

        protected override void InitializeControls(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            image = new Image();
            image.SetId(ChildId);

            cell.Controls.Add(image);
        }

        public static string ComposeIcon(object entity, string value, string field, string format)
        {
            if (entity == null || field.IsNullOrEmpty()) return String.Format(format, value);

            var signed = field.StartsWith("!");
            var iconField = signed ? field.Substring(1) : field;

            return String.Format(format, signed ^ Convert.ToBoolean(PropertyHelper.GetValue(entity, iconField)));
        }

        protected override void ItemDataBinding(object sender, EventArgs e)
        {
            var cell = sender as TableCell;
            if (cell == null) return;

            var entity = cell.GetDataItem();
            var icon = ComposeIcon(entity, Icon, DataField, IconFormat);

            image.ImageUrl = icon;
            image.ToolTip = this.Compose(entity, ToolTipField, ToolTipFormat);
            image.Visible = this.IsEnabled(entity, icon);
        }

        protected override void DisposeControls()
        {
            if (image != null) image.Dispose();
        }

    }
}