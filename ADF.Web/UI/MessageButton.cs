using System;
using System.ComponentModel;
using System.Web.UI;

namespace Adf.Web.UI
{
	/// <summary>
	/// Represents control that shows a link with a javascript confirmation box.
	/// </summary>
	public class MessageButton: Control, IPostBackEventHandler
	{
        /// <summary>
        /// Declaring an event of type <see cref="EventHandler"/>.
        /// </summary>
		public event EventHandler Click;

		/// <summary>
		/// Raises the click event when the confirmation button is clicked.
		/// </summary>
		/// <param name="eventArgument">The event argument.</param>
		public void RaisePostBackEvent(string eventArgument)
		{
			if (Click != null) Click(this, new EventArgs());
		}

		/// <summary>
		/// Gets or sets the message that is displayed in the confirmation box.
		/// </summary>
		/// <returns>The message to display.</returns>
		[Bindable(true), Category("MessageButton")]
		public string Message
		{
			get{ return (string) ViewState["Message"]; }
			set{ ViewState["Message"] = value; }
		}

		/// <summary>
		/// Gets or sets the class attribute.
		/// </summary>
        /// <returns>The class attribute.</returns>
		[Bindable(true), Category("MessageButton")]
		public string Class
		{
			get{ return (string) ViewState["Class"]; }
			set{ ViewState["Class"] = value; }
		}	
		
		/// <summary>
		/// Gets or sets the text that is used for the link.
		/// </summary>
        /// <returns>The link text.</returns>
		[Bindable(true), Category("MessageButton")]
		public string Text
		{
			get{ return (string) ViewState["CssClass"]; }
			set{ ViewState["CssClass"] = value; }
		}

		/// <summary>
		/// Gets or sets if the button is enabled.
		/// </summary>
        /// <returns>The link text.</returns>
		[Bindable(true), Category("MessageButton")]
		public bool Enabled
		{
            get
            {
                var enabled = ViewState["Enabled"];

                return (enabled == null) || (bool) enabled; // default Enabled
            }
            set { ViewState["Enabled"] = value; }
		}

		/// <summary>
		/// Renders the control to the specified <see cref="System.Web.UI.HtmlTextWriter"/>.
		/// </summary>
		/// <param name="writer">The output <see cref="System.Web.UI.HtmlTextWriter"/>.</param>
		protected override void Render(HtmlTextWriter writer)
		{
			writer.WriteBeginTag("a");

            if (Enabled)
            {
                if (string.IsNullOrEmpty(Message))
                {
                    writer.WriteAttribute("href", "javascript:" + Page.ClientScript.GetPostBackEventReference(this, ""));
                }
                else
                {
                    writer.WriteAttribute("href",
                                          "javascript: if (confirm('" + Message.Replace("'", "\\'") + "')) {" +
                                          Page.ClientScript.GetPostBackEventReference(this, "") + "}");
                }
            }
		    else
		    {
                writer.WriteAttribute("disabled", "disabled");
		    }
				
			if (!string.IsNullOrEmpty(Class)) writer.WriteAttribute("class", Class);

			writer.Write (HtmlTextWriter.TagRightChar);
			writer.Write(Text);

			base.Render(writer);

			writer.WriteEndTag("a");
		}
	}
}
