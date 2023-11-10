using BigOn.Infrastructure.Commons.Abstracts;
using BigOn.Infrastructure.Commons.Concrates;
using BigOn.Infrastructure.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ShopModule.Queries.ComplexFilterQuery
{
    public class ComplexFilterRequest : Pageable, IRequest<IPagedResponse<ComplexFilterResponseDto>>
    {
        public int[] Sizes { get; set; }
        public int[] Brands { get; set; }
        public int[] Materials { get; set; }
        public int[] Colors { get; set; }

        public ComplexFilterPrice Price { get; set; }
    }
}
