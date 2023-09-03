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
    internal class ProductEntityConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);
            builder.Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(200).IsRequired();
            builder.Property(x => x.StockKeepingUnit).HasColumnType("nvarchar").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Rate).HasColumnType("decimal").HasPrecision(18, 2).IsRequired();
            builder.Property(x => x.Price).HasColumnType("decimal").HasPrecision(18, 2).IsRequired();
            builder.Property(x => x.ShortDescription).HasColumnType("nvarchar").HasMaxLength(300).IsRequired();
            builder.Property(x => x.Description).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(x => x.BrandId).HasColumnType("int").IsRequired();
            builder.Property(x => x.CategoryId).HasColumnType("int").IsRequired();






            builder.ConfigureAsAuditable();

            builder.HasKey(x => x.Id);
            builder.ToTable("Products");
        }
    }
}
