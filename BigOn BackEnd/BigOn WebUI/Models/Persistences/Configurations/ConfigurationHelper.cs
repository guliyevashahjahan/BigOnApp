using BigOn_WebUI.Models.Entities.Commons;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigOn_WebUI.Models.Persistences.Configurations
{
    public static class ConfigurationHelper
    {
        public static EntityTypeBuilder<T> ConfigureAsAuditable<T>(this EntityTypeBuilder<T> builder)
            where T : AuditableEntity
        {
            builder.Property(x => x.CreatedBy).HasColumnType("int").IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnType("datetime").IsRequired();
            builder.Property(x => x.LastModifiedBy).HasColumnType("int");
            builder.Property(x => x.LastModifiedAt).HasColumnType("datetime");
            builder.Property(x => x.DeletedBy).HasColumnType("int");
            builder.Property(x => x.DeletedAt).HasColumnType("datetime");


            return builder;
        }
    }
}
