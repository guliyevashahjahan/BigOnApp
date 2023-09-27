using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Data.Persistences.Seed
{
    public static class AutoMigration
    {
        public static IApplicationBuilder UpdateDatabase(this IApplicationBuilder builder)
        {
            using (var scope = builder.ApplicationServices.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<DataContext>();
                db.Database.Migrate();
            }
            return builder;
        }
    }
}
