using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Panels;
using Adf.Core.Extensions;

namespace Adf.Web.Panels
{
    public class TextRenderer : BaseRenderer, IPanelItemRenderer
    {
        public bool CanRender(PanelItemType type)
        {
            return type.IsIn(PanelItemType.MultiLineText, PanelItemType.NonEditableText, PanelItemType.EditableText, PanelItemType.Password, PanelItemType.Label);
        }

        public IEnumerable<object> Render(PanelItem panelItem)
        {
            if (panelItem.Type == PanelItemType.EditableText)
            {
                var css = ItemStyle + ((!panelItem.Editable) ? " ReadOnly" : "");
                var text = new TextBox { ID = panelItem.GetId(), Wrap = true, ReadOnly = !panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), CssClass = css, Visible = panelItem.Visible };

                text.AttachToolTip(panelItem);

                panelItem.Target = text;

                return new List<Control> { text, PanelValidator.Create(panelItem) };
            }
            if (panelItem.Type == PanelItemType.Password)
            {
                var css = ItemStyle + ((!panelItem.Editable) ? " ReadOnly" : "");
                var text = new TextBox { ID = panelItem.GetId(), Wrap = true, TextMode = TextBoxMode.Password, ReadOnly = !panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), CssClass = css, Visible = panelItem.Visible };

                text.AttachToolTip(panelItem);

                panelItem.Target = text;

                return new List<Control> { text, PanelValidator.Create(panelItem) };
            }
            if (panelItem.Type == PanelItemType.MultiLineText)
            {
                var css = ItemStyle + ((!panelItem.Editable) ? " ReadOnly" : "");
                var text = new TextBox { ID = panelItem.GetId(), TextMode = TextBoxMode.MultiLine, Wrap = true, ReadOnly = !panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), Height = new Unit((panelItem.Height > 0) ? panelItem.Height : 20, UnitType.Ex), CssClass = css, Visible = panelItem.Visible };

                text.AttachToolTip(panelItem);

                panelItem.Target = text;

                return new List<Control> { text, PanelValidator.Create(panelItem) };
            }
            if (panelItem.Type == PanelItemType.NonEditableText)
            {
                var css = ItemStyle + ((!panelItem.Editable) ? " ReadOnly" : "");
                var text = new TextBox { ID = panelItem.GetId(), Wrap = true, ReadOnly = !panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), CssClass = css, Visible = panelItem.Visible };

                text.AttachToolTip(panelItem);

                panelItem.Target = text;

                return new List<Control> { text, PanelValidator.Create(panelItem) };
            }
            if (panelItem.Type == PanelItemType.Label)
            {
                var text = new Label { ID = panelItem.GetId(), Enabled = panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), CssClass = ItemStyle, Text = (!panelItem.Text.IsNullOrEmpty() ? panelItem.Text : string.Empty), Visible = panelItem.Visible };

                text.AttachToolTip(panelItem);

                panelItem.Target = text;

                return new List<Control> { text, PanelValidator.Create(panelItem) };
            }

            return null;
        }
    }
}
