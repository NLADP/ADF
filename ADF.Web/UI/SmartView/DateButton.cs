using System.Globalization;

namespace Adf.Web.UI.SmartView
{
    public class DateButton : BaseButton
    {
        public DateButton()
        {
            ColumnStyle = "DateTimeColumn";
            HeaderStyle.CssClass = "DateTimeColumnHeader";
        }

        public override bool Initialize(bool sortingEnabled, System.Web.UI.Control control)
        {
            DataTextFormatString = "{0:" + CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern + "}";

            return base.Initialize(sortingEnabled, control);
        }
    }
}