using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Panels;
using Adf.Core.Resources;

namespace Adf.Web.Panels
{
    public class SelectButtonRenderer : BaseRenderer, IPanelItemRenderer
    {
        public bool CanRender(PanelItemType type)
        {
            return type.IsIn(PanelItemType.SelectButton);
        }

        public IEnumerable<object> Render(PanelItem panelItem)
        {
            var textControl = new LinkButton { ID = panelItem.GetId(), Width = new Unit(panelItem.Width, UnitType.Ex), CssClass = "SmartPanelSelectItem" };

            var textDisabledControl = new Label { ID = panelItem.GetId("lbl"), Width = new Unit(panelItem.Width, UnitType.Ex), CssClass = "SmartPanelSelectItemDisabled" };

            var selectButton = new LinkButton { Text = ResourceManager.GetString("Select"), CssClass = "tinybutton", Visible = panelItem.Editable };
            var clearButton = new LinkButton { Text = ResourceManager.GetString("Clear"), CssClass = "tinybutton" };

            if (!panelItem.Optional || !panelItem.Editable)
            {
                clearButton.Visible = false;
            }

            textControl.AttachToolTip(panelItem);
            textDisabledControl.AttachToolTip(panelItem);

            var control = new SelectButtonControl(textControl, selectButton, clearButton, textDisabledControl);
            panelItem.Target = control;

            return new Control[] { textControl, textDisabledControl, selectButton, clearButton, PanelValidator.Create(panelItem) };
        }
    }

    public class SelectButtonControl
    {
        public LinkButton TextButton { get; private set; }

        public LinkButton SelectButton { get; private set; }

        public LinkButton ClearButton { get; private set; }

        private Label TextDisabledButton { get; set; }

        public event EventHandler TextClicked;

        public event EventHandler SelectClicked;

        public event EventHandler ClearClicked;

        public SelectButtonControl(LinkButton textButton, LinkButton selectButton, LinkButton clearButton, Label textDisabledButton)
        {
            TextButton = textButton;
            SelectButton = selectButton;
            ClearButton = clearButton;
            TextDisabledButton = textDisabledButton;

            textButton.Click += (sender, args) => { if (TextClicked != null) TextClicked.Invoke(sender, args); };
            selectButton.Click += (sender, args) => { if (SelectClicked != null) SelectClicked.Invoke(sender, args); };
            clearButton.Click += (sender, args) => { if (ClearClicked != null) ClearClicked.Invoke(sender, args); };

            textButton.PreRender += (sender, args) =>
            {
                TextButton.Visible = TextButton.Visible && TextClicked != null && !TextButton.Text.IsNullOrEmpty();
                TextDisabledButton.Visible = !TextButton.Visible;
            };
            selectButton.PreRender += (sender, args) => { SelectButton.Visible = SelectButton.Visible && SelectClicked != null; };
            clearButton.PreRender += (sender, args) => { ClearButton.Visible = ClearButton.Visible && !string.IsNullOrEmpty(TextButton.Text); };
        }
    }
}
