using BigOn.Business.Modules.BlogPostModule.Commands.BlogPostAddCommand;
using BigOn.Business.Modules.BlogPostModule.Commands.BlogPostEditCommand;
using BigOn.Business.Modules.BlogPostModule.Commands.BlogPostPublishCommand;
using BigOn.Business.Modules.BlogPostModule.Commands.BlogPostRemoveCommand;
using BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetAllQuery;
using BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetByIdQuery;
using BigOn.Business.Modules.BlogPostModule.Queries.TagGetUsedQuery;
using BigOn.Business.Modules.CategoryModule.Queries.CategoryGetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BigOn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly IMediator mediator;

        public BlogsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        // [Authorize("admin.blogposts.index")]
        public async Task<IActionResult> Get([FromRoute] BlogPostGetAllRequest request)
        {
            var model = await mediator.Send(request);
            return Ok(model);
        }

        [HttpGet("{id}")]
        //[Authorize("admin.blogposts.details")]
        public async Task<IActionResult> GetById([FromRoute] BlogPostGetByIdRequest request)
        {
            var response = await mediator.Send(request);

            return Ok(response);
        }


        [HttpPost]
        //[Authorize("admin.blogposts.create")]

        public async Task<IActionResult> Add([FromForm] BlogPostAddRequest request)
        {

            var response = await mediator.Send(request);
            return CreatedAtAction(nameof(GetById),routeValues: new { id = response.Id},response);
        }

        [HttpPost("{id}")]
        //[Authorize("admin.blogposts.delete")]

        public async Task<IActionResult> Remove([FromRoute] BlogPostRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }


        [HttpPut("{id}")]
        //[Authorize("admin.blogposts.edit")]

        public async Task<IActionResult> Edit(int id, [FromForm] BlogPostEditRequest request)
        {
            request.Id = id;
            var response = await mediator.Send(request);

            return Ok(response);
        }

        [HttpPost("{postId}/publish")]
        //[Authorize("admin.blogposts.publish")]

        public async Task<IActionResult> Publish([FromRoute]  BlogPostPublishRequest request)
        {
            await mediator.Send(request);
           return NoContent();
        }
    }
}
