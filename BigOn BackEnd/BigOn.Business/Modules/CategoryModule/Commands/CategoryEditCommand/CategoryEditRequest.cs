using BigOn.Infrastructure.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.CategoryModule.Commands.CategoryEditCommand
{
    public class CategoryEditRequest : IRequest<Category>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
