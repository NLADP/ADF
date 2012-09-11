using System.Linq;
using Adf.Core.Authorization;
using Adf.Core.Tasks;

namespace Adf.Core.Menu
{
    public static class MenuObjectExtensions
    {
        public static MenuObject Add(this MenuObject menu, string title, string tip = "")
        {
            var sub = new MenuObject(title, null, string.Empty, tip) { Parent = menu };

            menu.SubMenus.Add(sub);

            return sub;
        }
        
        public static MenuObject Add(this MenuObject menu, string title, ApplicationTask task, string topic = "", string tip = "")
        {
            var sub = new MenuObject(title, task, topic, tip) { Parent = menu };

            menu.SubMenus.Add(sub);

            return menu;
        }
        
        public static MenuObject AddLast(this MenuObject menu, string title, ApplicationTask task, string topic = "", string tip = "")
        {
            var sub = new MenuObject(title, task, topic, tip) { Parent = menu };

            menu.SubMenus.Add(sub);

            return menu.Parent;
        }

        public static MenuObject Up(this MenuObject menu)
        {
            return menu.Parent;
        }

        public static void Authorize(this MenuObject menu, IAction action)
        {
            foreach (var sub in menu.SubMenus)
            {
                sub.Authorize(action);
            }

            // Menu is enabled if one of it submenu's is, or if a task is associated with either an empty Subject or one that is allowed.
            menu.Enabled = (menu.SubMenus.Any(m => m.Enabled) 
                || (menu.Task != null && (menu.Task.Subject == null || AuthorizationManager.IsAllowed(menu.Task.Subject.Name, action))));
        }
    }
}
