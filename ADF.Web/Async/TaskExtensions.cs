using System;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.SessionState;
using Adf.Base.Tasks;
using Adf.Core.Validation;
using Task = System.Threading.Tasks.Task;

namespace Adf.Web.Async
{
    public static class TaskExtensions
    {
        public static Task RunAsync<T>(this T data, Action<T> action) where T : AsyncTaskData
        {
            var context = HttpContext.Current;
            var sessionState = SessionStateUtility.GetHttpSessionStateFromContext(HttpContext.Current);
            var identity = WindowsIdentity.GetCurrent();
            if (identity == null) throw new InvalidOperationException("no identity");

            data.Status = AsyncTaskData.StatusCode.Working;

            return Task.Factory.StartNew(o =>
            {
                RestoreIdentity(identity);
                RestoreSession(sessionState, context);

                action.Invoke((T)o);
            }, data, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default)

                .ContinueWith(task =>
                {
                    RestoreIdentity(identity);

                    ValidationManager.AddError(@"An error has occured. The server is experiencing a problem with the page you requested. 
                                             Please try again and if the problem persists, notify your system administrator. We apologize for the inconvenience.");
                    data.Status = AsyncTaskData.StatusCode.Error;
                }, TaskContinuationOptions.OnlyOnFaulted);
        }

        private static void RestoreSession(IHttpSessionState sessionState, HttpContext context)
        {
            HttpContext.Current = context;

            while (HttpContext.Current.Session != null) // wait for original request to end and remove the session before attaching the session again
            {
                Thread.Sleep(200);
            }
            SessionStateUtility.AddHttpSessionStateToContext(HttpContext.Current, sessionState); // re-attach session
        }

        private static void RestoreIdentity(WindowsIdentity identity)
        {
            identity.Impersonate();

            Thread.CurrentPrincipal = new WindowsPrincipal(identity);
        }
    }
}
