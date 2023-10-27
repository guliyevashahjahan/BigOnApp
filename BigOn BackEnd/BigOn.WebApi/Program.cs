using BigOn.Business;
using BigOn.Data;
using BigOn.Infrastructure.Services.Abstracts;
using BigOn.Infrastructure.Services.Concrates;
using BigOn.Infrastructure.Services.Configurations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.FileProviders;
using FluentValidation;
using FluentValidation.AspNetCore;
using BigOn.Infrastructure.Middlewares;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace BigOn.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(cfg =>
            {
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();

                cfg.Filters.Add(new AuthorizeFilter(policy));
            });
            builder.Services.AddAutoMapper(typeof(Program).Assembly);
            builder.Services.AddCors(cfg =>
            {
                cfg.AddPolicy("allow-js", p =>
                {
                    p.AllowAnyHeader();
                    p.AllowAnyMethod();
                    p.WithOrigins("http://127.0.0.1:5500");
                });
            });


            DataServiceInjection.InstallDataServices(builder.Services, builder.Configuration);


            builder.Services.AddRouting(cfg =>
            {
                cfg.LowercaseUrls = true;
            });

            builder.Services.Configure<EmailOptions>(cfg => builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));

            builder.Services.Configure<CryptoOptions>(cfg =>builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));

            builder.Services.Configure<JwtOptions>(cfg => builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));

            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddSingleton<IDateTimeService, DateTimeService>();
            builder.Services.AddSingleton<ICryptoService, CryptoService>();

            builder.Services.AddSingleton<IFileService, FileService>();
            builder.Services.AddScoped<IIdentityService, FakeIdentityService>();
            builder.Services.AddScoped<IJwtService, JwtService>();


            builder.Services.AddScoped<IValidatorInterceptor, ValidatorInterceptor>();

            //builder.Services.AddScoped<IClaimsTransformation, AppClaimProvider>();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(IBusinessReferance).Assembly);
            });

            builder.Services.AddValidatorsFromAssemblyContaining<IBusinessReferance>(includeInternalTypes:true);
            builder.Services.AddFluentValidationAutoValidation();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
   .AddJwtBearer(options =>
   {
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = builder.Configuration["JwtOptions:issuer"],
           ValidAudience = builder.Configuration["JwtOptions:audience"],
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:key"])),
           ClockSkew = TimeSpan.Zero
       };
   });
            builder.Services.AddAuthorization();

            var app = builder.Build();
            app.UseRequestLocalization(cfg =>
            {
                cfg.AddSupportedCultures("en", "az", "ru");
                cfg.AddSupportedUICultures("en", "az", "ru");
                cfg.RequestCultureProviders.Clear();
                cfg.RequestCultureProviders.Add(new AppCultureProvider());

            });
            app.UseGlobalErrorHandler();
            app.UseCors("allow-js");
            app.BuildServices();
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath="/files",
                FileProvider = new PhysicalFileProvider(builder.Configuration.GetValue<string>("physicalPath"))
            });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}