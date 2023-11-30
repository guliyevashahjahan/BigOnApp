using AngleSharp.Io;
using BigOn.Business.Modules.ShopModule.Commands.AddBasketCommand;
using BigOn.Business.Modules.ShopModule.Commands.CreateOrderCommand;
using BigOn.Business.Modules.ShopModule.Commands.RemoveFromBasketCommand;
using BigOn.Business.Modules.ShopModule.Commands.SetRateCommand;
using BigOn.Business.Modules.ShopModule.Queries.BasketListQuery;
using BigOn.Business.Modules.ShopModule.Queries.ComplexFilterQuery;
using BigOn.Business.Modules.ShopModule.Queries.GetPriceQuery;
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

        [HttpPost]
        public async Task<IActionResult> SetRate(SetRateRequest request)
        {
            var response = await mediator.Send(request);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> GetPrice(GetPriceRequest request)
        {
            var response = await mediator.Send(request);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddToBasket(BasketAddRequest request)
        {
            var response = await mediator.Send(request);
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> RemoveFromBasket(RemoveFromBasketRequest request)
        {
            var response = await mediator.Send(request);
            return Json(response);
        }
        public async Task<IActionResult> Basket([FromRoute] BasketListRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }
        public async Task<IActionResult> Checkout(BasketListRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CreateOrderRequest request)
        {
            var response = await mediator.Send(request);
            return Json(response);
        }


        [AllowAnonymous]
        public async Task<IActionResult> ProductCatalog(ProductCatalogRequest request)
        {
            var response = await mediator.Send(request);

            return PartialView("_ChooseProduct", response);
        }
    }
}
