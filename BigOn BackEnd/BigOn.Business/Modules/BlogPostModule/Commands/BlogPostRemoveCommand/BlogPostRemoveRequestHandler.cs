using BigOn.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BlogPostModule.Commands.BlogPostRemoveCommand
{
    internal class BlogPostRemoveRequestHandler : IRequestHandler<BlogPostRemoveRequest>
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogPostRemoveRequestHandler(IBlogPostRepository blogPostRepository)
        {
            this.blogPostRepository = blogPostRepository;
        }
        public async Task Handle(BlogPostRemoveRequest request, CancellationToken cancellationToken)
        {
           var model =  blogPostRepository.Get(m=>m.Id == request.Id);
           
                blogPostRepository.Remove(model);
            blogPostRepository.Save();
         
        }
    }
}
