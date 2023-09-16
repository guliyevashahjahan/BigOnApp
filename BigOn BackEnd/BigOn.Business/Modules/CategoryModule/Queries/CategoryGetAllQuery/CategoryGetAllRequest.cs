using BigOn.Infrastructure.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.CategoryModule.Queries.CategoryGetAllQuery
{
    public class CategoryGetAllRequest : IRequest<IEnumerable<Category>>
    {
    }
}
