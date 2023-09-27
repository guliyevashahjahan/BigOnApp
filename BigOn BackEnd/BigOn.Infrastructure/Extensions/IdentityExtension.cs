using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Extensions
{
    public static partial class Extension
    {
        public static string GetClaimValue(this ClaimsPrincipal princpal,string type)
        {
            return princpal.Claims.FirstOrDefault(m => m.Type.Equals(type)).Value;
        }

        public static bool HasClaim(this ClaimsPrincipal princpal, string type)
        {
            return princpal.Claims.Any(m => m.Type.Equals(type)) || princpal.IsInRole("superadmin");
        }
    }
}
