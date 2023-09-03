using BigOn.Infrastructure.Services.Abstracts;

namespace BigOn.Infrastructure.Services.Concrates
{
    public class IdentityService : IIdentityService
    {
        public int GetPrincipalId
        {
            get
            {
                return 1;
            }
        }
    }
}
