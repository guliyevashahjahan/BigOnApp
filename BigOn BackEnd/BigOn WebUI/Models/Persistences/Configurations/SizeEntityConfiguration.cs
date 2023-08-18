using BigOn_WebUI.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace BigOn_WebUI.Models.Persistences.Configurations
{
    public class SizeEntityConfiguration : IEntityTypeConfiguration<Size>
    {
        public void Configure(EntityTypeBuilder<Size> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);
            builder.Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(200).IsRequired();
            builder.Property(x => x.ShortName).HasColumnType("varchar").HasMaxLength(5).IsRequired();
            builder.ConfigureAsAuditable();

            builder.HasKey(x => x.Id);
            builder.ToTable("Sizes");
        }
    }
}
