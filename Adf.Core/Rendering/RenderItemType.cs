namespace Adf.Core.Rendering
{
    public class RenderItemType : Descriptor
    {
        public string Prefix { get; set; }
        public RenderItemType(string prefix, string name) : base(name)
        {
            Prefix = prefix;
        }

        public static readonly RenderItemType Unknown = new RenderItemType("non", "Unknown");
        public static readonly RenderItemType EditableText = new RenderItemType("txt", "EditableText");
        public static readonly RenderItemType Password = new RenderItemType("txt", "Password");
        public static readonly RenderItemType NonEditableText = new RenderItemType("txt", "NonEditableText");
        public static readonly RenderItemType Label = new RenderItemType("lbl", "Label");
        public static readonly RenderItemType Header = new RenderItemType("hdr", "Header");
        public static readonly RenderItemType MultiLineText = new RenderItemType("txt", "MultiLineText");
        public static readonly RenderItemType Calendar = new RenderItemType("dtb", "Calendar");
        public static readonly RenderItemType CheckBox = new RenderItemType("cbx", "CheckBox");
        public static readonly RenderItemType DropDown = new RenderItemType("ddl", "DropDown");
        public static readonly RenderItemType Image = new RenderItemType("img", "Image");
        public static readonly RenderItemType Link = new RenderItemType("lnk", "Link");
        public static readonly RenderItemType InfoIcon = new RenderItemType("icn", "InfoIcon");
        public static readonly RenderItemType TreeView = new RenderItemType("tv", "TreeView");
        public static readonly RenderItemType FileUpload = new RenderItemType("upl", "FileUpload");
        public static readonly RenderItemType RadioButtonList = new RenderItemType("rbl", "RadioButtonList");
        public static readonly RenderItemType CheckBoxList = new RenderItemType("cbl", "CheckBoxList");
    }
}
