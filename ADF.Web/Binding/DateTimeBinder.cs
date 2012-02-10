using System;
using System.Collections;
using System.Reflection;
using System.Web.UI.WebControls;
using Adf.Core.Binding;
using Adf.Web.UI;

namespace Adf.Web.Binding
{
    /// <summary>
    /// Represents a binder for a <see cref="System.Web.UI.WebControls.Calendar"/> and 
    /// <see cref="Adf.Web.UI.DateTextBox"/>.
    /// Provides methods to bind the 'SelectedDate' or 'Date' property of a 
    /// <see cref="System.Web.UI.WebControls.Calendar"/> or a <see cref="Adf.Web.UI.DateTextBox"/> control
    /// respectively with a date.
    /// </summary>
    public class DateTimeBinder : IControlBinder
    {
        /// <summary>
        /// The array of <see cref="System.Web.UI.WebControls.Calendar"/> or 
        /// <see cref="Adf.Web.UI.DateTextBox"/> id prefixes that support binding.
        /// </summary>
        private readonly string[] types = { "dtb", "cal" };

        /// <summary>
        /// Gets the array of <see cref="System.Web.UI.WebControls.Calendar"/> or 
        /// <see cref="Adf.Web.UI.DateTextBox"/> id prefixes that support binding.
        /// </summary>
        /// <returns>
        /// The array of <see cref="System.Web.UI.WebControls.Calendar"/> or 
        /// <see cref="Adf.Web.UI.DateTextBox"/> id prefixes.
        /// </returns>
        public IEnumerable Types
        {
            get { return types; }
        }

        /// <summary>
        /// Binds the 'SelectedDate' or 'Date' property of the specified
        /// <see cref="System.Web.UI.WebControls.Calendar"/> or <see cref="Adf.Web.UI.DateTextBox"/> 
        /// with the specified value.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.Calendar"/> or 
        /// <see cref="Adf.Web.UI.DateTextBox"/> to bind to.</param>
        /// <param name="value">The value to bind.</param>
        /// <param name="pi">Currently not being used.</param>
        /// <param name="p">The parameters used for binding. Currently not being used.</param>
        public virtual void Bind(object control, object value, PropertyInfo pi, params object[] p)
        {
            //if (value == null)
              //  return;

            var calendar = control as Calendar;
            if (calendar != null)
            {
                if (value == null) return;

                if (string.IsNullOrEmpty(value.ToString())) calendar.SelectedDate = GetDate(value).Value;
                return;
            }

            var box = control as DateTextBox;
            if (box != null)
            {
                box.Date = (value != null) ? GetDate(value) : new DateTime?();
            }

            var smartdatetextbox = control as SmartDateTextBox;
            if (smartdatetextbox != null)
            {
                smartdatetextbox.Date = (value != null) ? GetDate(value) : new DateTime?();
            }
        }

        /// <summary>
        /// Converts the specified value to a Nullable&lt;DateTime&gt; value and returns the output.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>
        /// The Nullable&lt;DateTime&gt; value if the conversion is successful; otherwise, null.
        /// </returns>
        private static DateTime? GetDate(object value)
        {
            if (value is DateTime) return (DateTime?)value;
            if (value is DateTime?) return (DateTime?)value;

            return null;
        }

        /// <summary>
        /// Binds the specified <see cref="System.Web.UI.WebControls.Calendar"/> or 
        /// <see cref="Adf.Web.UI.DateTextBox"/> with the specified array of objects.
        /// Currently not being supported.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.Calendar"/> or 
        /// <see cref="Adf.Web.UI.DateTextBox"/> to bind to.</param>
        /// <param name="values">The array of objects to bind.</param>
        /// <param name="p">The parameters used for binding.</param>
        public void Bind(object control, object[] values, params object[] p)
        {
            throw new NotSupportedException();
        }

        /// <summary>
        /// Binds the specified <see cref="System.Web.UI.WebControls.Calendar"/> or 
        /// <see cref="Adf.Web.UI.DateTextBox"/> with the specified list.
        /// Currently not being supported.
        /// </summary>
        /// <param name="control">The <see cref="System.Web.UI.WebControls.Calendar"/> or 
        ///   <see cref="Adf.Web.UI.DateTextBox"/> to bind to.</param>
        /// <param name="values">The list to bind.</param>
        /// <param name="p">The parameters used for binding.</param>
        public void Bind(object control, IEnumerable values, params object[] p)
        {
            throw new NotSupportedException();
        }
    }
}