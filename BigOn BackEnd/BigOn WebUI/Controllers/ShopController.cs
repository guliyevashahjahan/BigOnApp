using AngleSharp.Io;
using BigOn.Business.Modules.ShopModule.Queries.ComplexFilterQuery;
using BigOn.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BigOn_WebUI.Controllers
{
    
    public class ShopController : Controller
    {
        private readonly IMediator mediator;

        public ShopController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [AllowAnonymous]
        public async Task<IActionResult> Index(ComplexFilterRequest request)
        {
            request.Size = request.Size < 15 ? 15 : request.Size;
            var response = await mediator.Send(request);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Products", response);
            }
            return View(response);
        }


        [AllowAnonymous]
        public IActionResult Details()
        {
            return View();
        }
    }
}
