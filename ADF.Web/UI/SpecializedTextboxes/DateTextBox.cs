using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;
using Adf.Web.Properties;

namespace Adf.Web.UI
{
    /// <summary>
    /// Represents a Custom Control Textbox that allows only date values in it.
    /// Provides methods to convert a string value to an equivalent <see cref="System.DateTime"/>
    /// using the culture 'nl-NL', process post back data, etc.
    /// </summary>
    [DefaultProperty("FormatDisplay"), ToolboxData("<{0}:DateTextBox runat=server></{0}:DateTextBox>")]
    public class DateTextBox : WebControl, IPostBackDataHandler
    {
        private const string scriptLocation = "../script/date.js";
        private const string dateTimeFormat = "dd-MM-yyyy HH:mm";
        private const string dateFormat = "dd-MM-yyyy";

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.DateTextBox"/> class.
        /// </summary>
        public DateTextBox() : this(dateFormat)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Adf.Web.UI.DateTextBox"/> class with 
        /// the specified display format.
        /// </summary>
        /// <param name="displayformat">The display format of the 
        /// <see cref="Adf.Web.UI.DateTextBox"/>.</param>
        public DateTextBox(string displayformat)
        {
            FormatDisplay = displayformat;
            Date = null;
        }

        /// <summary>
        /// Gets or sets the display format of the <see cref="Adf.Web.UI.DateTextBox"/>.
        /// </summary>
        /// <returns>
        /// The display format of the <see cref="Adf.Web.UI.DateTextBox"/>.
        /// </returns>
        [Bindable(true), Category("Default"), DefaultValue(true)]
        public string FormatDisplay
        {
            get { return (string) ViewState["FormatDisplay"]; }
            set { ViewState["FormatDisplay"] = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="System.DateTime"/> of the <see cref="Adf.Web.UI.DateTextBox"/>.
        /// </summary>
        /// <returns>
        /// The <see cref="System.DateTime"/> of the <see cref="Adf.Web.UI.DateTextBox"/>.
        /// </returns>
        public DateTime? Date
        {
            get { return (DateTime?) ViewState["Date"]; }
            set { ViewState["Date"] = value; }
        }

        /// <summary>
        /// Converts the specified string representation of a date and time to its 
        /// <see cref="System.DateTime"/> equivalent using the culture 'nl-NL'.
        /// </summary>
        /// <param name="value">The string representation of a date and time.</param>
        /// <returns>
        /// A <see cref="System.DateTime"/> equivalent of the specified string.
        /// If the specified value is null or of wrong format then null is returned.
        /// </returns>
        private static DateTime? ParseToDateTime(string value)
        {
            try
            {
                return DateTime.Parse(value, new CultureInfo("nl-NL"));
            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (FormatException)
            {
                return null;
            }
        }

        /// <summary>
        /// Processes post back data for the <see cref="Adf.Web.UI.DateTextBox"/>.
        /// Sets the date and time of the <see cref="Adf.Web.UI.DateTextBox"/> with the 
        /// date and time in the specified collection of post back data corresponding to the 
        /// specified key.
        /// </summary>
        /// <param name="postDataKey">The key, corresponding to which the date and time value is required.</param>
        /// <param name="postCollection">The <see cref="System.Collections.Specialized.NameValueCollection"/>, 
        /// collection of post back data.</param>
        /// <returns>
        /// Always returns false.
        /// </returns>
        public bool LoadPostData(string postDataKey, NameValueCollection postCollection)
        {
            Date = ParseToDateTime(postCollection[postDataKey].ToString());

            return false;
        }

        /// <summary>
        /// When implemented by a class, the instance of the class signals the ASP.NET application 
        /// that the state of the Control has changed.
        /// </summary>
        public void RaisePostDataChangedEvent()
        {
        }

        /// <summary>
        /// The 'OnPreRender' event handler.
        /// </summary>
        /// <param name="e">The <see cref="System.EventArgs"/> containing event data.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", MessageId = "System.Exception.#ctor(System.String)"), SuppressMessage("Microsoft.Usage", "CA2201:DoNotRaiseReservedExceptionTypes")]
        protected override void OnPreRender(EventArgs e)
        {
            if (Page.ClientScript.IsClientScriptBlockRegistered(GetType(), "date")) return;
            Page.ClientScript.RegisterClientScriptBlock(GetType(), "date", Resources.ResourceManager.GetString("date"));
        }

        /// <summary>
        /// The 'Render' event handler.
        /// </summary>
        /// <param name="writer">The <see langword="System.Web.UI.HtmlTextWriter"/> object.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", MessageId = "System.DateTime.ToString(System.String)")]
        protected override void Render(HtmlTextWriter writer)
        {
            writer.WriteFullBeginTag("span");
            writer.WriteBeginTag("input");
            writer.WriteAttribute("type", "text");
            writer.WriteAttribute("name", UniqueID);
            if (ID != null)
                writer.WriteAttribute("id", ClientID);

            writer.WriteAttribute("size", "10");

            if (FormatDisplay == dateTimeFormat)
            {
                writer.WriteAttribute("onblur", "javascript:check_field_datetime()");
                writer.WriteAttribute("onkeypress", "javascript:datetime_keys();");
                writer.WriteAttribute("maxlength", "16");
            }
            else
            {
                writer.WriteAttribute("onblur", "javascript:check_field_date()");
                writer.WriteAttribute("onkeypress", "javascript:date_keys();");
                writer.WriteAttribute("maxlength", "10");
            }

            if (Date.HasValue)
                writer.WriteAttribute("value", Date.Value.ToString(FormatDisplay));
            if (!Enabled)
                writer.WriteAttribute("disabled", "disabled");
            if (!String.IsNullOrEmpty(CssClass))
                writer.WriteAttribute("class", CssClass);
            writer.Write(HtmlTextWriter.TagRightChar);
            writer.WriteBeginTag("span");
            if (!String.IsNullOrEmpty(CssClass))
                writer.WriteAttribute("class", CssClass);
            writer.Write(HtmlTextWriter.TagRightChar);
            writer.WriteEndTag("span");
            writer.WriteEndTag("span");
        }
    }
}