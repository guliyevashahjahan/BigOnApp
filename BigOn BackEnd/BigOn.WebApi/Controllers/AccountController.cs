using BigOn.Business.Modules.AccountModule.Commands.SignInCommand;
using BigOn.Infrastructure.Services.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace BigOn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IJwtService jwtService;

        public AccountController(IMediator mediator,IJwtService jwtService)
        {
            this.mediator = mediator;
            this.jwtService = jwtService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn (SignInRequest request)
        {
            var claims =await mediator.Send(request);
            var token = jwtService.GenerateAccessToken(claims);

            return Ok(new
            {
                access_token = token
            });
        }
    }
}
