using System;
using System.Linq.Expressions;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Resources;

namespace Adf.Web.UI
{
    /// <summary>
    /// Validate a text box control for user input.
    /// </summary>
    public class SelectItem : BasePanelItem
    {
        private const string Prefix = "lb";

        public LinkButton TextButton { get; protected set; }

        public LinkButton SelectButton { get; protected set; }

        public LinkButton ClearButton { get; protected set; }

        private Label TextDisabledButton { get; set; }

        public string Text
        {
            get { return TextButton.Text; }
            set
            {
                TextButton.Text = value;
                TextDisabledButton.Text = value;
            }
        }

        public event EventHandler TextClicked;

        public event EventHandler SelectClicked;

        public event EventHandler ClearClicked;

        public SelectItem(WebControl label, LinkButton textControl, Label textDisabledControl, LinkButton selectControl, LinkButton clearControl, bool mandatory)
        {
            AddLabelControl(label);
            if (mandatory) AddLabelControl(new Label { Text = "*" });

            textControl.Click += (sender, args) => { if (TextClicked != null) TextClicked.Invoke(sender, args); };
            selectControl.Click += (sender, args) => { if (SelectClicked != null) SelectClicked.Invoke(sender, args); };
            clearControl.Click += (sender, args) => { if (ClearClicked != null) ClearClicked.Invoke(sender, args); };

            textControl.PreRender += (sender, args) =>
            {
                TextButton.Visible = TextButton.Visible && TextClicked != null && !TextButton.Text.IsNullOrEmpty();
                TextDisabledButton.Visible = !TextButton.Visible;
            };
            selectControl.PreRender += (sender, args) => { SelectButton.Visible = SelectButton.Visible && SelectClicked != null; };
            clearControl.PreRender += (sender, args) => { ClearButton.Visible = ClearButton.Visible && !string.IsNullOrEmpty(TextButton.Text); };

            AddControl(TextButton = textControl);
            AddControl(TextDisabledButton = textDisabledControl);
            AddControl(SelectButton = selectControl);
            AddControl(ClearButton = clearControl);
        }

        public static SelectItem Create(string label, string name, int width, bool enabled, bool mandatory)
        {
            var l = new Label {Text = ResourceManager.GetString(label)};

            var textControl = new LinkButton { ID = Prefix + name, Width = new Unit(width, UnitType.Ex), CssClass = "SmartPanelSelectItem" };

            var textDisabledControl = new Label { ID = "lbl" + name, Width = new Unit(width, UnitType.Ex), CssClass = "SmartPanelSelectItemDisabled" };

            var selectButton = new LinkButton { Text = ResourceManager.GetString("Select"), CssClass = "tinybutton", Visible = enabled };
            var clearButton = new LinkButton { Text = ResourceManager.GetString("Clear"), CssClass = "tinybutton" };

            if (mandatory || !enabled)
            {
                clearButton.Visible = false;
            }

            return new SelectItem(l, textControl, textDisabledControl, selectButton, clearButton, mandatory);
        }

        public static SelectItem Create<T>(Expression<Func<T, object>> property, int width, bool enabled = true, bool mandatory = true)
        {
            return Create(property.GetMemberInfo().Name, property, width, enabled, mandatory);
        }

        public static SelectItem Create<T>(string label, Expression<Func<T, object>> property, int width, bool enabled = true, bool mandatory = true)
        {
            return Create(label, property.GetControlName(), width, enabled, mandatory);
        }

        public override bool Enabled
        {
            get { return TextButton.Visible; }
            set
            {
                TextButton.Visible = value;
                SelectButton.Enabled = value;
                ClearButton.Enabled = value;
            }
        }
    }
}
