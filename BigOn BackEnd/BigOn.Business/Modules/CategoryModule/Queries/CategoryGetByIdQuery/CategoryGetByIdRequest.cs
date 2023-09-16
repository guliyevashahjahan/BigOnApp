using BigOn.Infrastructure.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.CategoryModule.Queries.CategoryGetByIdQuery
{
    public class CategoryGetByIdRequest : IRequest<CategoryGetByIdDto>
    {
        public int Id { get; set; }
    }
}
