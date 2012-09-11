using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Base.Panels;
using Adf.Core.Extensions;
using Adf.Core.Panels;
using Adf.Web.UI;

namespace Adf.Web.Panels
{
    public class RadioButtonListRenderer : BaseRenderer, IPanelItemRenderer
    {
        public bool CanRender(PanelItemType type)
        {
            return type.IsIn(PanelItemType.RadioButtonList);
        }

        public IEnumerable<object> Render(PanelItem panelItem)
        {
            var list = new RadioButtonList { ID = panelItem.GetId(), Enabled = panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), CssClass = ItemStyle, Visible = panelItem.Visible };

            list.AttachToolTip(panelItem);

            panelItem.Target = list;

            return new List<Control> { list, PanelValidator.Create(panelItem) };
        }
    }
}
