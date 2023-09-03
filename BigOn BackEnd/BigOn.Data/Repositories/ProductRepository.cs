using BigOn.Infrastructure.Commons.Concrates;
using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Data.Repositories
{
    internal class ProductRepository : GeneralRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext db) : base(db)
        {
        }
    }
}
