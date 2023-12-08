using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ShopModule.Queries.ProductGetByIdQuery
{
    public class ProductGetByIdRequest : IRequest<ProductGetByIdDto>
    {
        public int Id { get; set; }
    }
}
