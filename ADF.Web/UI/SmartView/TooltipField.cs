using System;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;

namespace Adf.Web.UI.SmartView
{
    public class TooltipField : SmartField, IDisposable
    {
        private Image image;

        public TooltipField()
        {
            FieldStyle = "IconColumn";
            IconFormat = @"~\images\note.png";
            HideOnEmpty = true;
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
            var icon = (Icon.IsNullOrEmpty()) ? this.Compose(entity, DataField, IconFormat) : Icon;
            var tooltip = this.Compose(entity, ToolTipField, ToolTipFormat);
            
            image.ImageUrl = icon;
            image.ToolTip = tooltip;
            image.Visible = this.IsEnabled(entity, tooltip);
        }

        protected override void DisposeControls()
        {
            if (image != null) image.Dispose();
        }

    }
}