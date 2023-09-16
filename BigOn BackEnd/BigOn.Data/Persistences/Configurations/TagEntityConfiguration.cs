using BigOn.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigOn.Data.Persistences.Configurations
{
    public class TagEntityConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);
            builder.Property(x => x.Text).HasColumnType("nvarchar").HasMaxLength(200).IsRequired();
            builder.ConfigureAsAuditable();

            builder.HasKey(x => x.Id);
            builder.ToTable("Tags");
        }
    }
}
