using BigOn.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ColorsModule.Commands.ColorRemoveCommand
{
    internal class ColorRemoveRequestHandler : IRequestHandler<ColorRemoveRequest>

    {
        private readonly IColorRepository colorRepository;

        public ColorRemoveRequestHandler(IColorRepository colorRepository)
        {
            this.colorRepository = colorRepository;
        }
        public async Task Handle(ColorRemoveRequest request, CancellationToken cancellationToken)
        {
            var model = colorRepository.Get(m=>m.Id ==  request.Id);
            colorRepository.Remove(model);
            colorRepository.Save();
        }
    }
}
