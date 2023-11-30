using BigOn.Infrastructure.Commons.Abstracts;
using BigOn.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Size>> GetSizesForFilter();
        Task<IEnumerable<Color>> GetColorsForFilter();
        Task<IEnumerable<Material>> GetMaterialsForFilter();
        Task<IEnumerable<Brand>> GetBrandsForFilter();
        IQueryable<ProductCatalog> GetCatalog(Expression<Func<ProductCatalog, bool>> expression = null);
        IQueryable<ProductImage> GetImages(Expression<Func<ProductImage, bool>> expression = null);
        IQueryable<Basket> GetBasket(int userId);
        Task<Basket> AddToBasketAsync(Basket basket,CancellationToken cancellationToken);
        Task RemoveFromBasketAsync(Basket basket, CancellationToken cancellationToken);
        Task<Basket> ChangeBasketQuantityAsync(Basket basket,CancellationToken cancellationToken);
        Task<string> SetRateAsync(ProductRate rate,CancellationToken cancellationToken);
        Task<string> GetPriceAsync(ProductCatalog model,CancellationToken cancellationToken);
        Task<Order> CreateOrder(Order model, int userId,CancellationToken cancellationToken);
        Task<ProductImage> AddProductImageAsync(int productId, ProductImage image,CancellationToken cancellationToken);
        Task<ProductCatalog> AddProductCatalogItemAsync(int productId, ProductCatalog item,CancellationToken cancellationToken);
    }
}
