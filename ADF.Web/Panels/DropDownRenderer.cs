using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Panels;
using Adf.Web.Styling;
using Adf.Web.UI.Extensions;

namespace Adf.Web.Panels
{
    public class DropDownRenderer : BaseRenderer, IPanelItemRenderer
    {
        public bool CanRender(PanelItemType type)
        {
            return type.IsIn(PanelItemType.DropDown);
        }

        public IEnumerable<object> Render(PanelItem panelItem)
        {
            var dropDownList = new DropDownList { ID = panelItem.GetId(), Enabled = panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), Visible = panelItem.Visible };

            dropDownList
                .AddStyle(CssClass.Item)
                .AttachToolTip(panelItem)
                .ToggleStyle(panelItem.Editable, CssClass.Editable, CssClass.ReadOnly);

            panelItem.Target = dropDownList;

            return new List<Control> { dropDownList, PanelValidator.Create(panelItem) };
        }
    }
}
