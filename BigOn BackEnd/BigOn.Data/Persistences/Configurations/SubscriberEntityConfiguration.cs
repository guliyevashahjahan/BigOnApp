using BigOn.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigOn.Data.Persistences.Configurations
{
    public class SubscriberEntityConfiguration : IEntityTypeConfiguration<Subscriber>
    {
        public void Configure(EntityTypeBuilder<Subscriber> builder)
        {
            builder.Property(x => x.Email).HasColumnType("varchar").HasMaxLength(100);
            builder.Property(x => x.Approved).HasColumnType("bit").IsRequired();
            builder.Property(x => x.ApprovedAt).HasColumnType("datetime");
            builder.Property(x => x.CreatedAt).HasColumnType("datetime").IsRequired();

            builder.HasKey(x => x.Email);
            builder.ToTable("Subscribers");
        }
    }
}
