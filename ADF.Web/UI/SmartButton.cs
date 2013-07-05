using System;
using System.Web.UI.WebControls;
using Adf.Core.Extensions;
using Adf.Core.Resources;

namespace Adf.Web.UI
{
    public class SmartButton : LinkButton
    {
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = ResourceManager.GetString(value); }
        }

        public override string ToolTip
        {
            get { return base.ToolTip; }
            set { base.ToolTip = ResourceManager.GetString(value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { _message = ResourceManager.GetString(value); }
        }

        public SmartButton()
        {
            base.CssClass = "tbutton";
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (!Message.IsNullOrEmpty()) OnClientClick = @"return confirm('" + Message + "');";
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!Enabled) OnClientClick = string.Empty;  // disable client click as well
        }
    }

    public class ClearButton : SmartButton
    {
        public ClearButton()
        {
            base.CssClass = "tbutton clearbutton";
        }
    }

    public class TextButton : SmartButton
    {
        public TextButton()
        {
            base.CssClass = "textbutton";
        }
    }
}
