using BigOn.Business.Modules.BrandsModule.Commands.BrandEditCommand;
using BigOn.Business.Modules.BrandsModule.Queries.BrandGetByIdQuery;
using BigOn.Business.Modules.CategoryModule.Commands.CategoryAddCommand;
using BigOn.Business.Modules.CategoryModule.Commands.CategoryEditCommand;
using BigOn.Business.Modules.CategoryModule.Commands.CategoryRemoveCommand;
using BigOn.Business.Modules.CategoryModule.Queries.CategoryGetAllQuery;
using BigOn.Business.Modules.CategoryModule.Queries.CategoryGetByIdQuery;
using BigOn.Business.Modules.ColorsModule.Commands.ColorRemoveCommand;
using MediatR;
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
        public async Task <IActionResult> Index(CategoryGetAllRequest request)
        {
            var response = await mediator.Send(request);
            return View(response);
        }
        public async Task<IActionResult> Create()
        {
            var categories = await mediator.Send(new CategoryGetAllRequest());
            ViewBag.CategoryId = new SelectList(categories,"Id","Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryAddRequest request)
        {
            var response = await mediator.Send(request);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(CategoryGetByIdRequest request)
        {
            var model = await mediator.Send(request);
            return View(model);
        }
        public async Task<IActionResult> Edit(CategoryGetByIdRequest request)
        {
            var model = await mediator.Send(request);
            var categories = await mediator.Send(new CategoryGetAllRequest());
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CategoryEditRequest request)
        {
            await mediator.Send(request);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
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
