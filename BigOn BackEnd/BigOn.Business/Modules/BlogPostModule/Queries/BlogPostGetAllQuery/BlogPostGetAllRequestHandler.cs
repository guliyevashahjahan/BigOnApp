using BigOn.Infrastructure.Commons.Abstracts;
using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Extensions;
using BigOn.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetAllQuery
{
    internal class BlogPostGetAllRequestHandler : IRequestHandler<BlogPostGetAllRequest, IPagedResponse<BlogPostGetAllDto>>
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostGetAllRequestHandler(IBlogPostRepository blogPostRepository, ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }
        public async Task<IPagedResponse<BlogPostGetAllDto>> Handle(BlogPostGetAllRequest request, CancellationToken cancellationToken)
        {
            // var data = await blogPostRepository.GetAll(m => m.DeletedBy == null).ToListAsync(cancellationToken);
            var query = (from bp in blogPostRepository.GetAll(m => m.DeletedBy == null)
                         join c in categoryRepository.GetAll() on bp.CategoryId equals c.Id
                         select new BlogPostGetAllDto
                         {
                             Id = bp.Id,
                             Body = bp.Body,
                             Title = bp.Title,
                             Slug = bp.Slug,
                             ImagePath = bp.ImagePath,
                             PublishedAt = bp.PublishedAt,
                             CategoryId = bp.CategoryId,
                             CategoryName = c.Name

                         });

            var data = query.ToPaging(request, m => m.Id);
            return data;
        }
    }
}
