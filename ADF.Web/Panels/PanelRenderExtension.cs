using System;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Panels;
using Adf.Core.Resources;
using Adf.Web.UI;

namespace Adf.Web.Panels
{
    public static class PanelRenderExtension
    {

        public static WebControl AttachToolTip(this WebControl control, PanelItem item)
        {
            if (control == null) throw new ArgumentNullException("control");

            if (item.ToolTip.IsNotEmpty()) control.ToolTip = ResourceManager.GetString(item.ToolTip);

            return control;
        }

        public static void SetTabIndex(this PanelItem item, short index)
        {
            if (item == null) throw new ArgumentNullException("item");

            var target = item.Target as WebControl;
            if (target != null) target.TabIndex = index;
        }
    }
}
