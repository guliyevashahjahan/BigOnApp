using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.SubscribeModule.Commands.SubscribeTicketCommand
{
    public class SubscribeTicketRequest : IRequest
    {
        public string Email { get; set; }
    }
}
