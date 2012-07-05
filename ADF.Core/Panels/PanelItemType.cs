namespace Adf.Core.Panels
{
    public class PanelItemType : Descriptor
    {
        public string Prefix { get; set; }
        public PanelItemType(string prefix, string name) : base(name)
        {
            Prefix = prefix;
        }

        public static readonly PanelItemType Unknown = new PanelItemType("non", "Unknown");
        public static readonly PanelItemType EditableText = new PanelItemType("txt", "EditableText");
        public static readonly PanelItemType Password = new PanelItemType("txt", "Password");
        public static readonly PanelItemType NonEditableText = new PanelItemType("txt", "NonEditableText");
        public static readonly PanelItemType Label = new PanelItemType("lbl", "Label");
        public static readonly PanelItemType MultiLineText = new PanelItemType("txt", "MultiLineText");
        public static readonly PanelItemType Calendar = new PanelItemType("dtb", "Calendar");
        public static readonly PanelItemType CheckBox = new PanelItemType("cbx", "CheckBox");
        public static readonly PanelItemType DropDown = new PanelItemType("ddl", "DropDown");
        public static readonly PanelItemType Image = new PanelItemType("img", "Image");
        public static readonly PanelItemType Link = new PanelItemType("lnk", "Link");
        public static readonly PanelItemType InfoIcon = new PanelItemType("icn", "InfoIcon");
        public static readonly PanelItemType TreeView = new PanelItemType("tv", "TreeView");
        public static readonly PanelItemType FileUpload = new PanelItemType("upl", "FileUpload");
        public static readonly PanelItemType RadioButtonList = new PanelItemType("rbl", "RadioButtonList");
    }
}
