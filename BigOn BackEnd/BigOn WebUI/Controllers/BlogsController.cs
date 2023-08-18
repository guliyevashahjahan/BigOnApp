using Microsoft.AspNetCore.Mvc;

namespace BigOn_WebUI.Controllers
{
    public class BlogsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }
    }
}
