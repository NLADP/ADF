using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Panels;
using Adf.Web.UI;

namespace Adf.Web.Panels
{
    public class FileUploadRenderer : BaseRenderer, IPanelItemRenderer
    {
        public bool CanRender(PanelItemType type)
        {
            var types = new[] { PanelItemType.FileUpload };

            return types.Contains(type);
        }

        public IEnumerable<object> Render(PanelItem panelItem)
        {
            var upload = new FileUpload { Enabled = true, CssClass = ItemStyle, Visible = panelItem.Visible };

            upload.AttachToolTip(panelItem);

            panelItem.Target = upload;

            return new List<Control> { upload, PanelValidator.Create(panelItem) };
        }
    }
}
