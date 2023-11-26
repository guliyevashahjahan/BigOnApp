using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using BigOn.Infrastructure.Services.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ShopModule.Commands.AddBasketCommand
{
    internal class BasketAddRequestHandler : IRequestHandler<BasketAddRequest, Basket>
    {
        private readonly IProductRepository productRepository;
        private readonly IIdentityService identityService;

        public BasketAddRequestHandler(IProductRepository productRepository, IIdentityService identityService)
        {
            this.productRepository = productRepository;
            this.identityService = identityService;
        }
        public async Task<Basket> Handle(BasketAddRequest request, CancellationToken cancellationToken)
        {
            var catalog =productRepository.GetCatalog(m => m.ProductId == request.ProductId
            && m.ColorId == request.ColorId
            && m.MaterialId == request.MaterialId
            && m.SizeId == request.SizeId).FirstOrDefault();

            var basket = new Basket
            {
                UserId = identityService.GetPrincipalId().Value,
                CatalogId = catalog.Id,
                Quantity = request.Quantity<=0 ? 1 : request.Quantity,
            };

            await productRepository.AddToBasketAsync(basket, cancellationToken);
            productRepository.Save();
            return basket;
        }
    }
}
