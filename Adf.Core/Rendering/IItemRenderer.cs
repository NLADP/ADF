using System.Collections.Generic;
using Adf.Core.Panels;

namespace Adf.Core.Rendering
{
    public interface IItemRenderer
    {
        bool CanRender(RenderItemType type);
        IEnumerable<object> Render(PanelItem panelItem);
    }
}
