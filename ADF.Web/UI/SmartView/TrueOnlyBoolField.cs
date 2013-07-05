using System;
using System.Web.UI.WebControls;
using Adf.Core.Domain;
using Adf.Core.Extensions;

namespace Adf.Web.UI.SmartView
{
    public class TrueOnlyBoolField : SmartField, IDisposable
    {
        private Image image;

        public TrueOnlyBoolField()
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

        protected override void ItemDataBinding(object sender, EventArgs e)
        {
            var cell = sender as TableCell;
            if (cell == null) return;

            var entity = cell.GetDataItem();

            var signed = DataField.StartsWith("!");
            var iconField = signed ? DataField.Substring(1) : DataField;
            var result = signed ^ Convert.ToBoolean(PropertyHelper.GetValue(entity, iconField));

            if (result)
            {
                var icon = string.Format(IconFormat, true);

                image.ImageUrl = icon;
                image.ToolTip = this.Compose(entity, ToolTipField, ToolTipFormat);
            }

            image.Visible = result;
        }

        protected override void DisposeControls()
        {
            if (image != null) image.Dispose();
        }

    }
}