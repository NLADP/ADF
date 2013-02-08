using Adf.Core.Panels;

namespace Adf.Core.Rendering
{
    public interface IRenderer
    {
        object Render(PanelObject panel);
    }
}
