using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Panels;
using Adf.Core.Rendering;
using Adf.Web.UI;
using Adf.Web.UI.Extensions;

namespace Adf.Web.Panels
{
    public class TreeViewRenderer  : BaseRenderer, IItemRenderer
    {
        public bool CanRender(RenderItemType type)
        {
            return type.IsIn(RenderItemType.TreeView);
        }

        public IEnumerable<object> Render(PanelItem panelItem)
        {
            var dropdowntreeview = new DropDownTreeView 
            { 
                ID = panelItem.GetId(),
                Enabled = false,
                CssClass = "AdfPanelItem AdfTreeView",
                Width = new Unit(panelItem.Width, UnitType.Ex),
                Visible = panelItem.Visible
            };

            dropdowntreeview.AttachToolTip(panelItem);

            panelItem.Target = dropdowntreeview;

            return new List<Control> { dropdowntreeview, PanelValidator.Create(panelItem) };            
        }
    }
}
