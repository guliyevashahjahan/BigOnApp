using BigOn.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BigOn.Data.Persistences.Configurations
{
    public class BlogPostTagEntityConfiguration : IEntityTypeConfiguration<BlogPostTag>
    {
        public void Configure(EntityTypeBuilder<BlogPostTag> builder)
        {
            builder.HasOne<BlogPost>()
                .WithMany()
                .HasForeignKey(m => m.BlogPostId)
                .HasPrincipalKey(m => m.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<Tag>()
               .WithMany()
               .HasForeignKey(m => m.TagId)
               .HasPrincipalKey(m => m.Id)
               .OnDelete(DeleteBehavior.NoAction);

            builder.HasKey(x => new { x.TagId, x.BlogPostId});
            builder.ToTable("BlogPostTags");
        }
    }
}
