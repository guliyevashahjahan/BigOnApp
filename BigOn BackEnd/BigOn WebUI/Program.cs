using BigOn.Business;
using BigOn.Data;
using BigOn.Data.Persistences;
using BigOn.Infrastructure.Services.Abstracts;
using BigOn.Infrastructure.Services.Concrates;
using BigOn.Infrastructure.Services.Configurations;
using Microsoft.EntityFrameworkCore;


namespace BigOn_WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            DataServiceInjection.InstallDataServices(builder.Services,builder.Configuration);
         ;

            builder.Services.AddRouting(cfg =>
            {
                cfg.LowercaseUrls = true;
            });

            builder.Services.Configure<EmailOptions>(cfg => {

                builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg);
                });

            builder.Services.AddSingleton<IEmailService, EmailService>();
            builder.Services.AddSingleton<IDateTimeService, DateTimeService>();
            builder.Services.AddScoped<IIdentityService, IdentityService>();
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(IBusinessReferance).Assembly);
            });


            var app = builder.Build(); 

            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "Areas",
                  pattern: "{area:exists}/{controller=dashboard}/{action=index}/{id?}");
                endpoints.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");
                });

            app.Run();
        }
    }
}