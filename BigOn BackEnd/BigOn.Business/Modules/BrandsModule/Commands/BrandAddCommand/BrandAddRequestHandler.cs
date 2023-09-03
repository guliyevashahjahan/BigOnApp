using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BrandsModule.Commands.BrandAddCommand
{
    internal class BrandAddRequestHandler : IRequestHandler<BrandAddRequest, Brand>
    {
        private readonly IBrandRepository brandRepository;

        public BrandAddRequestHandler(IBrandRepository brandRepository)
        {
            this.brandRepository = brandRepository;
        }
        public async Task<Brand> Handle(BrandAddRequest request, CancellationToken cancellationToken)
        {
            var brand = new Brand
            {
                Name = request.Name,
            };
            brandRepository.Add(brand);
            return brand;
        }
    }
}
