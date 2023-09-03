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
    internal class ContactPostEntityConfiguration : IEntityTypeConfiguration<ContactPost>
    {
        public void Configure(EntityTypeBuilder<ContactPost> builder)
        {
            builder.Property(x => x.Id).UseIdentityColumn(1, 1);
            builder.Property(x => x.Name).HasColumnType("nvarchar").HasMaxLength(200).IsRequired();
            builder.Property(x => x.Email).HasColumnType("nvarchar").HasMaxLength(200).IsRequired();
            builder.Property(x => x.Subject).HasColumnType("nvarchar").HasMaxLength(200).IsRequired();
            builder.Property(x => x.Message).HasColumnType("nvarchar").HasMaxLength(500).IsRequired();
            builder.Property(x => x.Answer).HasColumnType("nvarchar").HasMaxLength(200).IsRequired();
            builder.Property(x => x.AnsweredBy).HasColumnType("int");
            builder.Property(x => x.AnsweredAt).HasColumnType("datetime");


            builder.ConfigureAsAuditable();

            builder.HasKey(x => x.Id);
           
            builder.ToTable("ContactPosts");
        }
    }
}
