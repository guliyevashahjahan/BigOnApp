using BigOn.Business.Modules.AccountModule.Commands.EmailConfirmCommand;
using BigOn.Business.Modules.AccountModule.Commands.RegisterCommand;
using BigOn.Business.Modules.AccountModule.Commands.SignInCommand;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
           await mediator.Send(request);
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
 