using BigOn.Business.Modules.BlogPostModule.Commands.BlogPostAddCommand;
using BigOn.Business.Modules.BlogPostModule.Commands.BlogPostEditCommand;
using BigOn.Business.Modules.BlogPostModule.Commands.BlogPostRemoveCommand;
using BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetAllQuery;
using BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetByIdQuery;
using BigOn.Business.Modules.BlogPostModule.Queries.TagGetUsedQuery;
using BigOn.Business.Modules.CategoryModule.Queries.CategoryGetAllQuery;
using BigOn.Business.Modules.ColorsModule.Commands.ColorRemoveCommand;
using BigOn.Business.Modules.ColorsModule.Queries.ColorGetByIdQuery;
using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Services.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BigOn_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogPostsController : Controller
    {
        private readonly IMediator mediator;

        public BlogPostsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IActionResult>  Index(BlogPostGetAllRequest request)
        {
            var model = await mediator.Send(request);
            return View(model);
        }
        public async Task<IActionResult> Create()
        {
            var categories = await mediator.Send(new CategoryGetAllRequest());
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            var tags = await mediator.Send(new TagGetUsedRequest());
            ViewBag.Tags = new SelectList(tags, "Text", "Text");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>  Create(BlogPostAddRequest request)
        {
            
          var reponse = await  mediator.Send(request);
          return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(BlogPostRemoveRequest request)
        {
            await mediator.Send(request);
            return Json(new
            {
                error = false,
                message = "Item deleted successfully"

            });
        }
        public async Task<IActionResult> Details(BlogPostGetByIdRequest request)
        {
            var response = await mediator.Send(request);

            return View(response);
        }
        public async Task<IActionResult> Edit(BlogPostGetByIdRequest request)
        {
            var categories = await mediator.Send(new CategoryGetAllRequest());
            ViewBag.CategoryId = new SelectList(categories, "Id", "Name");
            var tags = await mediator.Send(new TagGetUsedRequest());
            ViewBag.Tags = new SelectList(tags, "Text", "Text");
            var response = await mediator.Send(request);

            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(BlogPostEditRequest request)
        {
            var response = await mediator.Send(request);

            return RedirectToAction(nameof (Index));
        }
    }
}
