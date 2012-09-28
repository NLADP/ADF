namespace Adf.Web.UI.SmartView
{
    public class DeleteButton : IconButton
    {
        public DeleteButton()
        {
            MessageFormat = "Do you want to delete {0}?";
            MessageField = "Title";
            MessageSubject = "item";
            CommandName = "Delete";
            IconFormat = "~/Images/{0}";
            Icon = "bullet_delete.png";
            Header = "Delete";
            ToolTipFormat = "Click to delete {0}";
            ToolTipField = "Title";
        }
    }
}
