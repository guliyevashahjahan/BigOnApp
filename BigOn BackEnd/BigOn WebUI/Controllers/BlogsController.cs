using BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetAllQuery;
using BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetBySlugQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BigOn_WebUI.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IMediator mediator;

        public BlogsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IActionResult> Index(BlogPostGetAllRequest request)
        {
            var model = await mediator.Send(request);
            return View(model);
        }
        [Route("blogs/{slug}")]
        public async Task<IActionResult> Details(BlogPostGetBySlugRequest request)
        {
            var model = await mediator.Send(request);
            return View(model);
        }
    }
}
