using Microsoft.AspNetCore.Components.Forms;

namespace TangyWeb.Server.Service
{
    public interface IFileUpload
    {
        Task<string> UploadFile(IBrowserFile file);
        Task<bool> DeleteFile(string filePath);
    }
}
