using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigOn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SizesController : ControllerBase
    {
        private readonly IMediator mediator;

        public SizesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
       // [Authorize("admin.sizes.index")]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}
