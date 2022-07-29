using Microsoft.AspNetCore.Components.Forms;

namespace TangyWeb.Server.Service
{
    public class FileUpload : IFileUpload
    {
        readonly IWebHostEnvironment _webHostEnvironment;

        public FileUpload(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<bool> DeleteFile(string filePath)
        {
            var fullPath = _webHostEnvironment.WebRootPath + filePath;
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
                return true;
            }
            return false;
        }

        public async Task<string> UploadFile(IBrowserFile file)
        {
            var fileInfo = new FileInfo(file.Name);
            var fileName = Guid.NewGuid().ToString()+fileInfo.Extension;
            var savedFolder = $"{_webHostEnvironment.WebRootPath}\\images\\products";
            if (!Directory.Exists(savedFolder))
                Directory.CreateDirectory(savedFolder);
            var filePath = Path.Combine(savedFolder, fileName);

            await using FileStream fs = new FileStream(filePath, FileMode.Create);
            await file.OpenReadStream().CopyToAsync(fs);

            var fullPath = $"/images/products/{fileName}";
            return fullPath;

        }
    }
}
