using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BrandsModule.Commands.BrandRemoveCommand
{
    public class BrandRemoveRequest : IRequest
    {
        public byte Id { get; set; }
    }
}
