using System.Web.UI.WebControls;
using Adf.Core.Resources;

namespace Adf.Web.UI
{
    public class SmartText : Label
    {
        public override string Text
        {
            get { return base.Text; }
            set { base.Text = ResourceManager.GetString(value); }
        }

        public string Format { get; set; }

        public override string ToolTip
        {
            get { return base.ToolTip; }
            set { base.ToolTip = ResourceManager.GetString(value); }
        }

        public SmartText()
        {
            base.CssClass = "SmartText";
        }
    }

    public class SmallText : SmartText
    {
        public SmallText()
        {
            base.CssClass = "SmallText";
        }
    }

    public class Heading : SmartText
    {
        public Heading()
        {
            base.CssClass = "Heading";
        }
    }

    public static class SmartTextExtensions
    {
        public static void Format(this SmartText text, params object[] p)
        {
            text.Text = string.Format(text.Format ?? text.Text, p);
        }
    }
}
