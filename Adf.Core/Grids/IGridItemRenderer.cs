using System.Collections.Generic;

namespace Adf.Core.Grids
{
    public interface IGridItemRenderer
    {
        bool CanRender(GridItemType type);
        IEnumerable<object> Render(GridItem gridItem);
    }
}
