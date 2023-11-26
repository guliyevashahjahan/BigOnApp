using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ShopModule.Commands.RemoveFromBasketCommand
{
    public class RemoveFromBasketRequest : IRequest<BasketSummary>
    {
        public int CatalogId { get; set; }
    }
}
