using BigOn.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.CategoryModule.Commands.CategoryRemoveCommand
{
    internal class CategoryRemoveRequestHandler : IRequestHandler<CategoryRemoveRequest>
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryRemoveRequestHandler(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public async Task Handle(CategoryRemoveRequest request, CancellationToken cancellationToken)
        {
            var model = categoryRepository.Get(m => m.Id == request.Id);
            categoryRepository.Remove(model);
            categoryRepository.Save();
        }
    }
}
