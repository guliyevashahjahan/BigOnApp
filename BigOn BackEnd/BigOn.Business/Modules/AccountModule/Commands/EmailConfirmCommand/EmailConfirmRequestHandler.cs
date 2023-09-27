using BigOn.Infrastructure.Entities.Membership;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BigOn.Business.Modules.AccountModule.Commands.EmailConfirmCommand
{
    internal class EmailConfirmRequestHandler : IRequestHandler<EmailConfirmRequest>
    {
        private readonly UserManager<BigonUser> userManager;

        public EmailConfirmRequestHandler(UserManager<BigonUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task Handle(EmailConfirmRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email);
            await userManager.ConfirmEmailAsync(user,request.Token);
        }
    }
}
