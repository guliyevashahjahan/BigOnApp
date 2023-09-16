using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BlogPostModule.Queries.TagGetUsedQuery
{
    internal class TagGetUsedRequestHandler : IRequestHandler<TagGetUsedRequest,IEnumerable<Tag>>
    {
        private readonly IBlogPostRepository blogPostRepository;

        public TagGetUsedRequestHandler(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }

        public async Task<IEnumerable<Tag>> Handle(TagGetUsedRequest request, CancellationToken cancellationToken)
        {
           var data =  await blogPostRepository.GetUsedTags().ToListAsync(cancellationToken);
            return data;
        }
    }
}
