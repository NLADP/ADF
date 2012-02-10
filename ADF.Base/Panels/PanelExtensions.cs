using Adf.Core.Panels;

namespace Adf.Base.Panels
{
    public static class PanelExtensions
    {
        public static P InRow<P>(this P panel) where P : AdfPanel
        {
            panel.Rows.Add(new PanelRow(panel) { Panel = panel });

            return panel;
        }

        public static P OnlyUseCustomLabels<P>(this P panel) where P : AdfPanel
        {
            panel.AutoGenerateLabels = false;

            return panel;
        }

        public static P WithItemsPerRow<P>(this P panel, int numberofitems = 1) where P : AdfPanel
        {
            panel.ItemsPerRow = numberofitems;

            return panel;
        }
        
        public static P WithDefaultWidth<P>(this P panel, int defaultwidth = 50) where P : AdfPanel
        {
            panel.ItemsPerRow = defaultwidth;

            return panel;
        }
    }
}
