using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Resources;

namespace Adf.Web.UI.SmartView
{
    public abstract class IconButton : TemplateField
    {
        public event ImageClickEventHandler Click;

        private const string Source = @"../images/{0}";
        public string Image { get; set; }
        public string ColumnStyle { get; set; }
        public string CommandName { get; set; }
        
        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = ResourceManager.GetString(value); }
        }

        private string _tooltip;
        public string ToolTip
        {
            get { return _tooltip; }
            set { _tooltip = ResourceManager.GetString(value); }
        }

        public string Header
        {
            get { return HeaderText; }
            set { HeaderText = ResourceManager.GetString(value); }
        }

        protected IconButton()
        {
            ColumnStyle = "IconButton";
            
        }

        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);

            if (ColumnStyle != null)
            {
                if (HeaderStyle.CssClass.IsNullOrEmpty())  HeaderStyle.CssClass = ColumnStyle;
                if (ItemStyle.CssClass.IsNullOrEmpty())  ItemStyle.CssClass = ColumnStyle;
                if (FooterStyle.CssClass.IsNullOrEmpty())  FooterStyle.CssClass = ColumnStyle;
            }

            if (cellType == DataControlCellType.DataCell)
            {
                string imagename = (Image.IndexOf(".") > 0) ? Image : Image + ".png";
                
                var button = new ImageButton { ImageUrl = string.Format(Source, imagename), CssClass = ColumnStyle, CommandName = CommandName, ToolTip = ToolTip };
                
                if(Click != null) button.Click += Click;

                if (!string.IsNullOrEmpty(Message)) { button.OnClientClick = @"return confirm('" + Message + "');"; }

                cell.Controls.Add(button);
            }
        }
    }
}