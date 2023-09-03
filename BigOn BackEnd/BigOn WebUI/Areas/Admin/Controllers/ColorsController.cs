using BigOn.Business.Modules.ColorsModule.Commands.ColorAddCommand;
using BigOn.Business.Modules.ColorsModule.Commands.ColorEditCommand;
using BigOn.Business.Modules.ColorsModule.Commands.ColorRemoveCommand;
using BigOn.Business.Modules.ColorsModule.Queries.ColorGetAllQuery;
using BigOn.Business.Modules.ColorsModule.Queries.ColorGetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BigOn_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorsController : Controller
    {
        private readonly IMediator mediator;

        public ColorsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<IActionResult> Index(ColorGetAllRequest request)
        {
          var response =await mediator.Send(request);

            return View(response);
        }
        public async Task<IActionResult> Details(ColorGetByIdRequest request)
        {
           var response = await mediator.Send(request);

            return View(response);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ColorAddRequest request)
        {
           var response =  await mediator.Send(request);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(ColorGetByIdRequest request)
        {

            var response = await mediator.Send(request);

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ColorEditRequest request)
        {

            await mediator.Send(request);
            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> Delete(ColorRemoveRequest request)
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
