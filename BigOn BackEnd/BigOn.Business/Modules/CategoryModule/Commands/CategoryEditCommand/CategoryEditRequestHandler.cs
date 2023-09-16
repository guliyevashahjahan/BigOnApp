using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Extensions;
using BigOn.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.CategoryModule.Commands.CategoryEditCommand
{
    internal class CategoryEditRequestHandler : IRequestHandler<CategoryEditRequest, Category>
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryEditRequestHandler(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }
        public async Task<Category> Handle(CategoryEditRequest request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                Id = request.Id,
                Name = request.Name,
                ParentId = request.ParentId
            };
            if (category.ParentId != null )
            {
                var childDetect = categoryRepository.GetAll(tracking:false).GetHierarchy(category).Any(m=>m.Id== request.ParentId);
                if (childDetect)
                {
                    category.ParentId = null; 
                }
            }



            categoryRepository.Edit(category);
            categoryRepository.Save();
            return category;
        }
    }
}
