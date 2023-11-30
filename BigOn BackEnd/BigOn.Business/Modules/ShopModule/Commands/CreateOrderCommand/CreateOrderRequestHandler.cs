using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using BigOn.Infrastructure.Services.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ShopModule.Commands.CreateOrderCommand
{
    internal class CreateOrderRequestHandler : IRequestHandler<CreateOrderRequest, Order>
    {
        private readonly IProductRepository productRepository;
        private readonly IIdentityService identityService;

        public CreateOrderRequestHandler(IProductRepository productRepository, IIdentityService identityService)
        {
            this.productRepository = productRepository;
            this.identityService = identityService;
        }
        public async Task<Order> Handle(CreateOrderRequest request, CancellationToken cancellationToken)
        {
            var order =new Order
            {
                ShippingAddress = request.ShippingAddress,
                ShippingCity = request.ShippingCity,
                ShippingCountry = request.ShippingCountry,
                Email = request.Email,
                Phone = request.Phone,
                Postcode = request.Postcode,
                CouponCode = request.CouponCode
            };

            return await productRepository.CreateOrder(order,identityService.GetPrincipalId().Value, cancellationToken);
        }
    }
}
