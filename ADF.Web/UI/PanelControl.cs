using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Core.Panels;

namespace Adf.Web.UI
{
    public class PanelControl : WebControl, INamingContainer
    {
        public AdfPanel Panel = new AdfPanel();

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
            var table = PanelManager.Render(Panel) as Table;

            if (table != null) Controls.Add(table);
        }
    }
}
