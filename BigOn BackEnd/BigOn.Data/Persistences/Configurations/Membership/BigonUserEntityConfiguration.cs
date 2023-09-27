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
    internal class BigonUserEntityConfiguration : IEntityTypeConfiguration<BigonUser>
    {
        public void Configure(EntityTypeBuilder<BigonUser> builder)
        {
            builder.ToTable("Users", "Membership");
        }
    }
}
