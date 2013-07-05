using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Base.Formatting;
using Adf.Core.Domain;
using Adf.Core.Extensions;

namespace Adf.Web.UI.SmartView
{
    public class IconButton : SmartButton
    {
        private ImageButton button;
        public event ImageClickEventHandler Click;

        public IconButton()
        {
            FieldStyle = "IconColumn";
            Width = "16px";
            HideOnEmpty = false;
            IdField = "Id";
            CommandName = "Select";
        }

        protected override void InitializeControls(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            button = new ImageButton { ImageUrl = "", CommandName = CommandName };
            button.SetId(ChildId);

            if (Click != null) button.Click += Click;

            cell.Controls.Add(button);
        }

        protected override void ItemDataBinding(object sender, EventArgs e)
        {
            var cell = sender as TableCell;
            if (cell == null) return;

            var entity = cell.GetDataItem();
            var icon = this.ComposeIcon(entity, Icon, DataField, IconFormat);

            button.ImageUrl = icon;
            button.ToolTip = this.Compose(entity, ToolTipField, ToolTipFormat);
            button.CommandArgument = this.Compose(entity, IdField, null);
            button.Visible = this.IsEnabled(entity, icon);

            var message = this.Compose(entity, MessageField, MessageFormat, MessageSubject);
            
            if (!message.IsNullOrEmpty()) { button.OnClientClick = @"return confirm('" + message + "');"; }
        }

        protected override void DisposeControls()
        {
            if (button != null) button.Dispose();
        }
    }
}
