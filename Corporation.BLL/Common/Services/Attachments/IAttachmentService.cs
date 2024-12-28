using Microsoft.AspNetCore.Http;

namespace Corporation.BLL.Common.Services.Attachments
{
    public interface IAttachmentService
    {
        string Upload(IFormFile file, string folder);
     
        bool Delete(string filePath);
    }
}
