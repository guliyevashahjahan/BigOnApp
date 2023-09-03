using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ColorsModule.Commands.ColorEditCommand
{
    internal class ColorEditRequestHandler : IRequestHandler<ColorEditRequest, Color>
    {
        private readonly IColorRepository colorRepository;

        public ColorEditRequestHandler(IColorRepository colorRepository)
        {
            this.colorRepository = colorRepository;
        }
        public async Task<Color> Handle(ColorEditRequest request, CancellationToken cancellationToken)
        {
            var color = new Color
            {
                Name = request.Name,
                HexCode = request.HexCode,
                Id = request.Id,

            };

            colorRepository.Edit(color);
            colorRepository.Save();
            return color;
        }
    }
}
