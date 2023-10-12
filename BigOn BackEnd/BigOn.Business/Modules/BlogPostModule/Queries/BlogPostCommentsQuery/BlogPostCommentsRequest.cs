using BigOn.Business.Modules.BlogPostModule.Commands.BlogPostAddComment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BlogPostModule.Queries.BlogPostCommentsQuery
{
    public class BlogPostCommentsRequest : IRequest<IEnumerable<BlogPostCommentDto>>
    {
        public int PostId { get; set; }
    }
}
