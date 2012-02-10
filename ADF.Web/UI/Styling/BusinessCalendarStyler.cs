using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adf.Web.UI.Styling
{
	/// <summary>
    /// Represents a styler for a <see cref="Adf.Web.UI.BusinessCalendar"/>.
    /// Provides method to set the styles defined in the cascading style sheets to a 
    /// <see cref="Adf.Web.UI.BusinessCalendar"/>.
	/// </summary>
	public class BusinessCalendarStyler : IStyler
	{
		/// <summary>
        /// Sets the CSS styles to the specified <see cref="Adf.Web.UI.BusinessCalendar"/>.
		/// </summary>
        /// <param name="c">The <see cref="Adf.Web.UI.BusinessCalendar"/> to give CSS styles to.</param>
		public void SetStyles(Control c)
		{
			Calendar calendar = c as Calendar;
			if (calendar == null) return;

			calendar.SelectedDayStyle.CssClass = "BusinessCalendarSelectedDay";
			calendar.NextPrevStyle.CssClass = "BusinessCalendarNextPrev";
			calendar.OtherMonthDayStyle.CssClass = "BusinessCalendarOtherMonth";
			calendar.SelectorStyle.CssClass  = "BusinessCalendarSelector";
			calendar.TitleStyle.CssClass = "BusinessCalendarTitle";
			calendar.TodayDayStyle.CssClass = "BusinessCalendarTodayDay";
			calendar.WeekendDayStyle.CssClass = "BusinessCalendarWeekendDay";
			calendar.ControlStyle.CssClass = "BusinessCalendarControl";
			calendar.DayHeaderStyle.CssClass = "BusinessCalendarDayHeader";
			calendar.DayStyle.CssClass = "BusinessCalendarDay";
		}
	}
}
