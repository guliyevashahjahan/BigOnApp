using BigOn.Infrastructure.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.MaterialsModule.Queries.MaterialGetAllQuery
{
    public class MaterialGetAllRequest : IRequest<IEnumerable<Material>>
    {
    }
}
