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
        public int ProductId { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int MaterialId { get; set; }
        public decimal Quantity { get; set;}
    }

}
