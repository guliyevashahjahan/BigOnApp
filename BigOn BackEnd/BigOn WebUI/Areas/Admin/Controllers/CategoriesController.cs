using BigOn.Business.Modules.BrandsModule.Commands.BrandEditCommand;
using BigOn.Business.Modules.BrandsModule.Queries.BrandGetByIdQuery;
using BigOn.Business.Modules.CategoryModule.Commands.CategoryAddCommand;
using BigOn.Business.Modules.CategoryModule.Commands.CategoryEditCommand;
using BigOn.Business.Modules.CategoryModule.Commands.CategoryRemoveCommand;
using BigOn.Business.Modules.CategoryModule.Queries.CategoryGetAllQuery;
using BigOn.Business.Modules.CategoryModule.Queries.CategoryGetByIdQuery;
using BigOn.Business.Modules.ColorsModule.Commands.ColorRemoveCommand;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BigOn_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IMediator mediator;

        public CategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [Authorize("admin.categories.index")]
        public async Task <IActionResult> Index(CategoryGetAllRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }
        [Authorize("admin.categories.create")]
        public async Task<IActionResult> Create()
        {
            var categories = await mediator.Send(new CategoryGetAllRequest());
            ViewBag.CategoryId = new SelectList(categories,"Id","Name");
            return View();
        }
        [HttpPost]
        [Authorize("admin.categories.create")]
        public async Task<IActionResult> Create(CategoryAddRequest request)
        {
            var response = await mediator.Send(request);
            return RedirectToAction(nameof(Index));
        }
        [Authorize("admin.categories.details")]
        public async Task<IActionResult> Details(CategoryGetByIdRequest request)
        {
            var model = await mediator.Send(request);
            return View(model);
        }
        [Authorize("admin.categories.edit")]
        public async Task<IActionResult> Edit(CategoryGetByIdRequest request)
        {
            var model = await mediator.Send(request);
            var categories = await mediator.Send(new CategoryGetAllRequest());
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            return View(model);
        }

        [HttpPost]
        [Authorize("admin.categories.edit")]
        public async Task<IActionResult> Edit(CategoryEditRequest request)
        {
            await mediator.Send(request);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Authorize("admin.categories.delete")]
        public async Task<IActionResult> Delete(CategoryRemoveRequest request)
        {
            await mediator.Send(request);
            return Json(new
            {
                error = false,
                message = "Item deleted successfully"
            });
        }
    }
}
