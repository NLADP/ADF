using Adf.Core.Identity;

namespace Adf.Web.Download
{
    public interface IDownloadFile
    {
        /// <summary>
        /// Gets the unique file Id
        /// </summary>
        ID Id { get; }

        /// <summary>
        /// Gets or sets the ContentType of the file
        /// </summary>
        MediaTypeNames.MediaTypeName ContentType { get; set; }

        /// <summary>
        /// Gets or sets the file as a byte array
        /// </summary>
        byte[] Data { get; set; }

        /// <summary>
        /// Gets or sets the new filename for the file
        /// </summary>
        string FileName { get; set; }
    }
}
