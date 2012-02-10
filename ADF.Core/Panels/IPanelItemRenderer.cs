using System.Collections.Generic;

namespace Adf.Core.Panels
{
    public interface IPanelItemRenderer
    {
        bool CanRender(PanelItemType type);
        IEnumerable<object> Render(PanelItem panelItem);
    }
}
