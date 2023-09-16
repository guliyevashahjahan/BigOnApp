using Microsoft.EntityFrameworkCore;
using BigOn.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Data.Persistences.Configurations
{
    public class BlogPostEntityConfiguration : IEntityTypeConfiguration<BlogPost>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BlogPost> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1,1);
            builder.Property(x => x.Title).HasColumnType("nvarchar").HasMaxLength(200).IsRequired();
            builder.Property(x => x.Body).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(x => x.ImagePath).HasColumnType("nvarchar").HasMaxLength(50).IsRequired();
            builder.Property(x => x.Slug).HasColumnType("nvarchar").HasMaxLength(200).IsRequired();
            builder.Property(x => x.CategoryId).HasColumnType("int").IsRequired();
            builder.Property(x => x.PublishedAt).HasColumnType("datetime");



            builder.ConfigureAsAuditable();

            builder.HasKey(x => x.Id);
            builder.HasOne<Category>()
                .WithMany()
                .HasForeignKey(x => x.CategoryId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(x => x.Slug).IsUnique();
                
            builder.ToTable("BlogPosts");
        }
    }
}
