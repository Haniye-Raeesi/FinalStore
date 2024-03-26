using Microsoft.AspNetCore.Http;

namespace _0_FrameWork.Application
{
   public interface IFileUploader
    {
        public string Upload(IFormFile file,string Path);

    }
}
