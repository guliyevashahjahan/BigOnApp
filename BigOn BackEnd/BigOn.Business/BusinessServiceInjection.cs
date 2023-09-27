using BigOn.Data.Persistences.Seed;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business
{
    public static class BusinessServiceInjection
    {
        public static IApplicationBuilder BuildServices (this IApplicationBuilder app)
        {
            app.UpdateDatabase();
            return app;
        }
    }
}
