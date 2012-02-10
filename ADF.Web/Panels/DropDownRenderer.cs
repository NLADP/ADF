using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Base.Panels;
using Adf.Core.Panels;
using Adf.Web.UI;

namespace Adf.Web.Panels
{
    public class DropDownRenderer : BaseRenderer, IPanelItemRenderer
    {
        public bool CanRender(PanelItemType type)
        {
            var types = new[] { PanelItemType.DropDown };

            return types.Contains(type);
        }

        public IEnumerable<object> Render(PanelItem panelItem)
        {
            var validator = SmartValidator.Create(panelItem.GetId());
            var dropDownList = new DropDownList { ID = panelItem.GetId(), Enabled = panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), CssClass = ItemStyle };

            panelItem.Target = dropDownList;

            return new List<Control> { dropDownList, validator };

        }
    }
}
