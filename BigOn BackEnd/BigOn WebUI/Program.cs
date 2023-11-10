using BigOn.Business;
using BigOn.Data;
using BigOn.Data.Persistences;
using BigOn.Infrastructure.Services.Abstracts;
using BigOn.Infrastructure.Services.Concrates;
using BigOn.Infrastructure.Services.Configurations;
using BigOn_WebUI.Pipeline;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace BigOn_WebUI
{
    public class Program
    {
        internal static string[] policies = null;
        public static void Main(string[] args)
        {
            ReadAllPolicies();
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews(cfg =>
            {
               AuthorizationPolicy policy =new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
             
                cfg.Filters.Add(new AuthorizeFilter(policy));
               
            });
            DataServiceInjection.InstallDataServices(builder.Services,builder.Configuration);
         ;

            builder.Services.AddRouting(cfg =>
            {
                cfg.LowercaseUrls = true;
            });

            builder.Services.Configure<EmailOptions>(cfg => builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));
             
            builder.Services.Configure<CryptoOptions>(cfg => builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));

            builder.Services.Configure<JwtOptions>(cfg => builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));


            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddSingleton<IJwtService, JwtService>();

            builder.Services.AddSingleton<IDateTimeService, DateTimeService>();
            builder.Services.AddSingleton<ICryptoService, CryptoService>();

            builder.Services.AddSingleton<IFileService, FileService>();
            builder.Services.AddScoped<IIdentityService, IdentityService>();
            builder.Services.AddScoped<IClaimsTransformation, AppClaimProvider>();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(IBusinessReferance).Assembly);
            });
            builder.Services.AddAuthentication(cfg =>
            {
                cfg.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                cfg.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                cfg.DefaultForbidScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                cfg.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                cfg.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                cfg.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            })
                .AddCookie(cfg =>
                {
                    cfg.LoginPath = "/signin.html";
                    cfg.AccessDeniedPath = "/accessdenied.html";
                    cfg.Cookie.Name = "bigon";
                    cfg.ExpireTimeSpan = TimeSpan.FromMinutes(10);
                    cfg.Cookie.HttpOnly = true;
                });
            builder.Services.AddAuthorization(cfg =>
            {
                foreach (var item in policies)
                {
                    cfg.AddPolicy(item, p => p.RequireAssertion(handler =>
                    {
                        return handler.User.IsInRole("superadmin") || handler.User.HasClaim(item, "1");
                    }));
                }
            });
            builder.Services.Configure<IdentityOptions>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
                //cfg.User.AllowedUserNameCharacters = "";
                cfg.Password.RequireUppercase = false;
                cfg.Password.RequireLowercase = false;
                cfg.Password.RequireDigit = false;
                cfg.Password.RequiredUniqueChars = 1;
                cfg.Password.RequiredLength = 3;
                cfg.Password.RequireNonAlphanumeric = false;

                cfg.Lockout.AllowedForNewUsers = true;
                cfg.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                cfg.Lockout.MaxFailedAccessAttempts = 3; 
            });


            var app = builder.Build(); 

            app.BuildServices();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "Areas",
                  pattern: "{area:exists}/{controller=dashboard}/{action=index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");
               
            });
          
            app.Run();
        }
        private static void ReadAllPolicies() 
        {
            var types = typeof(Program).Assembly.GetTypes();
            policies = types
                 .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && t.IsDefined(typeof(AuthorizeAttribute), true))
                .SelectMany(t => t.GetCustomAttributes<AuthorizeAttribute>())
                .Union(
                types
                .Where(t => typeof(ControllerBase).IsAssignableFrom(t))
                .SelectMany(type => type.GetMethods())
                .Where(method => method.IsPublic
                 && !method.IsDefined(typeof(NonActionAttribute), true)
                 && method.IsDefined(typeof(AuthorizeAttribute), true))
                 .SelectMany(t => t.GetCustomAttributes<AuthorizeAttribute>())
                )
                .Where(a => !string.IsNullOrWhiteSpace(a.Policy))
                .SelectMany(a => a.Policy.Split(new[] { "," }, System.StringSplitOptions.RemoveEmptyEntries))
                .Distinct()
                .ToArray();
        }
    }
}