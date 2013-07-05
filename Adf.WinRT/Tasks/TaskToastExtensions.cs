using Adf.Core.Resources;
using Adf.Core.Tasks;
using Windows.UI.Notifications;

namespace Adf.WinRT.Tasks
{
    public static class TaskToastExtensions
    {
        public static void Toast(this ITask task, string key, params object[] p)
        {
            
            var template = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText02);

            var elements = template.GetElementsByTagName("text");
            elements[0].AppendChild(template.CreateTextNode(ResourceManager.GetString(task.Name.ToString())));
            elements[1].AppendChild(template.CreateTextNode(string.Format(ResourceManager.GetString(key), p)));

            var toast = new ToastNotification(template);

            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }
    }
}
