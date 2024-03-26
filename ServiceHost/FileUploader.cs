using _0_FrameWork.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ServiceHost
{
    public class FileUploader : IFileUploader
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileUploader(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string Upload(IFormFile file,string Path)
        {
            if (file==null)
            {
                return "";
            }
           var DirectoryPath = $"{ _webHostEnvironment.WebRootPath}//ProductPictures//{Path}//";
            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }
            var filePath =$"{DirectoryPath}//{file.FileName}";
            using var outPut = File.Create(filePath);
            file.CopyToAsync(outPut);
            return $"{Path}//{file.FileName}";

        }
    }
}
