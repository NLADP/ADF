using Adf.Core;

namespace Adf.Web
{
    public static class MediaTypeNames
    {
        public class MediaTypeName : Descriptor
        {
            public MediaTypeName(string name) : base(name) { }
        }

        public class Application : MediaTypeName
        {
            public Application(string name) : base(name) { }

            public static readonly Application Octet = new Application(System.Net.Mime.MediaTypeNames.Application.Octet);
            public static readonly Application Pdf = new Application(System.Net.Mime.MediaTypeNames.Application.Pdf);
            public static readonly Application Rtf = new Application(System.Net.Mime.MediaTypeNames.Application.Rtf);
            public static readonly Application Soap = new Application(System.Net.Mime.MediaTypeNames.Application.Soap);
            public static readonly Application Zip = new Application(System.Net.Mime.MediaTypeNames.Application.Zip);

            public static readonly Application Excel = new Application("application/vnd.ms-excel");
            public static readonly Application Json = new Application("application/json");
        }

        public class Text : MediaTypeName
        {
            public Text(string name) : base(name) { }

            public static readonly Text Html = new Text(System.Net.Mime.MediaTypeNames.Text.Html);
            public static readonly Text Plain = new Text(System.Net.Mime.MediaTypeNames.Text.Plain);
            public static readonly Text RichText = new Text(System.Net.Mime.MediaTypeNames.Text.RichText);
            public static readonly Text Xml = new Text(System.Net.Mime.MediaTypeNames.Text.Xml);
            
            public static readonly Text Csv = new Text("text/csv");
        }

        public class Image : MediaTypeName
        {
            public Image(string name) : base(name) { }

            public static readonly Image Gif = new Image(System.Net.Mime.MediaTypeNames.Image.Gif);
            public static readonly Image Tiff = new Image(System.Net.Mime.MediaTypeNames.Image.Tiff);
            public static readonly Image Jpeg = new Image(System.Net.Mime.MediaTypeNames.Image.Jpeg);
        }
    }
}
