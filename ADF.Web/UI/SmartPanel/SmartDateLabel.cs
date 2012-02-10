using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Base.Formatting;

namespace Adf.Web.UI
{
    /// <summary>
    /// Represent an user defined smart label control to display formated data of date and time.
    /// </summary>
    [DefaultProperty("FormatDisplay"), ToolboxData("<{0}:DateTextBox runat=server></{0}:DateTextBox>")]
    public class SmartDateLabel : Label
    {
        /// <summary>
        /// Get or sets the format to display data in a smart label. 
        /// </summary>
        /// <returns>The format to display data in a label.</returns>
        [Bindable(true), Category("Default"), DefaultValue(true)]
        public string FormatDisplay
        {
            get { return (string)ViewState["FormatDisplay"]; }
            set { ViewState["FormatDisplay"] = value; }
        }

        /// <summary>
        /// Gets or sets the formated date into smart label.
        /// Gets date in datetime format or sets datetime in string format.
        /// </summary>
        /// <returns>The formated date into smart label.</returns>
        public DateTime? Date
        {
            get
            {
                return FormatHelper.ParseToDateTime(Text);
            }
            set
            {
                Text = FormatHelper.Format(value, FormatDisplay);
            }
        }

        /// <summary>
        /// Gets the valid datetime.
        /// </summary>
        /// <returns>True if specified datetime value is correct; otherwise, false.</returns>
        public bool IsValid
        {
            get
            {
                return FormatHelper.IsValidDateTime(Text);
            }
        }
    }
}
