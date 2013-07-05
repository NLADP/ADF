using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Adf.Core.Binding;
using Adf.Core.Domain;
using Adf.Core.Validation;
using Adf.Web.UI;
using Calendar = System.Web.UI.WebControls.Calendar;

namespace Adf.Web.Binding
{
    /// <summary>
    /// Represents a persister for a <see cref="System.Web.UI.WebControls.Calendar"/> and 
    /// <see cref="Adf.Web.UI.DateTextBox"/>.
    /// Provides method to persist the date of a <see cref="System.Web.UI.WebControls.Calendar"/> 
    /// or <see cref="Adf.Web.UI.DateTextBox"/>.
    /// </summary>
    public class DateTimePersister : IControlPersister
    {
        /// <summary>
        /// The array of <see cref="System.Web.UI.WebControls.Calendar"/> or 
        /// <see cref="Adf.Web.UI.DateTextBox"/> id prefixes that support persistance.
        /// </summary>
        private readonly string[] types = { "dtb", "cal" };

        /// <summary>
        /// Gets the array of <see cref="System.Web.UI.WebControls.Calendar"/> or 
        /// <see cref="Adf.Web.UI.DateTextBox"/> id prefixes that support persistance.
        /// </summary>
        /// <returns>
        /// The array of <see cref="System.Web.UI.WebControls.Calendar"/> or 
        /// <see cref="Adf.Web.UI.DateTextBox"/> id prefixes.
        /// </returns>
        public IEnumerable<string> Types
        {
            get { return types; }
        }

        /// <summary>
        /// Persists the date of the specified <see cref="System.Web.UI.WebControls.Calendar"/>
        /// or <see cref="Adf.Web.UI.DateTextBox"/> to the specified property of the specified object.
        /// </summary>
        /// <param name="bindableObject">The object where to persist.</param>
        /// <param name="pi">The property of the object where to persist.</param>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.Calendar"/> or
        /// <see cref="Adf.Web.UI.DateTextBox"/>, the date of which is to persist.</param>
        public virtual void Persist(object bindableObject, PropertyInfo pi, object control)
        {
            // Persist Calender fields
            Calendar calendar = control as Calendar;
            if (calendar != null)
            {
                if (calendar.Enabled && calendar.Visible)
                {
                    PropertyHelper.SetValue(bindableObject, pi, calendar.SelectedDate, CultureInfo.CurrentUICulture);
                }
                return;
            }

            // Persist DatTime fields
            DateTextBox datetext = control as DateTextBox;
            if (datetext != null)
            {
                if (datetext.Enabled && datetext.Visible)
                {
                    PropertyHelper.SetValue(bindableObject, pi, datetext.Date, CultureInfo.CurrentUICulture);
                }
                return;
            }

            var smartdatetext = control as SmartDateTextBox;
            if (smartdatetext != null)
            {
                if (smartdatetext.Enabled && smartdatetext.Visible && !smartdatetext.ReadOnly)
                {
                    if (smartdatetext.IsValid)
                    {
                        PropertyHelper.SetValue(bindableObject, pi, smartdatetext.Date, CultureInfo.CurrentUICulture);
                    }
                    else
                    {
                        ValidationManager.AddError(pi, "Adf.Business.NotInstantiable", smartdatetext.Text, pi.Name);
                    }
                }
            }
        }
    }
}