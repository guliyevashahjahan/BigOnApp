using BigOn.Infrastructure.Entities.Membership;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Data.Persistences.Configurations.Membership
{
    internal class BigonRoleEntityConfiguration : IEntityTypeConfiguration<BigonRole>
    {
        public void Configure(EntityTypeBuilder<BigonRole> builder)
        {
            builder.ToTable("Roles", "Membership");
        }
    }
}
