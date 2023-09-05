namespace UploadImageApi.Models.DTOs
{
    public class ImageDto
    {
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
        public string FileExtention { get; set; }
        public long FileSizeInBytes { get; set; }

        public string FilePath { get; set; }
    }
}
