using BigOn.Infrastructure.Entities.Membership;
using BigOn.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Data.Entity;

namespace BigOn.Business.Modules.BlogPostModule.Commands.BlogPostAddComment
{
    internal class BlogPostAddCommentRequestHandler : IRequestHandler<BlogPostAddCommentRequest, BlogPostCommentDto>
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly UserManager<BigonUser> userManager;

        public BlogPostAddCommentRequestHandler(IBlogPostRepository blogPostRepository,UserManager<BigonUser> userManager)
        {
            this.blogPostRepository = blogPostRepository;
            this.userManager = userManager;
        }
        public async  Task<BlogPostCommentDto> Handle(BlogPostAddCommentRequest request, CancellationToken cancellationToken)
        {
            var comment =  blogPostRepository.AddComment(request.PostId, request.ParentId, request.Comment);
            blogPostRepository.Save();
            var user = userManager.Users.FirstOrDefault(m => m.Id == comment.CreatedBy);
            var response = new BlogPostCommentDto
            {
                Id = comment.Id,
                PostId = comment.PostId,
                ParentId = comment.ParentId,
                Comment = comment.Comment,
                CreatedAt = comment.CreatedAt,
                Author = $"{user.Name} {user.Surname}"
            };

            return response;
        }
    }
}
