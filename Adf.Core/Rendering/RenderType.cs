namespace Adf.Core.Rendering
{
    public class RenderType : Descriptor
    {
        public RenderType(string name) : base(name) { }

        public static readonly RenderType Grid = new RenderType("Grid");
        public static readonly RenderType Panel = new RenderType("Panel");
        public static readonly RenderType Menu = new RenderType("Menu");
    }
}
