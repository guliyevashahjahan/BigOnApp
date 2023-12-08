using BigOn.Infrastructure.Commons.Abstracts;
using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Extensions;
using BigOn.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ShopModule.Queries.ProductGetAllQuery
{
    internal class ProductGetAllRequestHandler : IRequestHandler<ProductGetAllRequest, IPagedResponse<ProductGetAllDto>>
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IBrandRepository brandRepository;

        public ProductGetAllRequestHandler(IProductRepository productRepository,
            ICategoryRepository categoryRepository,IBrandRepository brandRepository)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.brandRepository = brandRepository;
        }
        public async Task<IPagedResponse<ProductGetAllDto>> Handle(ProductGetAllRequest request, CancellationToken cancellationToken)
        {
            var query = (from p in productRepository.GetAll(m => m.DeletedBy == null)
                         join b in brandRepository.GetAll() on p.BrandId equals b.Id
                         join c in categoryRepository.GetAll() on p.BrandId equals c.Id
                         join i in productRepository.GetImages(m => m.IsMain == true) on p.Id equals i.ProductId
                         select new ProductGetAllDto
                         {
                             Id = p.Id,
                             Name = p.Name,
                             ShortDescription = p.ShortDescription,
                             Price = p.Price,
                             StockKeepingUnit = p.StockKeepingUnit,
                             Rate = p.Rate,
                             BrandId = p.BrandId,
                             BrandName = b.Name,
                             CategoryId = p.CategoryId,
                             CategoryName = c.Name,
                             ImagePath = i.Name,

                         });
            return query.ToPaging(request, m => m.Id);
        }
    }
}
