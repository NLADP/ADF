using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Base.Panels;
using Adf.Core.Panels;
using Adf.Web.UI;

namespace Adf.Web.Panels
{
    public class CheckBoxRenderer : BaseRenderer, IPanelItemRenderer
    {
        public bool CanRender(PanelItemType type)
        {
            var types = new[] { PanelItemType.CheckBox };

            return types.Contains(type);
        }

        public IEnumerable<object> Render(PanelItem panelItem)
        {
            var validator = SmartValidator.Create(panelItem.GetId());
            var checkBox = new CheckBox { ID = panelItem.GetId(), Enabled = panelItem.Editable, CssClass = ItemStyle };

            panelItem.Target = checkBox;

            return new List<Control> { checkBox, validator };
        }
    }
}
