using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Panels;

namespace Adf.Web.Panels
{
    public class BlankItemRenderer : BaseRenderer, IPanelItemRenderer
    {
        public bool CanRender(PanelItemType type)
        {
            return type.IsIn(PanelItemType.BlankItem);
        }

        public IEnumerable<object> Render(PanelItem panelItem)
        {
            var blank = new Label { Text = "&nbsp;" };

            return new List<Control> { blank };
        }
    }
}
