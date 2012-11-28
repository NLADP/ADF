using System;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;

namespace Adf.Web.UI.SmartView
{
    public class TextField : SmartField
    {
        protected Label label;
        protected Label tooltiplabel;

        public bool Multiline { get; set; }

        public TextField()
        {
            FieldStyle = "TextColumn";
        }

        protected override void InitializeControls(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            label = new Label();
            label.SetId(ChildId);

            tooltiplabel = new Label();

            cell.Controls.Add(label);
            cell.Controls.Add(tooltiplabel);
        }

        protected override void DisposeControls()
        {
            if (label != null) label.Dispose();
            if (tooltiplabel != null) tooltiplabel.Dispose();
        }

        protected override void ItemDataBinding(object sender, EventArgs e)
        {
            var cell = sender as TableCell;
            if (cell == null) return;

            var entity = cell.GetDataItem();
            var text = this.Compose(entity, DataField, DataFormat);

            if (HideOnEmpty && text.IsNullOrEmpty()) return;

            label.Text = Multiline ? text.Replace(Environment.NewLine, "<br />") : text.Left(MaxCharacters);
            label.ToolTip = this.Compose(entity, ToolTipField, ToolTipFormat);
            label.Visible = tooltiplabel.Visible = this.IsEnabled(entity, text);

            if (!Multiline && text.Length > MaxCharacters)
            {
                tooltiplabel.Text = " ...";
                tooltiplabel.ToolTip = text;
            }
        }
    }
}
