using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Panels;
using Adf.Core.Rendering;
using Adf.Web.Styling;
using Adf.Web.UI;
using Adf.Web.UI.Extensions;
using AjaxControlToolkit;

namespace Adf.Web.Panels
{
    public class CalendarRenderer : BaseRenderer, IItemRenderer
    {
        public bool CanRender(RenderItemType type)
        {
            return type.IsIn(RenderItemType.Calendar);
        }

        public IEnumerable<object> Render(PanelItem panelItem)
        {
            var formatDisplay = DateFormat;

            var box = new SmartDateTextBox { ID = panelItem.GetId(), TextMode = TextBoxMode.SingleLine, Wrap = true, Enabled = panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), FormatDisplay = formatDisplay, Visible = panelItem.Visible };
            var calendar = new CalendarExtender { TargetControlID = box.UniqueID, Format = formatDisplay, EnabledOnClient = panelItem.Editable };
            var description = new Label { Width = new Unit(panelItem.Width, UnitType.Ex), Text = string.Format("({0})", formatDisplay), Visible = panelItem.Visible };

            if (panelItem.MaxLength > 0) box.MaxLength = panelItem.MaxLength;

            box
            .AddStyle(CssClass.Item)
            .AttachToolTip(panelItem)
            .ToggleStyle(panelItem.Editable, CssClass.Editable, CssClass.ReadOnly);

            description
            .AddStyle(CssClass.Item);


            panelItem.Target = box;

            return new List<Control> { box, PanelValidator.Create(panelItem), calendar, description };
        }
    }
}
