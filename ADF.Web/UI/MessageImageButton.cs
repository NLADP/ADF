using System;
using System.ComponentModel;
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
}
