namespace UploadImageApi.Validations
{
    public class ValidateFileUpload
    {
        public void ValidateFileUpload(Command.UploadImageCommand request)
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
