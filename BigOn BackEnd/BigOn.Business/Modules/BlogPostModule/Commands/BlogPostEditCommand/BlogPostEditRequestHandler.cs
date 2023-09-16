using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using BigOn.Infrastructure.Services.Abstracts;
using Ganss.Xss;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BlogPostModule.Commands.BlogPostEditCommand
{
    internal class BlogPostEditRequestHandler : IRequestHandler<BlogPostEditRequest, BlogPost>
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IFileService fileService;

        public BlogPostEditRequestHandler(IBlogPostRepository blogPostRepository,IFileService fileService)
        {
            this.blogPostRepository = blogPostRepository;
            this.fileService = fileService;
        }
        public async Task<BlogPost> Handle(BlogPostEditRequest request, CancellationToken cancellationToken)
        {
            var sanitizer = new HtmlSanitizer();
            var entity = blogPostRepository.Get(m => m.Id == request.Id && m.DeletedBy == null);
            entity.Title = request.Title;
            entity.Body = sanitizer.Sanitize(request.Body);
            entity.CategoryId = request.CategoryId;
          entity.ImagePath = fileService.ChangeFile(request.File,entity.ImagePath);

            blogPostRepository.ResolveTags(entity, request.Tags);
            blogPostRepository.Save();

            return entity;
        }
    }
}
