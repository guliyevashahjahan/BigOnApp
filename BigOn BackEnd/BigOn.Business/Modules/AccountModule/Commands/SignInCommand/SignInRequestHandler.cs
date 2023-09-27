using BigOn.Infrastructure.Entities.Membership;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

namespace BigOn.Business.Modules.AccountModule.Commands.SignInCommand
{
    internal class SignInRequestHandler : IRequestHandler<SignInRequest>
    {
        private readonly UserManager<BigonUser> userManager;
        private readonly SignInManager<BigonUser> signInManager;
        private readonly IActionContextAccessor actionContextAccessor;

        public SignInRequestHandler(UserManager<BigonUser> userManager, 
            SignInManager<BigonUser> signInManager,
            IActionContextAccessor actionContextAccessor)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.actionContextAccessor = actionContextAccessor;
        }
        public async Task Handle(SignInRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.UserName);
            if (user == null)
            {
                throw new Exception($"{request.UserName} not found.");
            }

            var result = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);
            if (!result.Succeeded)
            {
                throw new Exception("Username or password is incorrect!");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimIdentity);
            await actionContextAccessor.ActionContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principal, new AuthenticationProperties
            {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(10)
            });
        }
    }
}
