using BigOn.Business.Modules.BrandsModule.Queries.BrandGetAllQuery;
using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.CategoryModule.Queries.CategoryGetAllQuery
{
    internal class CategoryGetAllRequestHandler : IRequestHandler<CategoryGetAllRequest, IEnumerable<Category>>
    {
        private readonly ICategoryRepository categoryrepository;

        public CategoryGetAllRequestHandler(ICategoryRepository categoryrepository)
        {
            this.categoryrepository = categoryrepository;
        }
        public async Task<IEnumerable<Category>> Handle(CategoryGetAllRequest request, CancellationToken cancellationToken)
        {
            var categories = categoryrepository.GetAll(m => m.DeletedBy == null);
            return categories;
        }
    }

}
