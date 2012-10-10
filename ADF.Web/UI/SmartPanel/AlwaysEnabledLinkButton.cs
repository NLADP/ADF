using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adf.Web.UI
{
    internal class AlwaysEnabledLinkButton : LinkButton
    {
        public override bool SupportsDisabledAttribute
        {
            get { return false; }
        }

        protected override void AddAttributesToRender(System.Web.UI.HtmlTextWriter writer)
        {
            base.AddAttributesToRender(writer);

            if (IsEnabled) return;

            PostBackOptions options = GetPostBackOptions();
            string postBackEventReference = null;
            if (options != null)
            {
                postBackEventReference = Page.ClientScript.GetPostBackEventReference(options, true);
            }

            // If the postBackEventReference is empty, use a javascript no-op instead, since 
            // <a href="" /> is a link to the root of the current directory. 
            if (String.IsNullOrEmpty(postBackEventReference))
            {
                postBackEventReference = "javascript:void(0)";
            }

            writer.AddAttribute(HtmlTextWriterAttribute.Href, postBackEventReference);
        }
    }
}