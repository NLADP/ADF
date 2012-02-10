using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Base.Formatting;

namespace Adf.Web.UI
{
    /// <summary>
    /// Represent an user defined smart text box control for formated user input of date and time.
    /// </summary>
    [DefaultProperty("FormatDisplay"), ToolboxData("<{0}:DateTextBox runat=server></{0}:DateTextBox>")]
    public class SmartDateTextBox : TextBox
    {
        /// <summary>
        /// Get or sets the format to input data in a smart text box.
        /// </summary>
        /// <returns>The format to input data in a smart text box.</returns>
        [Bindable(true), Category("Default"), DefaultValue(true)]
        public string FormatDisplay
        {
            get { return (string)ViewState["FormatDisplay"]; }
            set { ViewState["FormatDisplay"] = value; }
        }

        /// <summary>
        /// Gets or sets the formated date into smart text box.
        /// Gets date in datetime format or sets datetime in string format.
        /// </summary>
        /// <returns>The formated date into smart text box.</returns>
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
