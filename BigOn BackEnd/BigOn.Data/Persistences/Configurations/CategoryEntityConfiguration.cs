using BigOn.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigOn.Data.Persistences.Configurations
{
    public class CategoryEntityConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);
            builder.Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(200).IsRequired();
            builder.ConfigureAsAuditable();
            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(x => x.ParentId)
                .HasPrincipalKey(x => x.Id);

            builder.HasKey(x => x.Id);
            builder.ToTable("Categories");
        }
    }
}
