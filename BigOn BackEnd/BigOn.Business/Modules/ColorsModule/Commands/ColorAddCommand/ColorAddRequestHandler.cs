using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ColorsModule.Commands.ColorAddCommand
{
    internal class ColorAddRequestHandler : IRequestHandler<ColorAddRequest, Color>
    {
        private readonly IColorRepository colorRepository;

        public ColorAddRequestHandler(IColorRepository colorRepository)
        {
            this.colorRepository = colorRepository;
        }
        public async Task<Color> Handle(ColorAddRequest request, CancellationToken cancellationToken)
        {
            var color = new Color
            {
                HexCode = request.HexCode,
                Name = request.Name,
            };
            colorRepository.Add(color);
            colorRepository.Save();

            return color;
        }
    }
}
