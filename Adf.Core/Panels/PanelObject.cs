using System.Collections.Generic;
using System.Linq;

namespace Adf.Core.Panels
{
    public class PanelObject 
    {
        private readonly List<PanelRow> _rows = new List<PanelRow>();
        public int DefaultWidth { get; set;  }
        public short TabStart { get; set; }
        public short TabIncrement { get; set; }
        public int ItemsPerRow { get; set; }
        public bool AutoGenerateLabels { get; set; }

        public PanelObject()
        {
            AutoGenerateLabels = true;
            ItemsPerRow = 1;
            DefaultWidth = 50;
            TabStart = 1;
            TabIncrement = 3;
        }

        public List<PanelRow> Rows { get { return _rows; } }

        public void Add(PanelRow row)
        {
            row.Panel = this;
            _rows.Add(row);
        }

        public int GetMaxItemsPerRow()
        {
            return Rows.Max(r => r.Items.Count);
        }
    }
}
