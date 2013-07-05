using System.Collections.Generic;

namespace Adf.Core.Panels
{
    public class PanelRow 
    {
        private readonly List<PanelItem> _items = new List<PanelItem>();

        public PanelObject Panel { get; set; }
        public List<PanelItem> Items { get { return _items; } } 

        public PanelRow() { }

        public PanelRow(PanelObject panel)
        {
            Panel = panel;
        }

        public void Add(PanelItem item)
        {
            item.Row = this;
            _items.Add(item);
        }
    }
}
