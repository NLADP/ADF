using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Panels;
using Adf.Core.Extensions;
using Adf.Web.Styling;
using Adf.Web.UI.Extensions;

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
                var text = new TextBox { ID = panelItem.GetId(), Wrap = true, ReadOnly = !panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), Visible = panelItem.Visible };

                if (panelItem.MaxLength > 0) text.MaxLength = panelItem.MaxLength;

                text
                    .AddStyle(CssClass.Item)
                    .AttachToolTip(panelItem)
                    .ToggleStyle(panelItem.Editable, CssClass.Editable, CssClass.ReadOnly);

                panelItem.Target = text;

                return new List<Control> { text, PanelValidator.Create(panelItem) };
            }
            if (panelItem.Type == PanelItemType.Password)
            {
                var text = new TextBox { ID = panelItem.GetId(), Wrap = true, TextMode = TextBoxMode.Password, ReadOnly = !panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), Visible = panelItem.Visible };

                if (panelItem.MaxLength > 0) text.MaxLength = panelItem.MaxLength;

                text
                    .AddStyle(CssClass.Item)
                    .AttachToolTip(panelItem)
                    .ToggleStyle(panelItem.Editable, CssClass.Editable, CssClass.ReadOnly);

                panelItem.Target = text;

                return new List<Control> { text, PanelValidator.Create(panelItem) };
            }
            if (panelItem.Type == PanelItemType.MultiLineText)
            {
                var text = new TextBox { ID = panelItem.GetId(), TextMode = TextBoxMode.MultiLine, Wrap = true, ReadOnly = !panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), Height = new Unit((panelItem.Height > 0) ? panelItem.Height : 20, UnitType.Ex), Visible = panelItem.Visible };

                if (panelItem.MaxLength > 0) text.MaxLength = panelItem.MaxLength;

                text
                    .AddStyle(CssClass.Item)
                    .AttachToolTip(panelItem)
                    .ToggleStyle(panelItem.Editable, CssClass.Editable, CssClass.ReadOnly);

                panelItem.Target = text;

                return new List<Control> { text, PanelValidator.Create(panelItem) };
            }
            if (panelItem.Type == PanelItemType.NonEditableText)
            {
                var text = new TextBox { ID = panelItem.GetId(), Wrap = true, ReadOnly = !panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), Visible = panelItem.Visible };

                if (panelItem.MaxLength > 0) text.MaxLength = panelItem.MaxLength;

                text
                    .AddStyle(CssClass.Item)
                    .AttachToolTip(panelItem)
                    .ToggleStyle(panelItem.Editable, CssClass.Editable, CssClass.ReadOnly);

                panelItem.Target = text;

                return new List<Control> { text, PanelValidator.Create(panelItem) };
            }
            if (panelItem.Type == PanelItemType.Label)
            {
                var text = new Label { ID = panelItem.GetId(), Enabled = panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), Text = (!panelItem.Text.IsNullOrEmpty() ? panelItem.Text : string.Empty), Visible = panelItem.Visible };

                text
                    .AddStyle(CssClass.Item)
                    .AttachToolTip(panelItem)
                    .ToggleStyle(panelItem.Editable, CssClass.Editable, CssClass.ReadOnly);

                panelItem.Target = text;

                return new List<Control> { text, PanelValidator.Create(panelItem) };
            }

            return null;
        }
    }
}
