using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using BigOn.Infrastructure.Services.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
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
            var basket = new Basket
            {
                UserId = identityService.GetPrincipalId().Value,
                CatalogId = request.CatalogId,
                Quantity = request.Quantity,
            };

            return await productRepository.AddToBasketAsync(basket, cancellationToken); ;
        }
    }
}
