using AngleSharp.Io;
using BigOn.Business.Modules.ShopModule.Queries.ComplexFilterQuery;
using BigOn.Business.Modules.ShopModule.Queries.ProductCatalogQuery;
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
            request.Size = request.Size < 16 ? 16 : request.Size;
            var response = await mediator.Send(request);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Products", response);
            }
            return View(response);
        }

        [AllowAnonymous]
        [Route("/shop/details/{productId}")]
        public async Task<IActionResult> Details([FromRoute] ProductCatalogRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [AllowAnonymous]
        public async Task<IActionResult> ProductCatalog(ProductCatalogRequest request)
        {
            var response = await mediator.Send(request);

            return PartialView("_ChooseProduct", response);
        }
    }
}
