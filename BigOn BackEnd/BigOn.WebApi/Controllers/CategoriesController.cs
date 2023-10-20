﻿using BigOn.Business.Modules.CategoryModule.Commands.CategoryAddCommand;
using BigOn.Business.Modules.CategoryModule.Commands.CategoryEditCommand;
using BigOn.Business.Modules.CategoryModule.Commands.CategoryRemoveCommand;
using BigOn.Business.Modules.CategoryModule.Queries.CategoryGetAllQuery;
using BigOn.Business.Modules.CategoryModule.Queries.CategoryGetByIdQuery;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigOn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator mediator;

        public CategoriesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute]CategoryGetAllRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute]CategoryGetByIdRequest request)
        {
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody]CategoryAddRequest request)
        {
            var response = await mediator.Send(request);
            return CreatedAtAction(nameof(GetById), routeValues: new { id = response.Id},response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id,[FromBody]CategoryEditRequest request)
        {
            request.Id= id;
            var response = await mediator.Send(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove([FromRoute] CategoryRemoveRequest request)
        {
            await mediator.Send(request);
            return NoContent();
        }
    }
}
