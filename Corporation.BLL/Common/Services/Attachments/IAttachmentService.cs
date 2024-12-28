using Microsoft.AspNetCore.Http;

namespace Corporation.BLL.Common.Services.Attachments
{
    public interface IAttachmentService
    {
        Task<string> UploadAsync(IFormFile file, string folder);

        bool DeleteAsync(string filePath);
    }
}
