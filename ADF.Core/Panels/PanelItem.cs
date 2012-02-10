using System.Collections.Generic;
using System.Reflection;

namespace Adf.Core.Panels
{
    public class PanelItem
    {
        private readonly List<PanelItem> attacheditems = new List<PanelItem>();

        public PanelRow Row { get; set; }
        public PanelItemType Type { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Editable { get; set; }
        public bool Visible { get; set; }
        public bool Optional { get; set; }
        public string Label { get; set; }
        public string Text { get; set; }
        public MemberInfo Member { get; set; }
        public object Target { get; set; }
        public bool RequiresValidation { get; set; }
        public bool AttachToPrevious { get; set; }
        public List<PanelItem> AttachedItems { get { return attacheditems; } }

        public PanelItem(PanelRow row)
        {
            Row = row;
            Type = PanelItemType.EditableText;
            Width = Row.Panel.DefaultWidth;
            Height = 0;
            Editable = true;
            Visible = true;
            Optional = false;
            Label = string.Empty;
            Text = string.Empty;
            RequiresValidation = true;
            AttachToPrevious = false;
        }
    }
}
