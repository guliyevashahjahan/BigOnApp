using Bigon.Infrastructure.Middlewares;
using BigOn.Business.Modules.BrandsModule.Queries.BrandGetAllQuery;
using BigOn.Business.Modules.CategoryModule.Queries.CategoryGetAllQuery;
using BigOn.Business.Modules.ColorsModule.Queries.ColorGetAllQuery;
using BigOn.Business.Modules.MaterialsModule.Queries.MaterialGetAllQuery;
using BigOn.Business.Modules.ShopModule.Commands.ProductAddCommand;
using BigOn.Business.Modules.ShopModule.Commands.ProductEditCommand;
using BigOn.Business.Modules.ShopModule.Queries.ProductGetAllQuery;
using BigOn.Business.Modules.ShopModule.Queries.ProductGetByIdQuery;
using BigOn.Business.Modules.SizesModule.Queries.SizeGetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BigOn_WebUI.Areas.Admin.Controllers
{
    [Area("admin")]
    public class ProductController : Controller
    {
        private readonly IMediator mediator;

        public ProductController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [HttpPost]
        [Transaction]
        [Authorize("admin.products.create")]
        public async Task<IActionResult> Create(ProductAddRequest request)
        {
            var response =await mediator.Send(request);
            return Json(response);
        }
        [Authorize("admin.products.create")]
        public async Task<IActionResult> Create()
        {
            var brands =await mediator.Send(new BrandGetAllRequest());
            ViewBag.BrandId = new SelectList(brands,"Id","Name");
            var categories = await mediator.Send(new CategoryGetAllRequest());
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name"); 
            return View();
        }

        [HttpPost]
        [Authorize("admin.products.productcatalogitem")]
        public async Task<IActionResult> ProductCatalogItem()
        {
            ViewBag.ColorId = new SelectList(await mediator.Send(new ColorGetAllRequest()), "Id", "Name");
            ViewBag.SizeId = new SelectList(await mediator.Send(new SizeGetAllRequest()), "Id", "ShortName");
            ViewBag.MaterialId = new SelectList(await mediator.Send(new MaterialGetAllRequest()), "Id", "Name");
            return PartialView("_ProductCatalogItem");
        }
        [Authorize("admin.products.index")]
        public async Task<IActionResult> Index(ProductGetAllRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }
        [Authorize("admin.products.details")]
        public async Task<IActionResult> Details([FromRoute] ProductGetByIdRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }

        [Authorize("admin.products.edit")]
        public async Task<IActionResult> Edit([FromRoute] ProductGetByIdRequest request)
        {
            var response = await mediator.Send(request);

            if (response == null)
                return NotFound();

            ViewBag.BrandId = new SelectList(await mediator.Send(new BrandGetAllRequest()), "Id", "Name");
            ViewBag.CategoryId = new SelectList(await mediator.Send(new CategoryGetAllRequest()), "Id", "Name");
            ViewBag.ColorId = new SelectList(await mediator.Send(new ColorGetAllRequest()), "Id", "Name");
            ViewBag.SizeId = new SelectList(await mediator.Send(new SizeGetAllRequest()), "Id", "ShortName");
            ViewBag.MaterialId = new SelectList(await mediator.Send(new MaterialGetAllRequest()), "Id", "Name");
            return View(response);
        }

        [HttpPost]
        [Transaction]
        [Authorize("admin.products.edit")]
        public async Task<IActionResult> Edit(ProductEditRequest request)
        {
            var response = await mediator.Send(request);
            return Json(response);
        }
    }
}
