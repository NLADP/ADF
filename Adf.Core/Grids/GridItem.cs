using System.Reflection;

namespace Adf.Core.Grids
{
    public class GridItem
    {
        public GridControl Grid { get; set; }

        public GridItemType Type { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public string Header { get; set; }
        public MemberInfo Member { get; set; }

        public GridItem()
        {
            Type = GridItemType.Text;
            Header = string.Empty;
        }
    }
}
