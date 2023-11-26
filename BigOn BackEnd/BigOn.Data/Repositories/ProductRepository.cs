using BigOn.Infrastructure.Commons.Concrates;
using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Exceptions;
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

        public async Task<Basket> AddToBasketAsync(Basket basket, CancellationToken cancellationToken)
        {
            await db.Set<Basket>().AddAsync(basket, cancellationToken);
            return basket;
        }

        public async Task<Basket> ChangeBasketQuantityAsync(Basket basket, CancellationToken cancellationToken)
        {
            var entity = await db.Set<Basket>().FirstOrDefaultAsync(m => m.UserId == basket.UserId && m.CatalogId == basket.CatalogId);
            if (entity is null)
            {
                throw new BadRequestException("BAD_DATA", new Dictionary<string, IEnumerable<string>>
                {
                    [nameof(basket.CatalogId)] = new[] {"Product can't be fount"}
                });
            }
            if (basket.Quantity <= 0)
            {
                throw new BadRequestException("BAD_DATA", new Dictionary<string, IEnumerable<string>>
                {
                    [nameof(basket.CatalogId)] = new[] { "Invalid value for Quantity" }
                });
            }
            entity.Quantity = basket.Quantity;
            db.SaveChangesAsync(cancellationToken);
            return entity;
        }

        public IQueryable<Basket> GetBasket(int userId)
        {
            return db.Set<Basket>().Where(m => m.UserId == userId);
        }

        public async Task<IEnumerable<Brand>> GetBrandsForFilter()
        {
            var brandIds = await this.GetAll(m => m.DeletedBy == null).Select(m => m.BrandId).Distinct().ToArrayAsync();

            return await db.Set<Brand>().Where(m => brandIds.Contains(m.Id)).ToListAsync();
        }

        public IQueryable<ProductCatalog> GetCatalog(Expression<Func<ProductCatalog, bool>> expression = null)
        {
            var query = db.Set<ProductCatalog>().AsQueryable();
            if (expression is not null)
            {
                query = query.Where(expression);
            }
            return query;
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

        public async Task<string> GetPriceAsync(ProductCatalog model, CancellationToken cancellationToken)
        {
            var entity = await db.Set<ProductCatalog>().FirstOrDefaultAsync(m=>m.ProductId == model.ProductId
              && m.SizeId == model.SizeId 
              && m.ColorId == model.ColorId 
              && m.MaterialId == model.MaterialId, cancellationToken);

            if (entity?.Price != null)
            {
                return $"{entity?.Price.Value:0.00}₼";
            }
            var product = this.Get(m => m.Id == model.ProductId);

            return $"{product.Price:0.00}₼";
        }

        public async Task<IEnumerable<Size>> GetSizesForFilter()
        {
            var sizeIds = await db.Set<ProductCatalog>().Select(m => m.SizeId).Distinct().ToArrayAsync();
            var sizes = await db.Set<Size>().Where(m => sizeIds.Contains(m.Id)).ToListAsync();
            return sizes;
        }

        public async Task RemoveFromBasketAsync(Basket basket, CancellationToken cancellationToken)
        {
            var entity = await db.Set<Basket>().FirstOrDefaultAsync(m => m.UserId == basket.UserId && m.CatalogId == basket.CatalogId, cancellationToken);

            if (entity is null)
                throw new BadRequestException("BAD_DATA", new Dictionary<string, IEnumerable<string>>
                {
                    [nameof(basket.CatalogId)] = new[] { "Product cant be found!" }
                });

            db.Set<Basket>().Remove(entity);

            await db.SaveChangesAsync(cancellationToken);
        }

        public async Task<string> SetRateAsync(ProductRate rate, CancellationToken cancellationToken)
        {
            var product = this.Get(m => m.Id == rate.ProductId && m.DeletedBy == null);
            var productRate = await db.Set<ProductRate>().FirstOrDefaultAsync(m=>m.ProductId==rate.ProductId &&m.UserId==rate.UserId,cancellationToken);
            if (productRate!=null)
            {
                productRate.Rate = rate.Rate;
            }
            else
            {
                productRate = rate;
                await db.Set<ProductRate>().AddAsync(productRate, cancellationToken);
            }

            await db.SaveChangesAsync(cancellationToken);
            product.Rate = db.Set<ProductRate>().Where(m => m.ProductId == product.Id).Average(m => m.Rate);
           return productRate.Rate switch
            {
                var v when v >= 4.8D => "rate-5",
                var v when v > 4D => "rate-half5",
                var v when v >= 3.8D => "rate-4",
                var v when v > 3D => "rate-half4",
                var v when v >= 2.8D => "rate-3",
                var v when v > 2D => "rate-half3",
                var v when v >= 1.8D => "rate-2",
                var v when v > 1D => "rate-half2",
                var v when v >= 0.8D => "rate-1",
                var v when v > 0 => "rate-half1",
                _ => ""
            };
        }
    }
}
 