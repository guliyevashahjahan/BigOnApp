using BigOn.Infrastructure.Entities.Membership;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Data.Persistences.Configurations.Membership
{
    internal class BigonUserTokenEntityConfiguration : IEntityTypeConfiguration<BigonUserToken>
    {
        public void Configure(EntityTypeBuilder<BigonUserToken> builder)
        {
            builder.Property(m => m.Type).HasColumnType("tinyint").IsRequired();
            builder.Property(m => m.ExpireDate).HasColumnType("datetime");
            builder.Property(m => m.Value).HasColumnType("nvarchar").HasMaxLength(450).IsRequired();


            builder.HasKey(m => new {m.UserId,m.LoginProvider,m.Type,m.Value });
            builder.ToTable("UserTokens", "Membership");
        }
    }
}
