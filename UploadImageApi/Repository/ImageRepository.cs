using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using UploadImageApi.Data;
using UploadImageApi.Models.Domain;

namespace UploadImageApi.Repository
{
    public class ImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly ImageDbContext dbContext;
        private readonly IConfiguration configuration;
        private readonly Account account;

        public ImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor,ImageDbContext dbContext, IConfiguration configuration)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
            this.configuration = configuration;
            account = new Account(
                configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["ApiKey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]);
        }
        public async Task<Image> UploadImageAsync(Image image)
        {
            // var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtention}");

            //  using var stream = new FileStream(localFilePath, FileMode.Create);
            //   await image.File.CopyToAsync(stream);

            //https://localhost:1234/images/image.jpg
            //    var imageurl = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtention}";

            var client = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(image.File.FileName, image.File.OpenReadStream()),
                DisplayName = image.File.FileName
            };

            var uploadResult = await client.UploadAsync(uploadParams);

            if (uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                image.FilePath = uploadResult.SecureUri.ToString();
            }     
            await dbContext.images.AddAsync(image);
            await dbContext.SaveChangesAsync();
            return image;


        }
    }
}
