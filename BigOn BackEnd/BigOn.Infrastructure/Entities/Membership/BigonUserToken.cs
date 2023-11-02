using BigOn.Infrastructure.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Entities.Membership
{
    public class BigonUserToken : IdentityUserToken<int>
    {
        public TokenType Type { get; set; }
        public DateTime? ExpireDate { get; set; }

    }
}
