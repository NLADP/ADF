using System;
using System.Collections;
using System.Globalization;
using System.Web.UI.WebControls;
using Adf.Core.Resources;

namespace Adf.Web.UI.SmartView
{
    public class SmartViewFooter : Table
    {
        public GridView Owner { get; set; }
        public string PageSizes { get; set; }

        private int ItemCount
        {
            get
            {
                var source = ((ICollection) Owner.DataSource);

                return (source == null) ? 0 : source.Count;
            }
        }

        protected override void OnInit(EventArgs e)
        {
         	base.OnInit(e);

            Width = Unit.Parse("100%");
            CssClass = "SmartViewFooter";

            var row = new TableRow();
            var cellleft = new TableCell { Width = Unit.Parse("50%"), HorizontalAlign = HorizontalAlign.Left };
            var cellright = new TableCell { Width = Unit.Parse("50%"), HorizontalAlign = HorizontalAlign.Right };

            var results = new Literal { Text = string.Format(ResourceManager.GetString("Total results: {0}"), ItemCount) };

            var dropdown = new DropDownList { AutoPostBack = true };

            foreach (var pageSize in PageSizes.Trim().Split(','))
            {
                dropdown.Items.Add(pageSize);
            }
            
            dropdown.SelectedValue = (Owner == null) ? dropdown.Items[0].Value : Owner.PageSize.ToString(CultureInfo.InvariantCulture);
            dropdown.SelectedIndexChanged += dropdown_SelectedIndexChanged;

            var items = new Literal {Text = ResourceManager.GetString("items per page.")};

            cellleft.Controls.Add(results);
            cellright.Controls.Add(dropdown);
            cellright.Controls.Add(items);

            row.Controls.Add(cellleft);
            row.Controls.Add(cellright);

            Controls.Add(row);
        }

        void dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            var ddlPaging = sender as DropDownList;
            if (ddlPaging == null) return;
            if (Owner == null) return;

            Owner.PageSize = Int32.Parse(ddlPaging.SelectedValue);
            Owner.DataBind();
        }
    }
}
