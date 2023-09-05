using MediatR;
using Microsoft.AspNetCore.Mvc;
using UploadImageApi.Command;


namespace UploadImageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
       
        private readonly IMediator mediator;

        public ImagesController( IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        [Route("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadImageCommand request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                var imageDto = await mediator.Send(request);
                return Ok(imageDto);
            }
            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(UploadImageCommand request)
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
