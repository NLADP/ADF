using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Domain;
using Adf.Core.Extensions;

namespace Adf.Web.UI.SmartView
{
    public class CheckBoxField : SmartField
    {
        public string IdField { get; set; }

        private CheckBox allbox;
        private CheckBox box;
        private Label tooltiplabel;

        public CheckBoxField()
        {
            FieldStyle = "TextColumn";
            IdField = "Id";
        }

        public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            base.InitializeCell(cell, cellType, rowState, rowIndex);

            if (cellType != DataControlCellType.Header) return;

            allbox = new CheckBox();
            cell.Controls.Add(allbox);

            allbox.Attributes.Add("onclick", "javascript:CheckAll(this)");
        }

        protected override void InitializeControls(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
        {
            box = new CheckBox();
            box.SetId(ChildId ?? DataField);

            tooltiplabel = new Label();

            cell.Controls.Add(box);
            cell.Controls.Add(tooltiplabel);
        }

        protected override void DisposeControls()
        {
            if (box != null) box.Dispose();
            if (tooltiplabel != null) tooltiplabel.Dispose();
        }

        protected override void ItemDataBinding(object sender, EventArgs e)
        {
            var cell = sender as TableCell;
            if (cell == null) return;

            var entity = cell.GetDataItem();
            var text = this.Compose(entity, DataField, DataFormat);

            if (HideOnEmpty && text.IsNullOrEmpty()) return;

            box.Checked = Convert.ToBoolean(PropertyHelper.GetValue(entity, DataField));
            box.Attributes.Add("Id", this.Compose(entity, IdField, null));

            box.ToolTip = this.Compose(entity, ToolTipField, ToolTipFormat);
            box.Visible = tooltiplabel.Visible = this.IsEnabled(entity, text);

            if (text.Length > MaxCharacters)
            {
                tooltiplabel.Text = " ...";
                tooltiplabel.ToolTip = text;
            }
        }
    }
}
