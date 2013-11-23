using Adf.Core;

namespace Adf.Web.Styling
{
    public class CssClass : Descriptor
    {
        public static readonly CssClass Editable = new CssClass("IsEditable");
        public static readonly CssClass NonEditable = new CssClass("IsNotEditable");
        public static readonly CssClass ReadOnly = new CssClass("IsReadOnly");
        public static readonly CssClass Error = new CssClass("HasError");
        public static readonly CssClass Label = new CssClass("Label");
        public static readonly CssClass Header = new CssClass("Heading");
        public static readonly CssClass HelpIcon = new CssClass("HelpIcon");
        public static readonly CssClass Item = new CssClass("Item");
        public static readonly CssClass Panel = new CssClass("Panel");
        public static readonly CssClass Row = new CssClass("Row");
        public static readonly CssClass ViewRow = new CssClass("ViewRow");
        public static readonly CssClass Cell = new CssClass("Cell");

        private CssClass(string name) : base(name) {}
    }
}
