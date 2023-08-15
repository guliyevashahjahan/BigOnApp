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
                cfg => { cfg.UseSqlServer(builder.Configuration.GetConnectionString("cstring"));

                });

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=home}/{action=index}/{id?}");
                });

            app.Run();
        }
    }
}