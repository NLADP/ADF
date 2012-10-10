using System;

namespace Adf.Core.Panels
{
    public static class PanelItemExtensions
    {
        public static string GetId(this PanelItem panelitem, string prefix = null)
        {
            prefix = prefix ?? panelitem.Type.Prefix;
            var type = panelitem.Member.ReflectedType.Name;

            return String.Format("{0}{1}{2}", prefix, type, panelitem.Member.Name);
        }

        public static string GetPropertyName(this PanelItem panelitem)
        {
            var type = panelitem.Member.ReflectedType.Name;

            return String.Format("{0}{1}", type, panelitem.Member.Name);
        }

        public static string GetLabelId(this PanelItem panelitem)
        {
            var type = panelitem.Member.ReflectedType.Name;

            return String.Format("_label{0}{1}{2}", type, panelitem.Member.Name, panelitem.Alias);
        }
    }
}
