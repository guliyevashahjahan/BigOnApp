using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetByIdQuery
{
    public class BlogPostGetByIdRequest : IRequest<BlogPostGetByIdDto>
    {
        public int Id { get; set; }
    }
}
