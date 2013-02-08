using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Objects;
using Adf.Core.Panels;
using Adf.Core.Rendering;
using Adf.Core.Resources;
using Adf.Web.Styling;
using Adf.Web.UI;
using System.Linq;
using Adf.Web.UI.Extensions;

namespace Adf.Web.Panels
{
    public class PanelRenderer : BaseRenderer, IRenderer
    {
        private static IEnumerable<IItemRenderer> _renderers;
        private static readonly object _lock = new object();

        private static IEnumerable<IItemRenderer> Renderers
        {
            get { lock (_lock) return _renderers ?? (_renderers = ObjectFactory.BuildAll<IItemRenderer>().ToList()); }
        }

        public object Render(PanelObject panel)
        {
            short index = 0;
            var table = new Table { CssClass = PanelStyle};

            int cellsperrow = panel.GetMaxItemsPerRow() * 2;

            foreach (var panelrow in panel.Rows)
            {
                var row = new TableRow { CssClass = RowStyle };
                var itemcell = new TableCell();

                for (int i = 0; i < panelrow.Items.Count(); i++)
                {
                    var item = panelrow.Items[i];

                    var labels = RenderLabel(item);
                    var items = RenderItem(item);

                    item.SetTabIndex(index += 3);

                    if (!item.AttachToPrevious)
                    {
                        if (!item.Label.IsNullOrEmpty())
                        {
                            var labelcell = new TableCell();

                            labelcell.Controls.AddRange(labels);
                            row.Controls.Add(labelcell);
                        }

                        itemcell = new TableCell();
                    }

                    itemcell.Controls.AddRange(items);
                    if (i == panelrow.Items.Count() - 1) itemcell.ColumnSpan = cellsperrow - i;

                    row.Controls.Add(itemcell);
                }

                table.Rows.Add(row);
            }

            return table;
        }

        private IEnumerable<Control> RenderItem(PanelItem panelItem)
        {
            foreach (var renderer in Renderers)
            {
                if (renderer.CanRender(panelItem.Type)) return (IEnumerable<Control>) renderer.Render(panelItem);
            }
            return new List<Control>();
        }

        private IEnumerable<Control> RenderLabel(PanelItem panelItem)
        {
            var controls = new List<Control>();

            if (!panelItem.Label.IsNullOrEmpty())
            {
                var label = new Label { ID = panelItem.GetLabelId(), Text = ResourceManager.GetString(panelItem.Label), Visible = panelItem.Visible };

                label.AddStyle(CssClass.Label);
                
                controls.Add(label);
                panelItem.IDs.Add(label.ID);

                if (!panelItem.Optional)
                {
                    var asterix = new Label { ID = panelItem.GetLabelId() + "Asterix", Text = @" *", CssClass = "Mandatory", Visible = panelItem.Visible };

                    controls.Add(asterix);
                    panelItem.IDs.Add(asterix.ID);
                }
            }

            return controls;
        }
    }
}
