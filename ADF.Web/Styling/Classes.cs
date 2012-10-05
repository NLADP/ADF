using Adf.Core;

namespace Adf.Web.Styling
{
    public class CssClass : Descriptor
    {
        public static readonly CssClass Editable = new CssClass("IsEditable");
        public static readonly CssClass ReadOnly = new CssClass("IsReadOnly");
        public static readonly CssClass Error = new CssClass("HasError");
        public static readonly CssClass Label = new CssClass("Label");
        public static readonly CssClass Item = new CssClass("Item");

        public CssClass(string name) : base(name) {}
    }
}
