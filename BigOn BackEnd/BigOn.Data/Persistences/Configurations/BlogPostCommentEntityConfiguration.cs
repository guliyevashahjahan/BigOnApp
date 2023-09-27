using Microsoft.EntityFrameworkCore;
using BigOn.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Data.Persistences.Configurations
{
    public class BlogPostCommentEntityConfiguration : IEntityTypeConfiguration<BlogPostComment>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<BlogPostComment> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1,1);
            builder.Property(x => x.Comment).HasColumnType("nvarchar(max)").IsRequired();
            builder.Property(x => x.ParentId).HasColumnType("int");

            builder.ConfigureAsAuditable();

            builder.HasKey(x => x.Id);
            builder.HasOne<BlogPost>()
                .WithMany()
                .HasForeignKey(x => x.PostId)
                .HasPrincipalKey(x => x.Id)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne<BlogPostComment>()
              .WithMany()
              .HasForeignKey(x => x.ParentId)
              .HasPrincipalKey(x => x.Id)
              .OnDelete(DeleteBehavior.NoAction);
                
            builder.ToTable("BlogPostComments");
        }
    }
}
