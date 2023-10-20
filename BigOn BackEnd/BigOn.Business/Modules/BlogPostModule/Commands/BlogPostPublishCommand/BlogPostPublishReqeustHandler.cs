using BigOn.Infrastructure.Repositories;
using BigOn.Infrastructure.Services.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BlogPostModule.Commands.BlogPostPublishCommand
{
    internal class BlogPostPublishReqeustHandler : IRequestHandler<BlogPostPublishRequest>
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IIdentityService identityService;
        private readonly IDateTimeService dateTimeService;

        public BlogPostPublishReqeustHandler(IBlogPostRepository blogPostRepository,
            IIdentityService identityService,
            IDateTimeService dateTimeService)
        {
            this.blogPostRepository = blogPostRepository;
            this.identityService = identityService;
            this.dateTimeService = dateTimeService;
        }

        public async Task Handle(BlogPostPublishRequest request, CancellationToken cancellationToken)
        {
            var entity = blogPostRepository.Get(m => m.Id == request.PostId && m.DeletedBy == null);
            if (entity.PublishedBy !=null)
            {
                throw new Exception("This post is already published");
            }
            entity.PublishedBy = identityService.GetPrincipalId();
            entity.PublishedAt = dateTimeService.ExecutingTime;
            blogPostRepository.Save();
        }
    }
}
