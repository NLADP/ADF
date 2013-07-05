using System;
using System.Web.UI.WebControls;

namespace Adf.Web.UI.SmartView
{
    public class IconField : SmartField
    {
        private Image image;

        public IconField()
        {
            FieldStyle = "IconColumn";
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
            var icon = this.ComposeIcon(entity, Icon, DataField, IconFormat);

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
