using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.CategoryModule.Commands.CategoryRemoveCommand
{
    public class CategoryRemoveRequest : IRequest
    {
        public byte Id { get; set; }
    }
}
