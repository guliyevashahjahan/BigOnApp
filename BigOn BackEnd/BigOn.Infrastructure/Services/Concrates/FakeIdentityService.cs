using BigOn.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;

namespace BigOn.Infrastructure.Services.Concrates
{
    public class FakeIdentityService : IIdentityService
    {
        private readonly IActionContextAccessor ctx;

        public FakeIdentityService(IActionContextAccessor ctx)
        {
            this.ctx = ctx;
        }
        public int? GetPrincipalId ()
        {
            return 4;
        }
    }
}
