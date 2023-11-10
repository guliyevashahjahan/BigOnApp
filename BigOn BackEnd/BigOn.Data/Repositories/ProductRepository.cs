using BigOn.Infrastructure.Commons.Concrates;
using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Data.Repositories
{
    internal class ProductRepository : GeneralRepository<Product>, IProductRepository
    {
        public ProductRepository(DbContext db) : base(db)
        {
        }

        public async Task<IEnumerable<Brand>> GetBrandsForFilter()
        {
            var brandIds = await this.GetAll(m => m.DeletedBy == null).Select(m => m.BrandId).Distinct().ToArrayAsync();
          
            return await db.Set<Brand>().Where(m => brandIds.Contains(m.Id)).ToListAsync();
        }

        public IQueryable<ProductCatalog> GetCatalog()
        {
            return db.Set<ProductCatalog>().AsQueryable();
        }

        public async Task<IEnumerable<Color>> GetColorsForFilter()
        {
            var colorIds = await db.Set<ProductCatalog>().Select(m => m.ColorId).Distinct().ToArrayAsync();
            return await db.Set<Color>().Where(m => colorIds.Contains(m.Id)).ToListAsync();
        }

        public IQueryable<ProductImage> GetImages(Expression<Func<ProductImage, bool>> expression = null)
        {
            var query = db.Set<ProductImage>().AsQueryable();
            if (expression is not null)
            {
                query = query.Where(expression);
            }
            return query;
        }

        public async Task<IEnumerable<Material>> GetMaterialsForFilter()
        {
            var materialIds = await db.Set<ProductCatalog>().Select(m => m.MaterialId).Distinct().ToArrayAsync();
            return await db.Set<Material>().Where(m => materialIds.Contains(m.Id)).ToListAsync();
        }

        public async Task<IEnumerable<Size>> GetSizesForFilter()
        {
            var sizeIds = await db.Set<ProductCatalog>().Select(m=>m.SizeId).Distinct().ToArrayAsync();
            var sizes = await db.Set<Size>().Where(m => sizeIds.Contains(m.Id)).ToListAsync();
            return sizes;
        }
    }
}
