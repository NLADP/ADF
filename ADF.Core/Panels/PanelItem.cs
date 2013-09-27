using System;
using System.Collections.Generic;
using System.Reflection;
using Adf.Core.Rendering;

namespace Adf.Core.Panels
{
    public class PanelItem
    {
        private readonly List<PanelItem> attacheditems = new List<PanelItem>();

        public PanelRow Row { get; set; }
        public RenderItemType Type { get; set; }
        public short Tab { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int MaxLength { get; set; }
        public bool Editable { get; set; }
        public bool Visible { get; set; }
        public bool Optional { get; set; }
        public string Label { get; set; }
        public string Text { get; set; }
        public string Alias { get; set; }
        public MemberInfo Member { get; set; }
        public Type ReflectedType { get; set; }
        public bool RequiresValidation { get; set; }
        public bool AttachToPrevious { get; set; }
        public List<PanelItem> AttachedItems { get { return attacheditems; } }
        public string ToolTip { get; set; }
        public List<string> IDs;

        private object _target;
        public object Target
        {
            get { return _target; }
            set { _target = value; IDs.Add(this.GetId()); }
        }

        public PanelItem(PanelRow row)
        {
            Row = row;
            Type = RenderItemType.EditableText;
            Width = Row.Panel.DefaultWidth;
            Height = 0;
            MaxLength = 0;
            Editable = true;
            Visible = true;
            Optional = false;
            Tab = 0;
            Label = string.Empty;
            Text = string.Empty;
            RequiresValidation = true;
            AttachToPrevious = false;
            ToolTip = string.Empty;
            IDs = new List<string>();
        }
    }
}
