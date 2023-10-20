using BigOn.Business.Modules.SubscribeModule.Commands.SubscribeApproveCommand;
using BigOn.Business.Modules.SubscribeModule.Commands.SubscribeTicketCommand;
using BigOn.Data.Persistences;
using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Extensions;
using BigOn.Infrastructure.Repositories;
using BigOn.Infrastructure.Services.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;


namespace BigOn_WebUI.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ISubscriberRepository subscriberRepository;
        private readonly IMediator mediator;
        public HomeController(ISubscriberRepository subscriberRepository,IMediator mediator )
        {
            this.subscriberRepository = subscriberRepository;
            this.mediator = mediator;
        }
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
        public async Task<IActionResult> Subscribe(SubscribeTicketRequest request)
        {
            await mediator.Send(request);

            return Json(new
            {
                error = false,
                message = "Check your e-mail account for confirmation."
            });
        }

        [Route("/subscribe-approve.html")]
        public async Task<IActionResult>SubscribeApprove(SubscribeApproveRequest request)
        {
            await mediator.Send(request);
            TempData["Message"] = "Your subscription has been confirmed";
            return RedirectToAction(nameof(Index));
        }
    }

}
