using System.Collections.Generic;

namespace Adf.Core.Grids
{
    public class GridControl
    {
        private readonly List<GridItem> _items = new List<GridItem>();

        public List<GridItem> Items { get { return _items; } }
        public bool AutoGenerateHeaders { get; set; }

        public GridControl()
        {
            AutoGenerateHeaders = true;
        }

        public void Add(GridItem item)
        {
            item.Grid = this;
            _items.Add(item);
        }
    }
}
