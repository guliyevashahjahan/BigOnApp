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
    internal class BigonUserClaimEntityConfiguration : IEntityTypeConfiguration<BigonUserClaim>
    {
        public void Configure(EntityTypeBuilder<BigonUserClaim> builder)
        {
            builder.ToTable("UserClaims", "Membership");
        }
    }
}
