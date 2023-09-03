using BigOn.Infrastructure.Commons.Abstracts;
using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Services.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BigOn.Data.Persistences
{
    public class DataContext : DbContext
    {
        private readonly IDateTimeService dateTimeService;
        private readonly IIdentityService identityService;

        public DataContext(DbContextOptions options, IDateTimeService dateTimeService, IIdentityService identityService)
            : base(options)
        {
            this.dateTimeService = dateTimeService;
            this.identityService = identityService;
        }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Brand> Brands { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
        }
        public override int SaveChanges()
        {
            var changes = this.ChangeTracker.Entries<IAuditableEntity>();
            if (changes != null)
            {
                foreach (var entry in changes.Where(m => m.State == EntityState.Added || m.State == EntityState.Modified || m.State == EntityState.Deleted))

                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entry.Entity.CreatedAt = dateTimeService.ExecutingTime;
                            entry.Entity.CreatedBy = identityService.GetPrincipalId;
                            break;
                        case EntityState.Modified:
                            entry.Entity.LastModifiedAt = dateTimeService.ExecutingTime;
                            entry.Entity.LastModifiedBy = identityService.GetPrincipalId;
                            entry.Property(m => m.CreatedBy).IsModified = false;
                            entry.Property(m=>m.CreatedAt).IsModified = false;
                            break;
                        case EntityState.Deleted:
                            entry.State = EntityState.Modified;
                            entry.Entity.DeletedAt = dateTimeService.ExecutingTime;
                            entry.Entity.DeletedBy = identityService.GetPrincipalId;
                            entry.Property(m => m.CreatedBy).IsModified = false;
                            entry.Property(m => m.CreatedAt).IsModified = false;
                            entry.Property(m => m.LastModifiedAt).IsModified = false;
                            entry.Property(m => m.LastModifiedBy).IsModified = false;
                            break;
                        default:
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }
    }

}
