using Microsoft.AspNetCore.Http;

namespace Corporation.BLL.Common.Services.Attachments
{
    public class AttachmentService : IAttachmentService
    {

        private readonly List<string> _allowExtensions = new List<string>() { ".jpg", ".png", ".jpeg" };
        private const long _allowedSize = 2_097_152;

        public async Task<string> UploadAsync(IFormFile file, string folderName)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!_allowExtensions.Contains(extension))
                return null!;

            if (file.Length > _allowedSize)
                return null!;

            //var folderPath = $"{Directory.GetCurrentDirectory()}\\wwwroot\\files\\{folder}";
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", folderName);

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(folderPath, fileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(fileStream);

            return fileName;
        }

        public bool DeleteAsync(string filePath)
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }

            return false;
        }
    }
}
