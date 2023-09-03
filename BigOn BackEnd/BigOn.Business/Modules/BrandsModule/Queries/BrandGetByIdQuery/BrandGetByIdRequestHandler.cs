using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BrandsModule.Queries.BrandGetByIdQuery
{
    internal class BrandGetByIdRequestHandler : IRequestHandler<BrandGetByIdRequest, Brand>
    {
        private readonly IBrandRepository brandRepository;

        public BrandGetByIdRequestHandler(IBrandRepository brandRepository)
        {
            this.brandRepository = brandRepository;
        }
        public async Task<Brand> Handle(BrandGetByIdRequest request, CancellationToken cancellationToken)
        {
           var model = brandRepository.Get(m=>m.Id == request.Id && m.DeletedBy == null);
            return model;
        }
    }
}
