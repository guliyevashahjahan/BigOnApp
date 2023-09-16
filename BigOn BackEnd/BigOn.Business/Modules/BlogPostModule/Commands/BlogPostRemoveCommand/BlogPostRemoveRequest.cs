using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BlogPostModule.Commands.BlogPostRemoveCommand
{
    public class BlogPostRemoveRequest : IRequest
    {
        public int Id { get; set; }
    }
}
