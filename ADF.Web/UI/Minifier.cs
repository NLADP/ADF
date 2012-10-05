using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adf.Web.UI
{
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