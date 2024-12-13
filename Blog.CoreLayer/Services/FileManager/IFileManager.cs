using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.CoreLayer.Services.FileManager
{
    public interface IFileManager
    {
       // string SaveImageAndReturnImageName(IFormFile file, string savePath);
        string SaveFile(IFormFile file , string savePath);
        void DeleteFile(string fileName, string path);
    }
}
