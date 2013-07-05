using System.Globalization;
using System.Web;
using Adf.Core.Logging;

namespace Adf.Web.Extensions
{
    public static class WebExtensions
    {
        public static void WriteDocument(this HttpResponse response, byte[] buffer, MediaTypeNames.MediaTypeName contentType, string filename)
        {
            try
            {
                response.ClearContent();
                response.ClearHeaders();
                response.Buffer = false;
                response.Cache.SetCacheability(HttpCacheability.Private);
                response.ContentType = contentType.Name;
                response.AppendHeader("Content-Disposition", string.Format("attachment; filename=\"{0}\"", filename));
                response.AppendHeader("Content-Length", buffer.Length.ToString(CultureInfo.InvariantCulture));

                LogManager.Log(LogLevel.Verbose, "write bytes: " + buffer.Length);
                response.BinaryWrite(buffer);
                LogManager.Log(LogLevel.Verbose, "Done writing");
            }
            finally
            {
                LogManager.Log(LogLevel.Verbose, "ending response..");
                HttpContext.Current.ApplicationInstance.CompleteRequest();
                response.End();
            }
        }
    }
}
