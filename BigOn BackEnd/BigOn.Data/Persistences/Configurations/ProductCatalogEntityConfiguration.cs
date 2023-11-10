using BigOn.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Data.Persistences.Configurations
{
    class ProductCatalogEntityConfiguration : IEntityTypeConfiguration<ProductCatalog>
    {
        public void Configure(EntityTypeBuilder<ProductCatalog> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);
            builder.Property(x => x.ProductId).HasColumnType("int").IsRequired();
            builder.Property(x => x.SizeId).HasColumnType("int");
            builder.Property(x => x.MaterialId).HasColumnType("int");
            builder.Property(x => x.ColorId).HasColumnType("int");
            builder.Property(x => x.Price).HasColumnType("decimal").HasPrecision(18,2);

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.SizeId, x.ProductId, x.MaterialId, x.ColorId }).IsUnique();
            builder.ToTable("ProductCatalog");
            builder.HasOne<Product>()
                .WithMany()
                .HasForeignKey(x => x.ProductId)
                .HasPrincipalKey(x=>x.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Size>()
                .WithMany()
                .HasForeignKey(x => x.SizeId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Color>()
                .WithMany()
                .HasForeignKey(x => x.ColorId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne<Material>()
                .WithMany()
                .HasForeignKey(x => x.MaterialId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
