namespace Adf.Core.Domain
{
    public interface IImageProvider
    {
        string ImageUrl { get; }
        string DefaultImageUrl { get; }
    }
}
