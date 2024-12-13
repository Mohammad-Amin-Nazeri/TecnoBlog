using Blog.Web.Areas.Admin;
using Microsoft.AspNetCore.Mvc;

namespace Blog.web.Araes.Admin.Controllers
{
    public class HomeController : AdminControllerBase
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
