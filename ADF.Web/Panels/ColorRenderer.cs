using System.Collections.Generic;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Panels;
using Adf.Core.Rendering;
using Adf.Web.Styling;
using Adf.Web.UI;
using Adf.Web.UI.Extensions;
using AjaxControlToolkit;
using Image = System.Web.UI.WebControls.Image;

namespace Adf.Web.Panels
{
    public class ColorRenderer : BaseRenderer, IItemRenderer
    {
        public bool CanRender(RenderItemType type)
        {
            return type.IsIn(RenderItemType.ColorPicker);
        }

        public IEnumerable<object> Render(PanelItem panelItem)
        {
            var text = new TextBox { ID = panelItem.GetId(), Wrap = true, ReadOnly = !panelItem.Editable, Width = new Unit(panelItem.Width, UnitType.Ex), Visible = panelItem.Visible };

            if (panelItem.MaxLength > 0) text.MaxLength = panelItem.MaxLength;

            var image = new Image {Width = 18, Height = 18};
            image.Style.Add("vertical-align", "middle");
            image.Style.Add("border", "1px solid darkgrey");
            image.ID = panelItem.GetId() + "colorPickerImage";
            image.ImageUrl = "~/images/transparent.gif";

            image
            .AddStyle(CssClass.Item)
            .AttachToolTip(panelItem)
            .ToggleStyle(panelItem.Editable, CssClass.Editable, CssClass.ReadOnly);

            text
            .AddStyle(CssClass.Item);

            var colorpicker = new ColorPickerExtender { TargetControlID = text.UniqueID, EnabledOnClient = true, PopupButtonID = image.UniqueID, SampleControlID = image.UniqueID };

            panelItem.Target = text;

            return new List<Control> { text, PanelValidator.Create(panelItem), image, colorpicker };
        }
    }
}
