using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Resources;

namespace Adf.Web.UI.SmartView
{
    public class LinkColumn : TemplateField
    {
        public event EventHandler Click;

        public string ColumnStyle { get; set; }
        public string CommandName { get; set; }
        public string Text { get; set; }
        public string Width { get; set; }

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

        public LinkColumn()
        {
            ColumnStyle = "LinkButton";

        }

        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);

            if (Width != null)
            {
                ItemStyle.Width = Unit.Parse(Width);
            }

            if (cellType == DataControlCellType.DataCell)
            {

                var button = new LinkButton { Text = Text, CssClass = ColumnStyle, CommandName = CommandName, ToolTip = ToolTip };

                if (Click != null) button.Click += Click;

                if (!string.IsNullOrEmpty(Message)) { button.OnClientClick = @"return confirm('" + Message + "');"; }

                cell.Controls.Add(button);
            }
        }
    }
}