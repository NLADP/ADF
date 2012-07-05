namespace Adf.Web.UI.SmartView
{
    public class SelectButton : IconButton
    {
        public SelectButton()
        {
            CommandName = "Select";
            IconFormat = "~/Images/{0}";
            Icon = "bullet_select.gif";
            Header = "";
            ToolTipFormat = "Click to select {0}";
            ToolTipField = "Title";
        }
    }
}
