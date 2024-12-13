using Blog.CoreLayer.Services.FileManager;
using Blog.CoreLayer.Utlilties;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.web.Areas.Admin.Controllers
{
    public class UploadController : Controller
    {
        private readonly IFileManager _fileManager;
        public UploadController(IFileManager fileManager)
        {
            _fileManager = fileManager;
        }
        [Route("/Upload/Article")]
        public IActionResult Article(IFormFile upload)
        {
            if (upload == null)
                BadRequest();
            var ImageName = _fileManager.SaveFile(upload, Directories.PostContentImage);

            return new JsonResult(new {Uploaded = true , Url = Directories.GetPostContentImage(ImageName) });
        }
    }
}
