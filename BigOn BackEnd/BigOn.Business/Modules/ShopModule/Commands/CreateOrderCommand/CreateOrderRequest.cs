using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ShopModule.Commands.CreateOrderCommand
{
    public class CreateOrderRequest : IRequest<Order>
    {
        public string ShippingAddress { get; set; }
        public string ShippingCountry { get; set; }
        public string ShippingCity { get; set; }
        public string Postcode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string CouponCode { get; set; }
    }
}
