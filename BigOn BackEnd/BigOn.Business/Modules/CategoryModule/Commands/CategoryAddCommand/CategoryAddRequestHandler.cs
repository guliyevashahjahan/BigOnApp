using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.CategoryModule.Commands.CategoryAddCommand
{
    internal class CategoryAddRequestHandler : IRequestHandler<CategoryAddRequest, Category>
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryAddRequestHandler(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public async Task<Category> Handle(CategoryAddRequest request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Name = request.Name,
                ParentId = request.ParentId
            };
            categoryRepository.Add(category);
            categoryRepository.Save();
            return category;
        }
    }
}
