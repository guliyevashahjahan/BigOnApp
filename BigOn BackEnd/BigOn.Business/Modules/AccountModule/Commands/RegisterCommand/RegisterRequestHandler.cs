using BigOn.Infrastructure.Entities.Membership;
using BigOn.Infrastructure.Services.Abstracts;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text;
using System.Web;

namespace BigOn.Business.Modules.AccountModule.Commands.RegisterCommand
{
    internal class RegisterRequestHandler : IRequestHandler<RegisterRequest>
    {
        private readonly UserManager<BigonUser> userManager;
        private readonly RoleManager<BigonRole> roleManager;
        private readonly IEmailService emailService;
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly ICryptoService cryptoService;

        public RegisterRequestHandler(UserManager<BigonUser> userManager,
            RoleManager<BigonRole> roleManager,
            IEmailService emailService,
            IActionContextAccessor actionContextAccessor,
            ICryptoService cryptoService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.emailService = emailService;
            this.actionContextAccessor = actionContextAccessor;
            this.cryptoService = cryptoService;
        }
        public async Task Handle(RegisterRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);

            if (user != null)
            {
                throw new Exception($"{request.Email} is already taken.");
            }

            user = new BigonUser
            {
                Name = request.Name,
                Surname = request.Surname,
                Email = request.Email,
                EmailConfirmed = false
            };

            int? tryCount = null;
            while (true)
            {
                user.UserName = ($"{request.Name}.{request.Surname}{(tryCount.HasValue ? tryCount : "") }").ToLower();
                if (await userManager.FindByNameAsync(request.Name)==null)
                {
                    break;
                }
                tryCount = (tryCount ?? 0) + 1;
            }
            
            var result = await userManager.CreateAsync(user,request.Password);
            if (!result.Succeeded)
            {
                var sb = new StringBuilder();
                foreach (var item in result.Errors)
                {
                    sb.AppendLine($"{item.Code}: {item.Description}");
                }
                throw new Exception(sb.ToString());
            }
            string token = await userManager.GenerateEmailConfirmationTokenAsync(user);
            token = HttpUtility.UrlEncode(token);

            token = cryptoService.Encrypt($"t={token}&email={user.Email}", true);


            string url = $"{actionContextAccessor.ActionContext.HttpContext.Request.Scheme}://{actionContextAccessor.ActionContext.HttpContext.Request.Host}/email-confirm.html?token={token}";
            string message = $"Your registration is confirmed! Click the <a href='{url}'>link</a> to activate your account.";
            await  emailService.SendMailAsync(request.Email, "Bigon Registration", message);
        }
    }
}
