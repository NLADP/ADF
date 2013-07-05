using Adf.Core.Panels;

namespace Adf.Base.Panels
{
    public static class PanelExtensions
    {
        public static P InRow<P>(this P panel) where P : PanelObject
        {
            panel.Add(new PanelRow());

            return panel;
        }

        public static P OnlyUseCustomLabels<P>(this P panel) where P : PanelObject
        {
            panel.AutoGenerateLabels = false;

            return panel;
        }

        public static P WithItemsPerRow<P>(this P panel, int numberofitems = 1) where P : PanelObject
        {
            panel.ItemsPerRow = numberofitems;

            return panel;
        }
        
        public static P WithDefaultWidth<P>(this P panel, int defaultwidth = 50) where P : PanelObject
        {
            panel.ItemsPerRow = defaultwidth;

            return panel;
        }
    }
}
