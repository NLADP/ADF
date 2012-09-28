using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Panels;

namespace Adf.Web.Panels
{
    public class CheckBoxRenderer : BaseRenderer, IPanelItemRenderer
    {
        public bool CanRender(PanelItemType type)
        {
            return type.IsIn(PanelItemType.CheckBox);
        }

        public IEnumerable<object> Render(PanelItem panelItem)
        {
            var checkBox = new CheckBox { ID = panelItem.GetId(), Enabled = panelItem.Editable, CssClass = ItemStyle, Visible = panelItem.Visible};

            checkBox.AttachToolTip(panelItem);

            panelItem.Target = checkBox;

            return new List<Control> { checkBox, PanelValidator.Create(panelItem) };
        }
    }
}
