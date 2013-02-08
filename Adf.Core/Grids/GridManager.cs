using Adf.Core.Objects;

namespace Adf.Core.Grids
{
    public static class GridManager
    {
        private static IGridRenderer _renderer;

        private static readonly object Lock = new object();

        internal static IGridRenderer Renderer
        {
            get { lock (Lock) return _renderer ?? (_renderer = ObjectFactory.BuildUp<IGridRenderer>()); }
        }

        public static object Render(GridControl grid)
        {
            return Renderer.Render(grid);
        }
    }
}
