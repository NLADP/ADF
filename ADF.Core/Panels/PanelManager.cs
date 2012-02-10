using Adf.Core.Objects;

namespace Adf.Core.Panels
{
    public static class PanelManager
    {
        private static IPanelRenderer _renderer;

        private static readonly object _lock = new object();

        internal static IPanelRenderer Renderer
        {
            get { lock (_lock) return _renderer ?? (_renderer = ObjectFactory.BuildUp<IPanelRenderer>()); }
        }

        public static object Render(AdfPanel panel)
        {
            return Renderer.Render(panel);
        }
    }
}
