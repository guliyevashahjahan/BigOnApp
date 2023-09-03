using BigOn.Infrastructure.Commons.Concrates;
using BigOn.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Data.Repositories
{
    internal class ContactPostRepository : GeneralRepository<ContactPost>
    {
        public ContactPostRepository(DbContext db) : base(db)
        {
        }
    }
}
