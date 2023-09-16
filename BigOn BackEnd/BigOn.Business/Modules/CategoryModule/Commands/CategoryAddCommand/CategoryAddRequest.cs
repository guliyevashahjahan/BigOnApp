using BigOn.Infrastructure.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.CategoryModule.Commands.CategoryAddCommand
{
    public class CategoryAddRequest : IRequest<Category>
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
