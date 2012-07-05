using System;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;

namespace Adf.Web.UI.SmartView
{
    public class TextButton : SmartButton
    {
        public bool ShowLabelIfDisabled { get; set; }

        public event EventHandler Click;
        private LinkButton button;
        private LinkButton tooltipbutton;
        private Label labelIfDisabled;
        private Label tooltiplabelIfDisabled;

        public TextButton()
        {
            FieldStyle = "TextColumn";
            IdField = "Id";
            CommandName = "Select";
        }

        protected override void InitializeControls(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            button = new LinkButton { CommandName = CommandName };
            button.SetId(ChildId);

            if (Click != null) button.Click += Click;

            cell.Controls.Add(button);

            tooltipbutton = new LinkButton { CommandName = CommandName, Visible = false};
            tooltipbutton.SetId(ChildId);

            cell.Controls.Add(tooltipbutton);

            if (ShowLabelIfDisabled)
            {
                labelIfDisabled = new Label { Visible = false };

                cell.Controls.Add(labelIfDisabled);

                tooltiplabelIfDisabled = new Label { Visible = false };

                cell.Controls.Add(tooltiplabelIfDisabled);
            }

        }

        protected override void ItemDataBinding(object sender, EventArgs e)
        {
            var cell = sender as TableCell;
            if (cell == null) return;

            var entity = cell.GetDataItem();
            var enabled = this.IsEnabled(entity, button.Text);
            var text = this.Compose(entity, DataField, DataFormat);

            if (text.Length > MaxCharacters)
            {
                tooltipbutton.Text = " ...";
                tooltipbutton.ToolTip = text;
                if (ShowLabelIfDisabled)
                {
                    tooltiplabelIfDisabled.Text = " ...";
                    tooltiplabelIfDisabled.ToolTip = text;
                }
            }

            if (enabled)
            {
                button.Text = text.Left(MaxCharacters);
                button.ToolTip = this.Compose(entity, ToolTipField, ToolTipFormat);
                button.CommandArgument = tooltipbutton.CommandArgument = this.Compose(entity, IdField, null);
                button.Visible = tooltipbutton.Visible = true;
                
                var message = this.Compose(entity, MessageSubject, MessageField, MessageFormat);
            
                if (!string.IsNullOrEmpty(message)) { button.OnClientClick = tooltipbutton.OnClientClick = @"return confirm('" + message + "');"; }
            }
            else if (ShowLabelIfDisabled)
            {
                labelIfDisabled.Text = text.Left(MaxCharacters);
                labelIfDisabled.Visible = tooltiplabelIfDisabled.Visible = true;
            }
        }

        protected override void DisposeControls()
        {
            if (button != null) button.Dispose();
            if (tooltipbutton != null) tooltipbutton.Dispose();
            if (labelIfDisabled != null) labelIfDisabled.Dispose();
            if (tooltiplabelIfDisabled != null) tooltiplabelIfDisabled.Dispose();
        }

    }
}
