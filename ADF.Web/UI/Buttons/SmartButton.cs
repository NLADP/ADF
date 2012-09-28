using System;
using System.Web.UI.WebControls;
using Adf.Core.Domain;
using Adf.Core.Extensions;
using Adf.Core.Resources;

namespace Adf.Web.UI.Buttons
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

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (CssClass.IsNullOrEmpty()) CssClass = "tbutton";
            if (!Message.IsNullOrEmpty()) OnClientClick = @"return confirm('" + Message + "');";
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            if (!Enabled) OnClientClick = string.Empty;  // disable client click as well
        }
    }
}
