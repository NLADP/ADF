using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Adf.Web.UI
{
	/// <summary>
	/// Represents control that shows a link with a javascript confirmation box.
	/// </summary>
	public class MessageImageButton: ImageButton
	{
		/// <summary>
		/// Gets or sets the message that is displayed in the confirmation box.
		/// </summary>
		/// <returns>The message to display.</returns>
		[Bindable(true), Category("MessageImageButton")]
		public string Message
		{
			get{ return (string) ViewState["Message"]; }
			set{ ViewState["Message"] = value; }
		}

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!string.IsNullOrEmpty(Message))
            {
                OnClientClick = @"return confirm('" + Message + "');";
            }
        }
	}

    public class Minifier : WebControl
    {
        [IDReferenceProperty]
        public string ControlToMinify { get; set; }

        protected override string TagName
        {
            get { return "span"; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            CssClass="minifier minify";

            Attributes.Add("minify", ControlToMinify);
        }
    }
}
