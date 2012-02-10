using System.Collections.Generic;
using System.Linq;

namespace Adf.Core.Panels
{
    public class AdfPanel 
    {
        private readonly List<PanelRow> rows = new List<PanelRow>();
        public int DefaultWidth { get; set;  }
        public int ItemsPerRow { get; set; }
        public bool AutoGenerateLabels { get; set; }

        public AdfPanel()
        {
            AutoGenerateLabels = true;
            ItemsPerRow = 1;
            DefaultWidth = 50;
        }

        public List<PanelRow> Rows { get { return rows; } }

        public void Add(PanelRow row)
        {
            row.Panel = this;
            rows.Add(row);
        }

        public int GetMaxItemsPerRow()
        {
            return Rows.Max(r => r.Items.Count);
        }
    }
}
