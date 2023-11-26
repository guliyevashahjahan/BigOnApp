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

namespace BigOn.Business.Modules.ShopModule.Commands.RemoveFromBasketCommand
{
    internal class RemoveFromBasketRequestHandler : IRequestHandler<RemoveFromBasketRequest, BasketSummary>
    {
        private readonly IProductRepository productRepository;
        private readonly IIdentityService identityService;

        public RemoveFromBasketRequestHandler(IProductRepository productRepository,IIdentityService identityService)
        {
            this.productRepository = productRepository;
            this.identityService = identityService;
        }
        public async Task<BasketSummary> Handle(RemoveFromBasketRequest request, CancellationToken cancellationToken)
        {
            var basket = new Basket
            {
                UserId = identityService.GetPrincipalId().Value,
                CatalogId = request.CatalogId
            };

            await productRepository.RemoveFromBasketAsync(basket, cancellationToken);

            var summary = (from b in productRepository.GetBasket(identityService.GetPrincipalId().Value)
                                 join pc in productRepository.GetCatalog() on b.CatalogId equals pc.Id
                                 join p in productRepository.GetAll() on pc.ProductId equals p.Id
                                 select new
                                 {
                                     b.Quantity,
                                     Price = pc.Price == null ? p.Price : pc.Price.Value
                                 })
                         .GroupBy(m => 1)
                         .Select(m => new BasketSummary
                         {
                             Count = m.Count(),
                             Total = m.Sum(x => x.Quantity * x.Price)
                         }).FirstOrDefault();

            return summary ?? new();
        }
    }
}
