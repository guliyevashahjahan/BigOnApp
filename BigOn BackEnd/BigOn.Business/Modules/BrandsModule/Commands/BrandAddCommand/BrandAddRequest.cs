using BigOn.Infrastructure.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BrandsModule.Commands.BrandAddCommand
{
    public class BrandAddRequest : IRequest<Brand>
    {
        public string Name { get; set; }
    }
}
