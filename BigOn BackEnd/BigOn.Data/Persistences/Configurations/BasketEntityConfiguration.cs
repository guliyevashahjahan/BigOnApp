using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigOn.Data.Persistences.Configurations
{
    public class BasketEntityConfiguration : IEntityTypeConfiguration<Basket>
    {
        public void Configure(EntityTypeBuilder<Basket> builder)
        {
            builder.Property(x => x.UserId).HasColumnType("int").IsRequired();
            builder.Property(x => x.CatalogId).HasColumnType("int").IsRequired();
            builder.Property(x => x.Quantity).HasColumnType("decimal").HasPrecision(18,2).IsRequired();

            builder.HasKey(m=> new {m.UserId,m.CatalogId });
            builder.ToTable("Basket");

            builder.HasOne<ProductCatalog>()
                .WithMany()
                .HasForeignKey(m => m.CatalogId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<BigonUser>()
               .WithMany()
               .HasForeignKey(m => m.UserId)
               .HasPrincipalKey(m => m.Id)
               .OnDelete(DeleteBehavior.NoAction);

        }
    }
}
