using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Adf.Web.UI
{
    public class Minifier : WebControl
    {
        [IDReferenceProperty]
        public string ControlToMinify { get; set; }

        public string ClassToMinify { get; set; }

        public string ImageClass { get; set; }

        private bool _startMinified;
        public bool StartMinified
        {
            get { return _startMinified; }
            set
            {
                _startMinified = value;
                SetCssClasses();
            }
        }

        protected override string TagName
        {
            get { return "span"; }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            SetCssClasses();

            Attributes.Add("minifyControl", ControlToMinify);
            Attributes.Add("minifyClass", ClassToMinify);
            Attributes.Add("imageClass", ImageClass);
        }

        private void SetCssClasses()
        {
            CssClass = "minifier " + (StartMinified ? "maxify" : "minify");

            if (!string.IsNullOrEmpty(ImageClass))
                CssClass += "-" + ImageClass;
        }
    }
}