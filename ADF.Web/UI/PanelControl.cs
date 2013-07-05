using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Panels;
using Adf.Core.Rendering;

namespace Adf.Web.UI
{
    public class PanelControl : WebControl, INamingContainer
    {
        public PanelObject Panel = new PanelObject();

        public override ControlCollection Controls
        {
            get
            {
                EnsureChildControls();
                return base.Controls;
            }
        }

        public void Render()
        {
            EnsureChildControls();
        }

        protected override void CreateChildControls()
        {
            var table = RenderManager.Render(RenderType.Panel, Panel) as Table;

            if (table != null) Controls.Add(table);
        }

        protected T GetTarget<T>(PanelItem item)
        {
            Render();

            return (T)item.Target;
        }
    }
}
