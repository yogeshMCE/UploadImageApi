using System.ComponentModel.DataAnnotations;

namespace UploadImageApi.Models.DTOs
{
    public class UploadImageDto
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string FileName { get; set; }

        public string? FileDescription { get; set; }

    }
}
