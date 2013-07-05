using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Panels;
using Adf.Core.Rendering;
using Adf.Web.Styling;
using Adf.Web.UI.Extensions;

namespace Adf.Web.Panels
{
    public class CheckBoxRenderer : BaseRenderer, IItemRenderer
    {
        public bool CanRender(RenderItemType type)
        {
            return type.IsIn(RenderItemType.CheckBox);
        }

        public IEnumerable<object> Render(PanelItem panelItem)
        {
            var checkBox = new CheckBox { ID = panelItem.GetId(), Enabled = panelItem.Editable, Visible = panelItem.Visible};

            checkBox
                .AddStyle(CssClass.Item)
                .AttachToolTip(panelItem)
                .ToggleStyle(panelItem.Editable, CssClass.Editable, CssClass.ReadOnly);

            panelItem.Target = checkBox;

            return new List<Control> { checkBox, PanelValidator.Create(panelItem) };
        }
    }
}
