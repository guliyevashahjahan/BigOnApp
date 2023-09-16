using BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetAllQuery;
using BigOn.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetByIdQuery
{
    internal class BlogPostGetByIdRequestHandler : IRequestHandler<BlogPostGetByIdRequest, BlogPostGetByIdDto>
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostGetByIdRequestHandler(IBlogPostRepository blogPostRepository,ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }
        public async Task<BlogPostGetByIdDto> Handle(BlogPostGetByIdRequest request, CancellationToken cancellationToken)
        {
           var query =  (from bp in blogPostRepository.GetAll(m=>m.DeletedBy==null)
                                         join c in categoryRepository.GetAll() on bp.CategoryId equals c.Id
                               where bp.Id == request.Id
                                         select new BlogPostGetByIdDto
                                         {
                                             Id = bp.Id,
                                             Title = bp.Title,
                                             Body = bp.Body,
                                             Slug = bp.Slug,
                                             ImagePath = bp.ImagePath,
                                             PublishedAt = bp.PublishedAt,
                                             CategoryId = bp.CategoryId,
                                             CategoryName = c.Name

                                         });

            var data = await query.FirstOrDefaultAsync(cancellationToken);
            data.Tags = await blogPostRepository.GetTagsByBlogPostId(request.Id).Select(m=>m.Text).ToArrayAsync(cancellationToken);
            return  data;
        }
    }
}
