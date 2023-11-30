using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.MaterialsModule.Queries.MaterialGetAllQuery
{
    internal class MaterialGetAllRequestHandler : IRequestHandler<MaterialGetAllRequest, IEnumerable<Material>>
    {
        private readonly IMaterialRepository materialRepository;

        public MaterialGetAllRequestHandler(IMaterialRepository materialRepository)
        {
            this.materialRepository = materialRepository;
        }
        public async Task<IEnumerable<Material>> Handle(MaterialGetAllRequest request, CancellationToken cancellationToken)
        {
            return  materialRepository.GetAll(m => m.DeletedBy == null).ToList();
        }
    }
}
