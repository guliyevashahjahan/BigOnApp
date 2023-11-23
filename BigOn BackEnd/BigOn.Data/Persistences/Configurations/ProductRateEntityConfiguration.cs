using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Data.Persistences.Configurations
{
    class ProductRateEntityConfiguration : IEntityTypeConfiguration<ProductRate>
    {
        public void Configure(EntityTypeBuilder<ProductRate> builder)
        {
            builder.Property(x => x.UserId).HasColumnType("int").IsRequired();
            builder.Property(x => x.ProductId).HasColumnType("int").IsRequired();
            builder.Property(x => x.Rate).HasColumnType("int").IsRequired();

            builder.HasKey(m => new { m.UserId, m.ProductId });
            builder.ToTable("ProductRates");

            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(m => m.ProductId)
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
