using BigOn.Business.Modules.AccountModule.Commands.EmailConfirmCommand;
using BigOn.Business.Modules.AccountModule.Commands.RegisterCommand;
using BigOn.Business.Modules.AccountModule.Commands.SignInCommand;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

namespace BigOn_WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IMediator mediator;

        public AccountController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        [AllowAnonymous]
        [Route("/signin.html")]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/signin.html")]
        public async Task<IActionResult> SignIn(SignInRequest request)
        {
           var user = await mediator.Send(request);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));
            await Request.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                principal, new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(10)
                });
            var callback = Request.Query["ReturnUrl"];
            if (!string.IsNullOrWhiteSpace(callback))
            {
                return RedirectToAction(callback);
            }
            return RedirectToAction("index","home");
        }

        [AllowAnonymous]
        [Route("/register.html")]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("/register.html")]
        public async Task<IActionResult>  Register(RegisterRequest request)
        {
            await mediator.Send(request);
            return RedirectToAction("SignIn");
        }

        [AllowAnonymous]
        [Route("/email-confirm.html")]
        public async Task<IActionResult> EmailConfirm(EmailConfirmRequest request)
        {
            await mediator.Send(request);
            return RedirectToAction("SignIn");
        }

        [Route("/accessdenied.html")]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [Route("/logout.html")]
        public async Task<IActionResult> LogOut()
        {
            await Request.HttpContext.SignOutAsync();
            return RedirectToAction("index","home");
        }
    }
}
 