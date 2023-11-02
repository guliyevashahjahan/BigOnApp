using BigOn.Business.Modules.AccountModule.Commands.RefreshTokenCommand;
using BigOn.Business.Modules.AccountModule.Commands.SignInCommand;
using BigOn.Infrastructure.Entities.Membership;
using BigOn.Infrastructure.Services.Abstracts;
using BigOn.Infrastructure.Services.Concrates;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
        private readonly IUserManager userManager;

        public AccountController(IMediator mediator,IJwtService jwtService,IUserManager userManager)
        {
            this.mediator = mediator;
            this.jwtService = jwtService;
            this.userManager = userManager;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> SignIn (SignInRequest request)
        {
            var user =await mediator.Send(request);
            var token = jwtService.GenerateAccessToken(user);
            var refreshToken = await userManager.GenerateRefreshTokenAsync(user,token);

            return Ok(new
            {
                access_token = token,
                refresh_token = refreshToken
            });
        }

        [HttpPost("refresh-token")]

        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken([FromHeader] RefreshTokenRequest request)
        {
            var response = await mediator.Send(request);

            return Ok(new
            {
                access_token = response.AccessToken,
                refresh_token = response.RefreshToken
            });
        }
    }
}
