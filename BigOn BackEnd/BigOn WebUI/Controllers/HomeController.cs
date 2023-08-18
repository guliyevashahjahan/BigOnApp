using Microsoft.AspNetCore.Mvc;

namespace BigOn_WebUI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Subscribe(string email)
        {
            return Json(new
            {
                error=false,
                email
            });
        }
        public IActionResult SubscribeApprove()
        {
            return Content("You've successfully subscribed!");
        }
    }

}
