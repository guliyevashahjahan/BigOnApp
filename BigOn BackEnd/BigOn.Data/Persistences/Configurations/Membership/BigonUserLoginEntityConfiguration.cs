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
    internal class BigonUserLoginEntityConfiguration : IEntityTypeConfiguration<BigonUserLogin>
    {
        public void Configure(EntityTypeBuilder<BigonUserLogin> builder)
        {
            builder.ToTable("UserLogins", "Membership");
        }
    }
}
