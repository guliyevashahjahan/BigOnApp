using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using BigOn.Infrastructure.Services.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ShopModule.Commands.SetRateCommand
{
    internal class SetRateRequestHandler : IRequestHandler<SetRateRequest, ProductRate>
    {
        private readonly IProductRepository productRepository;
        private readonly IIdentityService identityService;

        public SetRateRequestHandler(IProductRepository productRepository,IIdentityService identityService)
        {
            this.productRepository = productRepository;
            this.identityService = identityService;
        }
        public async Task<ProductRate> Handle(SetRateRequest request, CancellationToken cancellationToken)
        {
            var rate = new ProductRate
            {
                ProductId = request.ProductId,
                Rate = request.Rate,
                UserId = identityService.GetPrincipalId().Value
            };
            return await productRepository.SetRateAsync(rate, cancellationToken);
        }
    }
}
