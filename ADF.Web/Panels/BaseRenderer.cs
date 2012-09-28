using System;
using System.Globalization;
using System.Web.UI.WebControls;
using Adf.Core.Panels;
using Adf.Core.Extensions;
using Adf.Core.Resources;
using Adf.Core.Styling;

namespace Adf.Web.Panels
{
    public static class RendererExtensions
    {

        public static void AttachToolTip(this WebControl control, PanelItem item)
        {
            if (control == null) throw new ArgumentNullException("control");

            if (item.ToolTip.IsNullOrEmpty()) return;

            control.ToolTip = ResourceManager.GetString(item.ToolTip);
        }        
    }

    public abstract class BaseRenderer
    {
        public string PanelStyle = string.Empty;
        public string RowStyle = string.Empty;
        public string LabelCellStyle = string.Empty;
        public string LabelStyle = string.Empty;
        public string ItemCellStyle = string.Empty;
        public string ItemStyle = string.Empty;

        protected string DateFormat
        {
            get { return string.Format("{0}", CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern); }
        }

        protected BaseRenderer()
        {
            StyleManager.Style(StyleType.Panel, this);
        }
    }
}
