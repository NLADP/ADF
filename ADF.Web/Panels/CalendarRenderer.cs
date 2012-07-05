﻿using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Base.Panels;
using Adf.Core.Extensions;
using Adf.Core.Panels;
using Adf.Web.UI;
using AjaxControlToolkit;

namespace Adf.Web.Panels
{
    public class CalendarRenderer : BaseRenderer, IPanelItemRenderer
    {
        public bool CanRender(PanelItemType type)
        {
            return type.IsIn(PanelItemType.Calendar);
        }

        public IEnumerable<object> Render(PanelItem panelItem)
        {
            var box = new SmartDateTextBox { ID = panelItem.GetId(), TextMode = TextBoxMode.SingleLine, Wrap = true, Enabled = panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), FormatDisplay = DateFormat, CssClass = ItemStyle, Visible = panelItem.Visible };
            var calendar = new CalendarExtender { TargetControlID = box.UniqueID, Format = DateFormat, EnabledOnClient = panelItem.Editable };
            var description = new Label { Width = new Unit(panelItem.Width, UnitType.Ex), Text = DateFormat, CssClass = ItemStyle, Visible = panelItem.Visible };

            box.AttachToolTip(panelItem);

            panelItem.Target = calendar;

            return new List<Control> { box, PanelValidator.Create(panelItem), calendar, description };
        }
    }
}
