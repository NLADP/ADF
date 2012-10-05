using System;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Web.Styling;

namespace Adf.Web.UI.Extensions
{
    public static class WebControlExtensions
    {
        public static WebControl ToggleStyle(this WebControl control, bool needsstyle, string styletrue, string stylefalse)
        {
            return control.ToggleStyle(needsstyle ? styletrue : stylefalse);
        }

        public static WebControl ToggleStyle(this WebControl control, bool needsstyle, CssClass styletrue, CssClass stylefalse)
        {
            return control.ToggleStyle(needsstyle, styletrue.Name, stylefalse.Name);
        }

        public static WebControl ToggleStyle(this WebControl control, bool needsstyle, string style)
        {
            return needsstyle ? control.ToggleStyle(style) : control;
        }

        public static WebControl ToggleStyle(this WebControl control, bool needsstyle, CssClass style)
        {
            return control.ToggleStyle(style.Name);
        }

        public static WebControl ToggleStyle(this WebControl control, CssClass style)
        {
            return control.ToggleStyle(style.Name);
        }

        public static WebControl ToggleStyle(this WebControl control, string style)
        {
            if (control == null) throw new ArgumentNullException("control");

            if (style.IsNullOrEmpty()) return control;

            control.CssClass = control.CssClass.Toggle(style, "{0} {1}");

            return control;
        }

        public static WebControl AddStyle(this WebControl control, string style)
        {
            if (control == null) throw new ArgumentNullException("control");

            if (style.IsNullOrEmpty() || control.CssClass.Contains(style)) return control;

            control.CssClass = string.Format("{0} {1}", control.CssClass, style);
            
            return control;
        }

        public static WebControl AddStyle(this WebControl control, CssClass style)
        {
            return control.AddStyle(style.Name);
        }
    }
}