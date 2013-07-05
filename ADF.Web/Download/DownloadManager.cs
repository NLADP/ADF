using System;
using System.Threading;
using System.Web;
using Adf.Core.Identity;
using Adf.Core.Logging;
using Adf.Core.State;
using Adf.Web.Extensions;

namespace Adf.Web.Download
{
    public static class DownloadManager
    {
        private const string Key = "DownloadManager.CurrentDownload";
        private const string IdKey = "DownloadManager.CurrentID";

        public static IDownloadFile CurrentDownload
        {
            get { return StateManager.Personal[Key] as IDownloadFile; }
            set { StateManager.Personal[Key] = value; }
        }

        public static ID CurrentId
        {
            get { return new ID(StateManager.Personal[IdKey]); }
            set { StateManager.Personal[IdKey] = value; }
        }

        public static Func<IDownloadFile> CurrentDownloadAsync
        {
            get { return () => CurrentDownload; }
        }

        public static string GenerateUrl(string downloadPage, string downloadIdVariableName)
        {
            if (string.IsNullOrEmpty(downloadPage)) throw new ArgumentNullException("downloadPage");
            if (string.IsNullOrEmpty(downloadIdVariableName)) throw new ArgumentNullException("downloadIdVariableName");

            CurrentId = IdManager.New<Guid>();
            return string.Format("window.open('../{0}?{1}={2}', '_blank')", downloadPage, downloadIdVariableName, CurrentId);
        }

        public static void DownloadCurrentFile(this HttpResponse response)
        {
            LogManager.Log(LogLevel.Verbose, "Downloading file:");

            var file = CurrentDownload;

            if (file == null) throw new InvalidOperationException("There is no current download.");

            //        CurrentDownload = null;   //  read-only session.. (will be cleared in UserMenu when starting new task from menu)

            if (file.Data != null)
            {
                LogManager.Log(LogLevel.Verbose, file.Id.ToString());

                response.WriteDocument(file.Data, file.ContentType, file.FileName);
            }
            else
            {
                response.Write("<script>window.close();</script>");
            }
        }

        public static void QueueDownload(byte[] data, MediaTypeNames.MediaTypeName contentType, string fileName)
        {
            var file = new DownloadFile(CurrentId)
            {
                Data = data,
                ContentType = contentType,
                FileName = fileName
            };

            QueueDownload(file);
        }

        public static void QueueDownload(IDownloadFile file)
        {
            if (file == null) throw new ArgumentNullException("file");

            LogManager.Log(LogLevel.Verbose, "Queueing download " + file.Id);

            CurrentDownload = file; // set file ready for download
        }

        public static void CancelDownload()
        {
            QueueDownload(null, null, null);
        }

        public static T WaitFor<T>(this Func<T> file, ID downloadId, TimeSpan timeout = default(TimeSpan)) where T : IDownloadFile
        {
            LogManager.Log(LogLevel.Verbose, "Waiting for download " + downloadId);

            if (timeout == TimeSpan.Zero) timeout = new TimeSpan(0, 0, HttpContext.Current.Server.ScriptTimeout);

            var start = DateTime.Now;

            T waitFor = default(T);

            while (waitFor == null || waitFor.Id != downloadId)
            {
                if ((DateTime.Now - start) > timeout)
                {
                    LogManager.Log("File download timed out: " + downloadId);
                    return default(T);
                }

                Thread.Sleep(TimeSpan.FromMilliseconds(500));

                waitFor = file.Invoke();
            }

            return waitFor;
        }

        private class DownloadFile : IDownloadFile
        {
            public DownloadFile(ID id)
            {
                Id = id;
            }

            public ID Id { get; private set; }
            public MediaTypeNames.MediaTypeName ContentType { get; set; }
            public string FileName { get; set; }
            public byte[] Data { get; set; }
        }
    }
}
