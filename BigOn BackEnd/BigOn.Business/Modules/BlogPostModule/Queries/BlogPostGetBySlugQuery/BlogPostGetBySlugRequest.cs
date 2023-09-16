using BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetAllQuery;
using BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetByIdQuery;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetBySlugQuery
{
    public class BlogPostGetBySlugRequest : IRequest<BlogPostGetByIdDto>
    {
        public string Slug { get; set; }
    }
}
