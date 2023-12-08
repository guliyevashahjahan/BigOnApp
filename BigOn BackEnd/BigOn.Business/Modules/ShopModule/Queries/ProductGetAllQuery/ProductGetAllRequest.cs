using BigOn.Infrastructure.Commons.Abstracts;
using BigOn.Infrastructure.Commons.Concrates;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ShopModule.Queries.ProductGetAllQuery
{
    public class ProductGetAllRequest : Pageable, IRequest<IPagedResponse<ProductGetAllDto>>
    {
        public override int Size
        {
            get
            {
                return base.Size < 12 ? 12 : base.Size;
            }
            set
            {
                base.Size = value < 12 ? 12 : value;
            }
        }
    }
}
