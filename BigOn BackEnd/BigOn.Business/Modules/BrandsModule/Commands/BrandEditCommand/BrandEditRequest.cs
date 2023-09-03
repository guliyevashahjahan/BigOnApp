using BigOn.Infrastructure.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BrandsModule.Commands.BrandEditCommand
{
    public class BrandEditRequest : IRequest<Brand>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
