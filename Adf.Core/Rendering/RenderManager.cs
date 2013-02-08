using System.Collections.Generic;
using Adf.Core.Objects;
using Adf.Core.Panels;

namespace Adf.Core.Rendering
{
    public static class RenderManager
    {
        private static readonly object Lock = new object();
        private static readonly Dictionary<RenderType, IRenderer> Renderers = new Dictionary<RenderType, IRenderer>();

        private static IRenderer GetRenderer(RenderType type)
        {
            IRenderer renderer;

            if (!Renderers.TryGetValue(type, out renderer))
            {
                lock (Lock) Renderers[type] = renderer = ObjectFactory.BuildUp<IRenderer>(type.Name);
            }

            return renderer;
        }

        public static object Render(RenderType type, PanelObject panel)
        {
            return GetRenderer(type).Render(panel);
        }
    }
}
