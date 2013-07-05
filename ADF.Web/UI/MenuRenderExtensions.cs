using System.Linq;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Menu;
using Adf.Core.Resources;

namespace Adf.Web.UI
{
    public static class MenuRenderExtensions
    {
        public static void Render(this Menu control, MenuObject root)
        {
            foreach (var sub in root.SubMenus.Where(sub => sub.Enabled))
            {
                control.Items.Add(sub.Render());
            }
        }

        private static MenuItem Render(this MenuObject menu)
        {
            var menuitem = new MenuItem
            {
                Text = ResourceManager.GetString(menu.Title),
                Value = (!menu.Topic.IsNullOrEmpty()) ? menu.Topic : (menu.Task != null) ? menu.Task.Name : menu.Title,
                ToolTip = menu.Tip,
                Selectable = !menu.Topic.IsNullOrEmpty() || menu.Task != null
            };

            foreach (var sub in menu.SubMenus.Where(sub => sub.Enabled))
            {
                menuitem.ChildItems.Add(sub.Render());
            }

            return menuitem;
        }
    }
}
