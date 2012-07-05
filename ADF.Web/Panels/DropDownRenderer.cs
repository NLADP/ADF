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
    public class DropDownRenderer : BaseRenderer, IPanelItemRenderer
    {
        public bool CanRender(PanelItemType type)
        {
            return type.IsIn(PanelItemType.DropDown);
        }

        public IEnumerable<object> Render(PanelItem panelItem)
        {
            var dropDownList = new DropDownList { ID = panelItem.GetId(), Enabled = panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), CssClass = ItemStyle, Visible = panelItem.Visible };

            dropDownList.AttachToolTip(panelItem);

            panelItem.Target = dropDownList;

            return new List<Control> { dropDownList, PanelValidator.Create(panelItem) };

        }
    }
}
