using BigOn.Business.Modules.BlogPostModule.Commands.BlogPostAddComment;
using BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetAllQuery;
using BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetBySlugQuery;
using BigOn.Business.Modules.BlogPostModule.Queries.BlogPostRecentsQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public async Task<IActionResult> Index(BlogPostGetAllRequest request)
        {
            var model = await mediator.Send(request);
           
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("blogs/{slug}")]
        public async Task<IActionResult> Details(BlogPostGetBySlugRequest request)
        {
            var model = await mediator.Send(request);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(BlogPostAddCommentRequest  request)
        {
            var model = await mediator.Send(request);
            return Json(model);
        }
    }
}
