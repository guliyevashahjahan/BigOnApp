using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ShopModule.Queries.GetPriceQuery
{
    internal class GetPriceRequestHandler : IRequestHandler<GetPriceRequest, string>
    {
        private readonly IProductRepository productRepository;

        public GetPriceRequestHandler(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public async Task<string> Handle(GetPriceRequest request, CancellationToken cancellationToken)
        {
            var model = new ProductCatalog
            {
                ProductId = request.ProductId,
                SizeId = request.SizeId,
                ColorId = request.ColorId,
                MaterialId = request.MaterialId,
            };

           return await productRepository.GetPriceAsync(model, cancellationToken);
        }
    }
}
