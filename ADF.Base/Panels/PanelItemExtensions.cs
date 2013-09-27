using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Adf.Base.Validation;
using Adf.Core.Extensions;
using Adf.Core.Panels;
using Adf.Core.Rendering;

namespace Adf.Base.Panels
{
    public static class PanelItemExtensions
    {
        public static PanelItem LastItem(this PanelObject panel)
        {
            return panel.Rows.Last().Items.Last();
        }

        private static void AddPanelItem<P>(this P panel, bool editable = true, bool optional = false, bool visible = true) where P : PanelObject
        {
            var row = panel.Rows.Last();

            var panelItem = new PanelItem(row) {Editable = editable, Optional = optional, Visible = visible};

            row.Add(panelItem);
        }

        public static P AsText<P>(this P panel) where P : PanelObject
        {
            panel.LastItem().Type = RenderItemType.EditableText;

            return panel;
        }

        public static P AsNonEditableText<P>(this P panel) where P : PanelObject
        {
            panel.LastItem().Type = RenderItemType.NonEditableText;
            panel.LastItem().Editable = false;

            return panel;
        }
        
        public static P AsLabel<P>(this P panel) where P : PanelObject
        {
            panel.LastItem().Type = RenderItemType.Label;

            return panel;
        }
        
        public static P AsHeader<P>(this P panel) where P : PanelObject
        {
            panel.LastItem().Type = RenderItemType.Header;

            return panel;
        }

        public static PanelObject ShowHeader(this PanelObject panel, string label, int? width = null)
        {
            panel.Show();

            var panelItem = panel.LastItem();

            panelItem.Type = RenderItemType.Header;

            if (width != null) panelItem.Width = width.Value;
            if (label != null) panelItem.Label = label;

            return panel;
        }

        public static PanelObject ShowLabel<TDomainObject>(this PanelObject panel, Expression<Func<TDomainObject, object>> property, string label = null, int? width = null)
        {
            panel.Show(property);

            var panelItem = panel.LastItem();

            panelItem.Type = RenderItemType.Label;
            panelItem.Optional = true;

            if (width != null) panelItem.Width = width.Value;
            if (label != null) panelItem.Label = label;

            return panel;
        }

        public static PanelObject ShowTextBox<TDomainObject>(this PanelObject panel, Expression<Func<TDomainObject, object>> property, string label = null, int? width = null, bool? mandatory = true, bool? editable = true)
        {
            panel.CreateItem(RenderItemType.EditableText, property, label, width, mandatory, editable);

            return panel;
        }

        public static PanelObject ShowMultiLineTextBox<TDomainObject>(this PanelObject panel, Expression<Func<TDomainObject, object>> property, string label = null, int? width = null, int? height = null, bool? mandatory = true, bool? editable = true)
        {
            panel.CreateItem(RenderItemType.MultiLineText, property, label, width, mandatory, editable);

            if (height.HasValue) panel.LastItem().Height = height.Value;

            return panel;
        }

        public static PanelObject ShowNonEditable<TDomainObject>(this PanelObject panel, Expression<Func<TDomainObject, object>> property, string label = null, int? width = null)
        {
            panel.CreateItem(RenderItemType.NonEditableText, property, label, width, false, false);

            return panel;
        }

        public static PanelObject ShowCheckBox<TDomainObject>(this PanelObject panel, Expression<Func<TDomainObject, object>> property, string label = null, int? width = null, bool? mandatory = true, bool? editable = true)
        {
            panel.CreateItem(RenderItemType.CheckBox, property, label, width, mandatory, editable);

            return panel;
        }

        public static PanelObject ShowColorPicker<TDomainObject>(this PanelObject panel, Expression<Func<TDomainObject, object>> property, string label = null, int? width = null, bool? mandatory = true, bool? editable = true)
        {
            panel.CreateItem(RenderItemType.ColorPicker, property, label, width, mandatory, editable);

            return panel;
        }

        public static PanelObject ShowCheckBoxList<TDomainObject>(this PanelObject panel, Expression<Func<TDomainObject, object>> property, string label = null, int? width = null, bool? mandatory = true, bool? editable = true)
        {
            panel.CreateItem(RenderItemType.CheckBoxList, property, label, width, mandatory, editable);

            return panel;
        }

        public static PanelObject ShowInfoIcon<TDomainObject>(this PanelObject panel, Expression<Func<TDomainObject, object>> property, string label = null, int? width = 16, bool? mandatory = false, bool? editable = false)
        {
            panel.CreateItem(RenderItemType.InfoIcon, property, label, width, mandatory, editable);
            panel.WithPrevious();

            return panel;
        }


        public static PanelObject ShowCalendar<TDomainObject>(this PanelObject panel, Expression<Func<TDomainObject, object>> property, string label = null, int? width = 10, bool? mandatory = true, bool? editable = true)
        {
            panel.CreateItem(RenderItemType.Calendar, property, label, width, mandatory, editable);

            return panel;
        }

        private static void CreateItem<TDomainObject>(this PanelObject panel, RenderItemType itemType, Expression<Func<TDomainObject, object>> property, string label, int? width, bool? mandatory, bool? editable)
        {
            panel.Show(property);

            var panelItem = panel.LastItem();

            panelItem.Type = itemType;
            panelItem.Optional = true;

            if (width != null) panelItem.Width = width.Value;
            if (label != null) panelItem.Label = label;
            if (mandatory != null) panelItem.Optional = !mandatory.Value;
            if (editable != null) panelItem.Editable = editable.Value;
        }

        public static P AsCheckBox<P>(this P panel) where P : PanelObject
        {
            panel.LastItem().Type = RenderItemType.CheckBox;

            return panel;
        }
        
        public static P AsDropDown<P>(this P panel) where P : PanelObject
        {
            panel.LastItem().Type = RenderItemType.DropDown;

            return panel;
        }

        public static PanelObject ShowDropDown<TDomainObject>(this PanelObject panel, Expression<Func<TDomainObject, object>> property, string label = null, int? width = null)
        {
            panel.Show(property);

            var panelItem = panel.LastItem();

            panelItem.Type = RenderItemType.DropDown;

            if (width != null) panelItem.Width = width.Value;
            if (label != null) panelItem.Label = label;

            return panel;
        }

        public static PanelObject ShowRadioButtonList<TDomainObject>(this PanelObject panel, Expression<Func<TDomainObject, object>> property, string label = null, int? width = null)
        {
            panel.Show(property);

            var panelItem = panel.LastItem();

            panelItem.Type = RenderItemType.RadioButtonList;

            if (width != null) panelItem.Width = width.Value;
            if (label != null) panelItem.Label = label;

            return panel;
        }

        public static P AsCalender<P>(this P panel) where P : PanelObject
        {
            panel.LastItem().Type = RenderItemType.Calendar;
            panel.LastItem().Width = 10;

            return panel;
        }

        public static P AsImage<P>(this P panel) where P : PanelObject
        {
            panel.LastItem().Type = RenderItemType.Image;
            panel.LastItem().Width = 16;

            return panel;
        }

        public static P AsFileSelect<P>(this P panel) where P : PanelObject
        {
            panel.LastItem().Type = RenderItemType.FileUpload;

            return panel;
        }

        public static P AsTreeView<P>(this P panel) where P : PanelObject
        {
            panel.LastItem().Type = RenderItemType.TreeView;

            return panel;
        }

        public static P AsInfoIcon<P>(this P panel, string text = null) where P : PanelObject
        {
            panel.LastItem().Type = RenderItemType.InfoIcon;
            panel.LastItem().Width = 16;
            panel.LastItem().Text = text;

            return panel;
        }


        public static P AsLink<P>(this P panel) where P : PanelObject
        {
            panel.LastItem().Type = RenderItemType.Link;

            return panel;
        }

        public static P AsMultiLineText<P>(this P panel) where P : PanelObject
        {
            panel.LastItem().Type = RenderItemType.MultiLineText;

            return panel;
        }

        public static P WithLabel<P>(this P panel, string label) where P : PanelObject
        {
            panel.LastItem().Label = label;

            return panel;
        }

        public static P WithTooltip<P>(this P panel, string tooltip) where P : PanelObject
        {
            panel.LastItem().ToolTip = tooltip;

            return panel;
        }

        public static P NoLabel<P>(this P panel) where P : PanelObject
        {
            panel.LastItem().Label = string.Empty;

            return panel;
        }

        public static P Width<P>(this P panel, int width) where P : PanelObject
        {
            panel.LastItem().Width = width;

            return panel;
        }

        public static P Height<P>(this P panel, int height) where P : PanelObject
        {
            panel.LastItem().Height = height;

            return panel;
        }

        public static P IsOptional<P>(this P panel) where P : PanelObject
        {
            panel.LastItem().Optional = true;

            return panel;
        }

        public static P WithAlias<P>(this P panel, string alias) where P : PanelObject
        {
            panel.LastItem().Alias = alias;

            return panel;
        }


        public static P RequiresNoValidation<P>(this P panel) where P : PanelObject
        {
            panel.LastItem().RequiresValidation = false;

            return panel;
        }

        public static PanelObject Show<D>(this PanelObject panel, Expression<Func<D, object>> expression)
        {
            panel.AddPanelItem();

            var panelItem = panel.LastItem();
            panelItem.Member = expression.GetMemberInfo();
            panelItem.ReflectedType = expression.GetMemberType();
            panelItem.Optional = !panelItem.Member.IsDefined(typeof(NonEmptyAttribute), false);
            if (panel.AutoGenerateLabels) panelItem.Label = expression.GetMemberInfo().Name;

            var maxlength = panelItem.Member.GetCustomAttributes(typeof(MaxLengthAttribute), false).FirstOrDefault() as MaxLengthAttribute;
            if (maxlength != null) panelItem.MaxLength = maxlength.Length;

            return panel;
        }
        
        public static PanelObject Show(this PanelObject panel)
        {
            panel.AddPanelItem();

            panel.LastItem().Member = null;
            panel.LastItem().Optional = panel.LastItem().Member != null && !panel.LastItem().Member.IsDefined(typeof(NonEmptyAttribute), false);
            if (panel.AutoGenerateLabels) panel.LastItem().Label = "";

            MaxLengthAttribute maxlength = null; 
            if (panel.LastItem().Member != null) {maxlength = panel.LastItem().Member.GetCustomAttributes(typeof(MaxLengthAttribute), false).FirstOrDefault() as MaxLengthAttribute;}
            if (maxlength != null) panel.LastItem().MaxLength = maxlength.Length;

            return panel;
        }

        public static P WithPrevious<P>(this P panel) where P : PanelObject
        {
            panel.LastItem().AttachToPrevious = true;

            return panel;
        }

        public static P AssignTo<P>(this P panel, out PanelItem target) where P : PanelObject
        {
            target = panel.LastItem();

            return panel;
        }

        public static P Tab<P>(this P panel, short tabindex) where P : PanelObject
        {
            panel.LastItem().Tab = tabindex;

            return panel;
        }


        public static PanelObject WithTab(this PanelObject panel, short tabstart)
        {
            return panel.WithTab(tabstart, panel.TabIncrement);
        }

        public static PanelObject WithTab(this PanelObject panel, short tabstart, short tabincrement)
        {
            panel.TabStart = tabstart;
            panel.TabIncrement = tabincrement;

            return panel;
        }

    }
}
