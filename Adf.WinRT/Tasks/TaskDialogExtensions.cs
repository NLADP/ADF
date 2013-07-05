using Adf.Core.Resources;
using Adf.Core.Tasks;
using Windows.UI.Popups;

namespace Adf.WinRT.Tasks
{
    public static class TaskDialogExtensions
    {
        public static void YesNoDialog(this ITask task, UICommandInvokedHandler yes, UICommandInvokedHandler no, string key, params object[] p)
        {

            var header = ResourceManager.GetString(task.Name.ToString());
            var text = string.Format(ResourceManager.GetString(key), p);

            var dialog = new MessageDialog(text, header);

            dialog.Commands.Add(new UICommand("Yes", yes));
            dialog.Commands.Add(new UICommand("No", no));

            dialog.ShowAsync();            
        }
    }
}
