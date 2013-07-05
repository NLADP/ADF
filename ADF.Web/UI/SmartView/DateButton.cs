using System.Globalization;

namespace Adf.Web.UI.SmartView
{
    public class DateButton : TextButton
    {
        public DateButton()
        {
            FieldStyle = "DateTimeColumn";
            HeadStyle = "DateTimeColumnHeader";
        }

        //public override bool Initialize(bool sortingEnabled, System.Web.UI.Control control)
        //{
        //    DataTextFormatString = "{0:" + CultureInfo.CurrentUICulture.DateTimeFormat.ShortDatePattern + "}";

        //    return base.Initialize(sortingEnabled, control);
        //}
    }
}