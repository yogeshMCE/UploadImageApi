using UploadImageApi.Models.Domain;

namespace UploadImageApi.Repository
{
    public interface IImageRepository
    {
        Task<Image> UploadImageAsync(Image image);
    }
}
