using MediatR;
using System.ComponentModel.DataAnnotations;
using UploadImageApi.Models.DTOs;

namespace UploadImageApi.Command
{
    public class UploadImageCommand: IRequest<ImageDto>
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }

        public string? FileDescription { get; set; }
    }
}
