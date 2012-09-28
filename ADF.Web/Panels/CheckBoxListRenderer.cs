using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Panels;

namespace Adf.Web.Panels
{
    public class CheckBoxListRenderer : BaseRenderer, IPanelItemRenderer
    {
        public bool CanRender(PanelItemType type)
        {
            return type.IsIn(PanelItemType.CheckBoxList);
        }

        public IEnumerable<object> Render(PanelItem panelItem)
        {
            var list = new CheckBoxList { ID = panelItem.GetId(), Enabled = panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), CssClass = ItemStyle, Visible = panelItem.Visible };

            list.AttachToolTip(panelItem);

            panelItem.Target = list;

            return new List<Control> { list, PanelValidator.Create(panelItem) };
        }
    }
}
