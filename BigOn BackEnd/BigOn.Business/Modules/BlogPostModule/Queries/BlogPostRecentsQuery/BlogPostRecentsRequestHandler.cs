using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BlogPostModule.Queries.BlogPostRecentsQuery
{
    internal class BlogPostRecentsRequestHandler : IRequestHandler<BlogPostRecentsRequest, IEnumerable<BlogPost>>
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogPostRecentsRequestHandler(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        public async Task<IEnumerable<BlogPost>> Handle(BlogPostRecentsRequest request, CancellationToken cancellationToken)
        {
            var response = await blogPostRepository.GetAll(m => m.DeletedBy == null).
                OrderByDescending(m => m.CreatedAt).
                Take(request.Size).
                ToListAsync(cancellationToken);

            return response;
        }
    }
}
