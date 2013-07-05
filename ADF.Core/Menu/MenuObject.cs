using System.Collections.Generic;
using Adf.Core.Tasks;

namespace Adf.Core.Menu
{
    public class MenuObject
    {
        public string Title { get; protected set; }
        public ApplicationTask Task { get; protected set; }
        public string Tip { get; protected set; }
        public string Topic { get; protected set; }
        public bool Enabled { get; set; }

        public MenuObject Parent { get; set; }
        public List<MenuObject> SubMenus { get; protected set; }
        

        public MenuObject(string title, ApplicationTask task = null, string topic = "", string tip = "")
        {
            Title = title;
            Task = task;
            Topic = topic;
            Tip = tip;
            Enabled = false;
            SubMenus = new List<MenuObject>();
        }

        public static MenuObject Root()
        {
            var root = new MenuObject("Root");

            root.Parent = root;

            return root;
        }
    }
}