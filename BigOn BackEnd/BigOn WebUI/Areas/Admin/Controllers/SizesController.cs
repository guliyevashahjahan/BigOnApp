using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BigOn_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SizesController : Controller
    {
        private readonly IMediator mediator;

        public SizesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
