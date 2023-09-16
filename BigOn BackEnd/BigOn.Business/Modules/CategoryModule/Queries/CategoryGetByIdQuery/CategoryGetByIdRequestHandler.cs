 using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.CategoryModule.Queries.CategoryGetByIdQuery
{
    internal class CategoryGetByIdRequestHandler : IRequestHandler<CategoryGetByIdRequest, CategoryGetByIdDto>
    {
        private readonly ICategoryRepository categoryrepository;

        public CategoryGetByIdRequestHandler(ICategoryRepository categoryrepository)
        {
            this.categoryrepository = categoryrepository;
        }
        public async Task<CategoryGetByIdDto> Handle(CategoryGetByIdRequest request, CancellationToken cancellationToken)
        {
            var query = from current in categoryrepository.GetAll(m => m.Id == request.Id && m.DeletedBy == null)
                        join parent in categoryrepository.GetAll() on current.ParentId equals parent.Id into ljCurrent
                        from lj in ljCurrent.DefaultIfEmpty()
                        select new CategoryGetByIdDto
                        {
                            Id = current.Id,
                            Name = current.Name,
                            ParentId = current.ParentId,
                            ParentName = lj.Name
                        };

            return await query.FirstOrDefaultAsync(cancellationToken);
        }
    }
}
