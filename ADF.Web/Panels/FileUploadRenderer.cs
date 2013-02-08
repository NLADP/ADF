using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Panels;
using Adf.Core.Rendering;
using Adf.Web.Styling;
using Adf.Web.UI.Extensions;

namespace Adf.Web.Panels
{
    public class FileUploadRenderer : BaseRenderer, IItemRenderer
    {
        public bool CanRender(RenderItemType type)
        {
            var types = new[] { RenderItemType.FileUpload };

            return types.Contains(type);
        }

        public IEnumerable<object> Render(PanelItem panelItem)
        {
            var upload = new FileUpload { Enabled = true, Visible = panelItem.Visible };

            upload
                .AddStyle(CssClass.Item)
                .AttachToolTip(panelItem)
                .ToggleStyle(panelItem.Editable, CssClass.Editable, CssClass.ReadOnly);
            
            panelItem.Target = upload;

            return new List<Control> { upload, PanelValidator.Create(panelItem) };
        }
    }
}
