using BigOn_WebUI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BigOn_WebUI.Models.Persistences
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {

        }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Color> Colors { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
    }

}
