using System;
using System.Linq;
using System.Linq.Expressions;
using Adf.Base.Validation;
using Adf.Core.Extensions;
using Adf.Core.Panels;

namespace Adf.Base.Panels
{
    public static class PanelItemExtensions
    {
        public static PanelItem LastItem(this AdfPanel panel)
        {
            return panel.Rows.Last().Items.Last();
        }

        private static void AddPanelItem<P>(this P panel, bool editable = true, bool optional = false, bool visible = true) where P : AdfPanel
        {
            var row = panel.Rows.Last();

            var panelItem = new PanelItem(row) {Editable = editable, Optional = optional, Visible = visible};

            row.Add(panelItem);
        }

        public static P AsText<P>(this P panel) where P : AdfPanel
        {
            panel.LastItem().Type = PanelItemType.EditableText;

            return panel;
        }

        public static P AsNonEditableText<P>(this P panel) where P : AdfPanel
        {
            panel.LastItem().Type = PanelItemType.NonEditableText;
            panel.LastItem().Editable = false;

            return panel;
        }
        
        public static P AsLabel<P>(this P panel) where P : AdfPanel
        {
            panel.LastItem().Type = PanelItemType.Label;

            return panel;
        }

        public static AdfPanel ShowLabel<TDomainObject>(this AdfPanel panel, Expression<Func<TDomainObject, object>> property, string label = null, int? width = null)
        {
            panel.Show(property);

            var panelItem = panel.LastItem();

            panelItem.Type = PanelItemType.Label;
            panelItem.Optional = true;

            if (width != null) panelItem.Width = width.Value;
            if (label != null) panelItem.Label = label;

            return panel;
        }

        public static AdfPanel ShowTextBox<TDomainObject>(this AdfPanel panel, Expression<Func<TDomainObject, object>> property, string label = null, int? width = null, bool? mandatory = true, bool? editable = true)
        {
            panel.CreateItem(PanelItemType.EditableText, property, label, width, mandatory, editable);

            return panel;
        }

        public static AdfPanel ShowCheckBox<TDomainObject>(this AdfPanel panel, Expression<Func<TDomainObject, object>> property, string label = null, int? width = null, bool? mandatory = true, bool? editable = true)
        {
            panel.CreateItem(PanelItemType.CheckBox, property, label, width, mandatory, editable);

            return panel;
        }

        public static AdfPanel ShowCheckBoxList<TDomainObject>(this AdfPanel panel, Expression<Func<TDomainObject, object>> property, string label = null, int? width = null, bool? mandatory = true, bool? editable = true)
        {
            panel.CreateItem(PanelItemType.CheckBoxList, property, label, width, mandatory, editable);

            return panel;
        }

        public static AdfPanel ShowInfoIcon<TDomainObject>(this AdfPanel panel, Expression<Func<TDomainObject, object>> property, string label = null, int? width = 16, bool? mandatory = false, bool? editable = false)
        {
            panel.CreateItem(PanelItemType.InfoIcon, property, label, width, mandatory, editable);

            return panel;
        }


        public static AdfPanel ShowCalendar<TDomainObject>(this AdfPanel panel, Expression<Func<TDomainObject, object>> property, string label = null, int? width = 10, bool? mandatory = true, bool? editable = true)
        {
            panel.CreateItem(PanelItemType.Calendar, property, label, width, mandatory, editable);

            return panel;
        }

        private static void CreateItem<TDomainObject>(this AdfPanel panel, PanelItemType itemType, Expression<Func<TDomainObject, object>> property, string label, int? width, bool? mandatory, bool? editable)
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

        public static P AsCheckBox<P>(this P panel) where P : AdfPanel
        {
            panel.LastItem().Type = PanelItemType.CheckBox;

            return panel;
        }
        
        public static P AsDropDown<P>(this P panel) where P : AdfPanel
        {
            panel.LastItem().Type = PanelItemType.DropDown;

            return panel;
        }

        public static AdfPanel ShowDropDown<TDomainObject>(this AdfPanel panel, Expression<Func<TDomainObject, object>> property, string label = null, int? width = null)
        {
            panel.Show(property);

            var panelItem = panel.LastItem();

            panelItem.Type = PanelItemType.DropDown;

            if (width != null) panelItem.Width = width.Value;
            if (label != null) panelItem.Label = label;

            return panel;
        }

        public static AdfPanel ShowRadioButtonList<TDomainObject>(this AdfPanel panel, Expression<Func<TDomainObject, object>> property, string label = null, int? width = null)
        {
            panel.Show(property);

            var panelItem = panel.LastItem();

            panelItem.Type = PanelItemType.RadioButtonList;

            if (width != null) panelItem.Width = width.Value;
            if (label != null) panelItem.Label = label;

            return panel;
        }

        public static P AsCalender<P>(this P panel) where P : AdfPanel
        {
            panel.LastItem().Type = PanelItemType.Calendar;
            panel.LastItem().Width = 10;

            return panel;
        }

        public static P AsImage<P>(this P panel) where P : AdfPanel
        {
            panel.LastItem().Type = PanelItemType.Image;
            panel.LastItem().Width = 16;

            return panel;
        }

        public static P AsFileSelect<P>(this P panel) where P : AdfPanel
        {
            panel.LastItem().Type = PanelItemType.FileUpload;

            return panel;
        }

        public static P AsTreeView<P>(this P panel) where P : AdfPanel
        {
            panel.LastItem().Type = PanelItemType.TreeView;

            return panel;
        }

        public static P AsInfoIcon<P>(this P panel, string text = null) where P : AdfPanel
        {
            panel.LastItem().Type = PanelItemType.InfoIcon;
            panel.LastItem().Width = 16;
            panel.LastItem().Text = text;

            return panel;
        }


        public static P AsLink<P>(this P panel) where P : AdfPanel
        {
            panel.LastItem().Type = PanelItemType.Link;

            return panel;
        }

        public static P AsMultiLineText<P>(this P panel) where P : AdfPanel
        {
            panel.LastItem().Type = PanelItemType.MultiLineText;

            return panel;
        }

        public static P WithLabel<P>(this P panel, string label) where P : AdfPanel
        {
            panel.LastItem().Label = label;

            return panel;
        }

        public static P WithTooltip<P>(this P panel, string tooltip) where P : AdfPanel
        {
            panel.LastItem().ToolTip = tooltip;

            return panel;
        }

        public static P NoLabel<P>(this P panel) where P : AdfPanel
        {
            panel.LastItem().Label = string.Empty;

            return panel;
        }

        public static P Width<P>(this P panel, int width) where P : AdfPanel
        {
            panel.LastItem().Width = width;

            return panel;
        }

        public static P Height<P>(this P panel, int height) where P : AdfPanel
        {
            panel.LastItem().Height = height;

            return panel;
        }

        public static P IsOptional<P>(this P panel) where P : AdfPanel
        {
            panel.LastItem().Optional = true;

            return panel;
        }

        public static P WithAlias<P>(this P panel, string alias) where P : AdfPanel
        {
            panel.LastItem().Alias = alias;

            return panel;
        }


        public static P RequiresNoValidation<P>(this P panel) where P : AdfPanel
        {
            panel.LastItem().RequiresValidation = false;

            return panel;
        }

        public static AdfPanel Show<D>(this AdfPanel panel, Expression<Func<D, object>> expression)
        {
            panel.AddPanelItem();

            panel.LastItem().Member = expression.GetMemberInfo();
            panel.LastItem().Optional = !panel.LastItem().Member.IsDefined(typeof(NonEmptyAttribute), false);
            if (panel.AutoGenerateLabels) panel.LastItem().Label = expression.GetMemberInfo().Name;

            return panel;
        }

        public static P WithPrevious<P>(this P panel) where P : AdfPanel
        {
            panel.LastItem().AttachToPrevious = true;

            return panel;
        }

        public static P AssignTo<P>(this P panel, out PanelItem target) where P : AdfPanel
        {
            target = panel.LastItem();

            return panel;
        }
    }
}
