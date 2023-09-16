using BigOn.Infrastructure.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BlogPostModule.Queries.TagGetUsedQuery
{
    public class TagGetUsedRequest : IRequest<IEnumerable<Tag>>
    {
    }
}
