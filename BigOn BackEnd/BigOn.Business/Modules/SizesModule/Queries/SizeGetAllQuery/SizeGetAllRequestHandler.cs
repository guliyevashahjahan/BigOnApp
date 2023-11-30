using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.SizesModule.Queries.SizeGetAllQuery
{
    internal class SizeGetAllRequestHandler : IRequestHandler<SizeGetAllRequest, IEnumerable<Size>>
    {
        private readonly ISizeRepository sizeRepository;

        public SizeGetAllRequestHandler(ISizeRepository sizeRepository)
        {
            this.sizeRepository = sizeRepository;
        }
        public async Task<IEnumerable<Size>> Handle(SizeGetAllRequest request, CancellationToken cancellationToken)
        {
            return sizeRepository.GetAll(m => m.DeletedBy == null).ToList();
        }
    }
}
