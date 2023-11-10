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
    class ProductStockEntityConfiguration : IEntityTypeConfiguration<ProductStock>
    {
        public void Configure(EntityTypeBuilder<ProductStock> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);
            builder.Property(x => x.CatalogId).HasColumnType("int").IsRequired();
            builder.Property(x => x.DocumentNo).HasColumnType("varchar").IsRequired().HasMaxLength(100);
            builder.Property(x => x.Quantity).HasColumnType("decimal").HasPrecision(18, 2);

            builder.ConfigureAsAuditable();

            builder.HasKey(x => x.Id);
            builder.ToTable("ProductStock");
            
                
        }
    }
}
