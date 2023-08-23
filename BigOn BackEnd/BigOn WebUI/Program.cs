using BigOn_WebUI.AppCode.Services;
using BigOn_WebUI.Models.Persistences;
using Microsoft.EntityFrameworkCore;


namespace BigOn_WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DataContext>(
                cfg => {
                    cfg.UseSqlServer(builder.Configuration.GetConnectionString("cString"),
                    opt =>
                    {
                        opt.MigrationsHistoryTable("Migrations");
                    });

                    });

            builder.Services.AddRouting(cfg =>
            {
                cfg.LowercaseUrls = true;
            });

            builder.Services.Configure<EmailOptions>(cfg => {

                builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg);
                });

            builder.Services.AddSingleton<IEmailService, EmailService>();

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