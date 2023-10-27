using BigOn.Infrastructure.Services.Abstracts;
using BigOn.Infrastructure.Services.Configurations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Services.Concrates
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions options;

        public JwtService(IOptions<JwtOptions> options)
        {
            this.options = options.Value;
        }
        public string GenerateAccessToken(List<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(options.Issuer,options.Audience,claims,
       expires: DateTime.UtcNow.AddMinutes(20),
       signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
