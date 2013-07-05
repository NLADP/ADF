using System.Linq;
using Adf.Core.Grids;

namespace Adf.Base.Grids
{
    public static class GridControlExtensions
    {
        public static GridItem LastItem(this GridControl grid)
        {
            return grid.Items.Last();
        }
    }
}
