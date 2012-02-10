using System.Globalization;

namespace Adf.Web.Panels
{
    public abstract class BaseRenderer
    {
        protected readonly string PanelStyle = "AdfPanelPanel";
        protected readonly string RowStyle = "AdfPanelRow";
        protected readonly string LabelCellStyle = "AdfPanelLabelCell";
        protected readonly string LabelStyle = "AdfPanelLabel";
        protected readonly string ItemCellStyle = "AdfPanelItemCell";
        protected readonly string ItemStyle = "AdfPanelItem";

        protected string DateFormat = string.Format("{0}", CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern);
    }
}
