using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Panels;
using Adf.Web.UI;

namespace Adf.Web.Panels
{
    public class TreeViewRenderer  : BaseRenderer, IPanelItemRenderer
    {
        public bool CanRender(PanelItemType type)
        {
            return type.IsIn(PanelItemType.TreeView);
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
