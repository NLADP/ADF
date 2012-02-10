using System.Collections.Generic;

namespace Adf.Core.Panels
{
    public class PanelRow 
    {
        private readonly List<PanelItem> items = new List<PanelItem>();

        public AdfPanel Panel { get; set; }
        public List<PanelItem> Items { get { return items; } } 

        public PanelRow(AdfPanel panel)
        {
            Panel = panel;
        }

        public void Add(PanelItem item)
        {
            item.Row = this;
            items.Add(item);
        }
    }
}
