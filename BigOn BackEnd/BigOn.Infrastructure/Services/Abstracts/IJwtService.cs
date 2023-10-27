﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Services.Abstracts
{
    public interface IJwtService
    {
        string GenerateAccessToken(List<Claim> claims);
    }
}
