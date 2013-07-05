using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Base.Panels;
using Adf.Core.Extensions;
using Adf.Core.Panels;
using Adf.Core.Rendering;
using Adf.Core.Resources;
using Adf.Web.Styling;
using Adf.Web.UI;
using Adf.Web.UI.Extensions;

namespace Adf.Web.Panels
{
    public class ImageRenderer : BaseRenderer, IItemRenderer
    {
        public bool CanRender(RenderItemType type)
        {
            var types = new[] { RenderItemType.Image, RenderItemType.InfoIcon };

            return types.Contains(type);
        }

        public IEnumerable<object> Render(PanelItem panelItem)
        {
            if (panelItem.Type == RenderItemType.Image)
            {
                var image = new Image { ID = panelItem.GetId(), Enabled = false, Width = new Unit(panelItem.Width, UnitType.Pixel), Visible = panelItem.Visible };

                image
                    .AddStyle(CssClass.Item)
                    .AttachToolTip(panelItem);

                panelItem.Target = image;

                return new List<Control> { image, PanelValidator.Create(panelItem) };
            }
            if (panelItem.Type == RenderItemType.InfoIcon)
            {
                var image = new Image
                {
                    ID = panelItem.GetId(),
                    ImageUrl = @"../images/help.png",
                    ToolTip = ResourceManager.GetString(panelItem.Label.IsNullOrEmpty() ? panelItem.GetPropertyName() + "Info" : panelItem.Label),
                    Enabled = false,
                    Width = new Unit(panelItem.Width, UnitType.Pixel),
                    Visible = panelItem.Visible
                };

                image
                    .AddStyle(CssClass.Item)
                    .AddStyle(CssClass.HelpIcon)
                    .AttachToolTip(panelItem);
                
                panelItem.Target = image;

                return new List<Control> { image };
            }

            return null;
        }
    }
}
