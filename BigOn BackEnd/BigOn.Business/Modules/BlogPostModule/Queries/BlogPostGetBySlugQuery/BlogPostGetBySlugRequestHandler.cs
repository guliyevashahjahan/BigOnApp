using BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetByIdQuery;
using BigOn.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetBySlugQuery
{
    internal class BlogPostGetBySlugRequestHandler : IRequestHandler<BlogPostGetBySlugRequest, BlogPostGetByIdDto>
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly ICategoryRepository categoryRepository;

        public BlogPostGetBySlugRequestHandler(IBlogPostRepository blogPostRepository,ICategoryRepository categoryRepository)
        {
            this.blogPostRepository = blogPostRepository;
            this.categoryRepository = categoryRepository;
        }
        public async Task<BlogPostGetByIdDto> Handle(BlogPostGetBySlugRequest request, CancellationToken cancellationToken)
        {
            var query = (from bp in blogPostRepository.GetAll(m => m.DeletedBy == null)
                         join c in categoryRepository.GetAll() on bp.CategoryId equals c.Id
                         where bp.Slug == request.Slug
                         select new BlogPostGetByIdDto
                         {
                             Id = bp.Id,
                             Title = bp.Title,
                             Body = bp.Body,
                             Slug = bp.Slug,
                             ImagePath = bp.ImagePath,
                             PublishedAt = bp.PublishedAt,
                             PublishedBy = bp.PublishedBy,
                             CategoryId = bp.CategoryId,
                             CategoryName = c.Name
                         });

            var data = await query.FirstOrDefaultAsync(cancellationToken);
           
            data.Tags = await blogPostRepository.GetTagsByBlogPostId(data.Id).Select(m => m.Text).ToArrayAsync(cancellationToken);
           
            data.Comments = blogPostRepository.CommentCounts(data.Id);
            return data;
        }
    }
}
