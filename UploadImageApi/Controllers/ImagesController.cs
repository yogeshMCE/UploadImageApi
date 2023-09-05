using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UploadImageApi.Models.Domain;
using UploadImageApi.Models.DTOs;
using UploadImageApi.Repository;

namespace UploadImageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;
        private readonly IMapper mapper;

        public ImagesController( IImageRepository imageRepository, IMapper mapper)
        {
            this.imageRepository = imageRepository;
            this.mapper = mapper;
        }
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadImageDto request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                // convert DTO to DomainModel
                var ImageDomainModel = new Image
                {
                    File = request.File,
                    FileExtention = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes= request.File.Length,
                    FileName = request.FileName,
                    FileDescription=request.FileDescription,
                };
                await imageRepository.UploadImageAsync(ImageDomainModel);

                return Ok(mapper.Map<ImageDto>(ImageDomainModel));
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(UploadImageDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtensions.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("File", "UnSupported File");
            }
            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File Size is More Than 10KB");
            }
        }

    }
  
}
