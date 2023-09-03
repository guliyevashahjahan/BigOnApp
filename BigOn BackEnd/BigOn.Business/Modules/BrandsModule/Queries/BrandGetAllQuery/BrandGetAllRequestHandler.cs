using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BrandsModule.Queries.BrandGetAllQuery
{
    internal class BrandGetAllRequestHandler : IRequestHandler<BrandGetAllRequest, IEnumerable<Brand>>
    {
        private readonly IBrandRepository brandRepository;

        public BrandGetAllRequestHandler(IBrandRepository brandRepository)
        {
            this.brandRepository = brandRepository;
        }
        public async Task<IEnumerable<Brand>> Handle(BrandGetAllRequest request, CancellationToken cancellationToken)
        {
            var brands = brandRepository.GetAll(m => m.DeletedBy == null);
            return brands;
        }
    }
}
