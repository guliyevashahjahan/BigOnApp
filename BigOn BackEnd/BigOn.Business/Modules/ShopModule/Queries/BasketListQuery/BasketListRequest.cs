using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ShopModule.Queries.BasketListQuery
{
    public class BasketListRequest : IRequest<IEnumerable<BasketListItem>>
    {
    }
}
