using BigOn.Infrastructure.Entities.Membership;
using BigOn.Infrastructure.Enums;
using BigOn.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BigOn.Infrastructure.Services.Concrates
{
    public interface IUserManager
    {
        Task<string> GenerateRefreshTokenAsync(BigonUser user, string accessToken, CancellationToken cancellationToken = default);
        Task<BigonUser> GetUserById(int userId, CancellationToken cancellationToken = default);
        Task<bool> ValidateRefreshTokenAsync(BigonUser user,string refreshToken, CancellationToken cancellationToken = default);

    }
    public class AppUserManager : UserManager<BigonUser>, IUserManager
    {
        private readonly ICryptoService cryptoService;
        private readonly IServiceProvider service;

        public AppUserManager(ICryptoService cryptoService, IUserStore<BigonUser> store,IOptions<IdentityOptions> options,IPasswordHasher<BigonUser> hasher,
            IEnumerable<IUserValidator<BigonUser>> userValidators, IEnumerable<IPasswordValidator<BigonUser>> passwordValidators, 
            ILookupNormalizer normalizer,IdentityErrorDescriber errors,IServiceProvider service,
            ILogger<UserManager<BigonUser>> logger)
            : base(store,options,hasher, userValidators, passwordValidators,normalizer,errors,service,logger)
        {
            this.cryptoService = cryptoService;
            this.service = service;
        }

        public async Task<string> GenerateRefreshTokenAsync(BigonUser user, string accessToken, CancellationToken cancellationToken = default)
        {
            string refreshToken = cryptoService.ToSha1($"{user.Id}{accessToken}{DateTime.Now:hh:mm:ss}" );
            using (var scope = service.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DbContext>();
                var tokenTable = db.Set<BigonUserToken>();

                var token = new BigonUserToken
                {
                    UserId = user.Id,
                    LoginProvider = "REFRESH_TOKEN",
                    Name = "REFRESH_TOKEN",
                    Value = cryptoService.ToMd5(refreshToken),
                    Type = TokenType.RefreshToken,
                    ExpireDate = DateTime.UtcNow.AddDays(15)
                };

                await tokenTable.AddAsync(token, cancellationToken);
                await db.SaveChangesAsync(cancellationToken);
            }
            return refreshToken;
        }

        public async Task<BigonUser> GetUserById(int userId, CancellationToken cancellationToken = default)
        {
            var user =await Users.FirstOrDefaultAsync(m => m.Id == userId, cancellationToken);
            return user;
        }

        public async Task<bool> ValidateRefreshTokenAsync(BigonUser user, string refreshToken, CancellationToken cancellationToken = default)
        {
            using (var scope = service.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DbContext>();
                var token =await db.Set<BigonUserToken>().Where(m=>m.UserId == user.Id
                     && m.Type == TokenType.RefreshToken&&m.ExpireDate!=null)
                    .OrderByDescending(m=>m.ExpireDate).FirstOrDefaultAsync(cancellationToken);
                if (token!=null && token.ExpireDate >= DateTime.UtcNow && token.Value.Equals(refreshToken)) 
                {
                    token.ExpireDate = DateTime.UtcNow;
                    await db.SaveChangesAsync(cancellationToken);
                    return true;
                }
            }
          
            return false;
        }
    }
}
