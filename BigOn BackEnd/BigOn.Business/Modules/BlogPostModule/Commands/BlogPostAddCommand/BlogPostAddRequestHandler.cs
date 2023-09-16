using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Extensions;
using BigOn.Infrastructure.Repositories;
using BigOn.Infrastructure.Services.Abstracts;
using Ganss.Xss;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BlogPostModule.Commands.BlogPostAddCommand
{
    internal class BlogPostAddRequestHandler : IRequestHandler<BlogPostAddRequest, BlogPost>
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IFileService fileService;

        public BlogPostAddRequestHandler(IBlogPostRepository blogPostRepository, IFileService fileService)
        {
            this.blogPostRepository = blogPostRepository;
            this.fileService = fileService;
        }
        public async Task<BlogPost> Handle(BlogPostAddRequest request, CancellationToken cancellationToken)
        {
            var sanitizer = new HtmlSanitizer();
            var model = new BlogPost
            {
                Title = request.Title,
                Body =sanitizer.Sanitize(request.Body),
                CategoryId = request.CategoryId
            };
            model.ImagePath = fileService.Upload(request.File);
            model.Slug = model.Title.ToSlug();
            blogPostRepository.Add(model);
            blogPostRepository.Save();

            blogPostRepository.ResolveTags(model,request.Tags);
            blogPostRepository.Save();


            return model;
        }
    }
}
