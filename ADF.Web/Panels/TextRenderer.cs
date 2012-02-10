using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Base.Panels;
using Adf.Core.Panels;
using Adf.Core.Extensions;
using Adf.Web.UI;

namespace Adf.Web.Panels
{
    public class TextRenderer : BaseRenderer, IPanelItemRenderer
    {
        public bool CanRender(PanelItemType type)
        {
            var types = new[]{ PanelItemType.MultiLineText, PanelItemType.NonEditableText, PanelItemType.EditableText, PanelItemType.Password, PanelItemType.Label };

            return types.Contains(type);
        }

        public IEnumerable<object> Render(PanelItem panelItem)
        {
            var validator = SmartValidator.Create(panelItem.GetId());

            if (panelItem.Type == PanelItemType.EditableText)
            {
                string css = ItemStyle + ((!panelItem.Editable) ? " ReadOnly" : "");
                var text = new TextBox { ID = panelItem.GetId(), Wrap = true, ReadOnly = !panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), CssClass = css };

                panelItem.Target = text;

                return new List<Control> { text, validator };
            }
            if (panelItem.Type == PanelItemType.Password)
            {
                string css = ItemStyle + ((!panelItem.Editable) ? " ReadOnly" : "");
                var text = new TextBox { ID = panelItem.GetId(), Wrap = true, TextMode =TextBoxMode.Password, ReadOnly = !panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), CssClass = css };

                panelItem.Target = text;
                
                return new List<Control> { text, validator };
            }
            if (panelItem.Type == PanelItemType.MultiLineText)
            {
                string css = ItemStyle + ((!panelItem.Editable) ? " ReadOnly" : "");
                var text = new TextBox { ID = panelItem.GetId(), TextMode = TextBoxMode.MultiLine, Wrap = true, ReadOnly = !panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), Height = new Unit((panelItem.Height > 0) ? panelItem.Height : 20, UnitType.Ex), CssClass = css };

                panelItem.Target = text;

                return new List<Control> { text, validator };
            }
            if (panelItem.Type == PanelItemType.NonEditableText)
            {
                string css = ItemStyle + ((!panelItem.Editable) ? " ReadOnly" : "");
                var text = new TextBox { ID = panelItem.GetId(), Wrap = true, ReadOnly = !panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), CssClass = css };

                panelItem.Target = text;

                return new List<Control> { text };
            }
            if (panelItem.Type == PanelItemType.Label)
            {
                var text = new Label { ID = panelItem.GetId(), Enabled = panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), CssClass = ItemStyle, Text = (!panelItem.Text.IsNullOrEmpty() ? panelItem.Text : string.Empty)};

                panelItem.Target = text;

                return new List<Control> { text };
            }

            return null;
        }
    }
}
