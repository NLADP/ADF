using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Panels;
using Adf.Core.Rendering;
using Adf.Core.Resources;
using Adf.Web.Styling;
using Adf.Web.UI.Extensions;

namespace Adf.Web.Panels
{
    public class ViewRenderer : BaseRenderer, IRenderer
    {
        public object Render(PanelObject panel)
        {
            var table = new Table();
            table.AddStyle(CssClass.Panel);

            foreach (var panelrow in panel.Rows)
            {
                var row = new TableRow();
                row.AddStyle(CssClass.ViewRow);

                foreach (var item in panelrow.Items)
                {
                    var labelcell = new TableCell();

                    labelcell.AddStyle(CssClass.Cell);
                    labelcell.Controls.Add(RenderLabel(item));

                    row.Controls.Add(labelcell);
                }

                table.Controls.Add(row);

                row = new TableRow();
                row.AddStyle(CssClass.ViewRow);

                foreach (var item in panelrow.Items)
                {
                    var itemcell = new TableCell();

                    itemcell.AddStyle(CssClass.Cell);
                    itemcell.Controls.Add(RenderBox(item));

                    row.Controls.Add(itemcell);
                }


                table.Controls.Add(row);
            }

            return table;
        }

        private static WebControl RenderLabel(PanelItem panelItem)
        {
            var label = new Label { ID = panelItem.GetLabelId(), Text = ResourceManager.GetString(panelItem.Label), Visible = panelItem.Visible };

            label.AddStyle(CssClass.Label);

            panelItem.IDs.Add(label.ID);

            return label;
        }

        private static WebControl RenderBox(PanelItem panelItem)
        {
            if (panelItem.Type.IsIn(RenderItemType.Link))
            {
                var link = new HyperLink { ID = panelItem.GetId(), Text = ResourceManager.GetString(panelItem.Label), Visible = panelItem.Visible };

                link.AddStyle(CssClass.Label);

                panelItem.IDs.Add(link.ID);

                return link;
            }

            var text = new Label { ID = panelItem.GetId(), Enabled = false, Width = new Unit(panelItem.Width, UnitType.Ex), Text = (!panelItem.Text.IsNullOrEmpty() ? panelItem.Text : string.Empty), Visible = panelItem.Visible };

            text
                .AddStyle(CssClass.Item)
                .AddStyle(CssClass.ReadOnly)
                .AttachToolTip(panelItem);

            panelItem.Target = text;

            return text;
        }
    }
}
