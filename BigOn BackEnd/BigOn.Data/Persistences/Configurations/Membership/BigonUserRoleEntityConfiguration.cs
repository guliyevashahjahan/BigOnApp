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
    internal class BigonUserRoleEntityConfiguration : IEntityTypeConfiguration<BigonUserRole>
    {
        public void Configure(EntityTypeBuilder<BigonUserRole> builder)
        {
            builder.ToTable("UserRoles", "Membership");
        }
    }
}
