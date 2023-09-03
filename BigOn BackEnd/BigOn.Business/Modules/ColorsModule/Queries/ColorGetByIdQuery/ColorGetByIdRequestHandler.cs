using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ColorsModule.Queries.ColorGetByIdQuery
{
    internal class ColorGetByIdRequestHandler : IRequestHandler<ColorGetByIdRequest, Color>
    {
        private readonly IColorRepository colorRepository;

        public ColorGetByIdRequestHandler(IColorRepository colorRepository)
        {
            this.colorRepository = colorRepository;
        }
        public async Task<Color> Handle(ColorGetByIdRequest request, CancellationToken cancellationToken)
        {
            var data = colorRepository.Get(m=>m.Id == request.Id && m.DeletedBy == null);
            return data;
        }
    }
}
