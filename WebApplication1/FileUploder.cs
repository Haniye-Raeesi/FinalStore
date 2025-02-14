﻿using _0_FrameWork.Application;

namespace WebApplication1
{
    public class FileUploder
    {
        public class FileUploader : IFileUploader
        {
            private readonly IWebHostEnvironment _webHostEnvironment;

            public FileUploader(IWebHostEnvironment webHostEnvironment)
            {
                _webHostEnvironment = webHostEnvironment;
            }

            public string Upload(IFormFile file, string Path)
            {
                if (file == null)
                {
                    return "";
                }
                var DirectoryPath = $"{ _webHostEnvironment.WebRootPath}//ProductPictures//{Path}//";
                if (!Directory.Exists(DirectoryPath))
                {
                    Directory.CreateDirectory(DirectoryPath);
                }
                var filePath = $"{DirectoryPath}//{file.FileName}";
                using var outPut = File.Create(filePath);
                file.CopyToAsync(outPut);
                return $"{Path}//{file.FileName}";

            }
        }
    }
}
