using BigOn.Infrastructure.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ShopModule.Commands.AddBasketCommand
{
    public class BasketAddRequest : IRequest<Basket>
    {
        public int CatalogId { get; set; }
        public decimal Quantity { get; set;}
    }

}
