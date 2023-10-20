using BigOn.Business;
using BigOn.Data;
using BigOn.Infrastructure.Services.Abstracts;
using BigOn.Infrastructure.Services.Concrates;
using BigOn.Infrastructure.Services.Configurations;
using Microsoft.AspNetCore.Authentication;

namespace BigOn.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            DataServiceInjection.InstallDataServices(builder.Services, builder.Configuration);


            builder.Services.AddRouting(cfg =>
            {
                cfg.LowercaseUrls = true;
            });
            builder.Services.Configure<EmailOptions>(cfg => {

                builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg);

            });
            builder.Services.Configure<CryptoOptions>(cfg => {

                builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg);

            });
            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddSingleton<IDateTimeService, DateTimeService>();
            builder.Services.AddSingleton<ICryptoService, CryptoService>();

            builder.Services.AddSingleton<IFileService, FileService>();
            builder.Services.AddScoped<IIdentityService, IdentityService>();
            //builder.Services.AddScoped<IClaimsTransformation, AppClaimProvider>();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(IBusinessReferance).Assembly);
            });




            var app = builder.Build();

            app.BuildServices();
            app.UseStaticFiles();
            app.UseRouting();

            app.MapControllers();

            app.Run();
        }
    }
}