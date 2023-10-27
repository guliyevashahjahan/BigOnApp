using AngleSharp.Io;
using BigOn.Business.Modules.CategoryModule.Commands.CategoryAddCommand;
using BigOn.Business.Modules.CategoryModule.Commands.CategoryEditCommand;
using BigOn.Business.Modules.CategoryModule.Commands.CategoryRemoveCommand;
using BigOn.Business.Modules.CategoryModule.Queries.CategoryGetAllQuery;
using BigOn.Business.Modules.CategoryModule.Queries.CategoryGetByIdQuery;
using BigOn.Business.Modules.ColorsModule.Commands.ColorAddCommand;
using BigOn.Business.Modules.ColorsModule.Commands.ColorEditCommand;
using BigOn.Business.Modules.ColorsModule.Commands.ColorRemoveCommand;
using BigOn.Business.Modules.ColorsModule.Queries.ColorGetAllQuery;
using BigOn.Business.Modules.ColorsModule.Queries.ColorGetByIdQuery;
using BigOn.Infrastructure.Commons.Concrates;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BigOn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IValidator<ColorAddRequest> validator;

        public ColorsController(IMediator mediator, IValidator<ColorAddRequest> validator)
        {
            this.mediator = mediator;
            this.validator = validator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute] ColorGetAllRequest request)
        {
            var response = await mediator.Send(request);
            var data = ApiResponse.Success(response, "OK");
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] ColorGetByIdRequest request)
        {
            var response = await mediator.Send(request);
            var data = ApiResponse.Success(response, "OK");
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ColorAddRequest request)
        {
            var state = await validator.ValidateAsync(request);
            if (!state.IsValid)
            {
                var errors = state.Errors.GroupBy(m => m.PropertyName).ToDictionary(k => k.Key, v => v.Select(m => m.ErrorMessage));
                return BadRequest(errors);
            }
            var response = await mediator.Send(request);
            var data = ApiResponse.Success(response, "CREATED", HttpStatusCode.Created);
            return CreatedAtAction(nameof(GetById), routeValues: new { id = response.Id }, data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] ColorEditRequest request)
        {
            request.Id = id;
            var response = await mediator.Send(request);

            var data = ApiResponse.Success(response, "EDITED");
            return Ok(data);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove([FromRoute] ColorRemoveRequest request)
        {
            await mediator.Send(request);

            var data = ApiResponse.Success("REMOVED", HttpStatusCode.NoContent);
            return Ok(data);
        }
    }
}
