using System.Globalization;
using Adf.Core.Styling;

namespace Adf.Web.Panels
{
    public abstract class BaseRenderer
    {
        public string PanelStyle = string.Empty;
        public string RowStyle = string.Empty;

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
