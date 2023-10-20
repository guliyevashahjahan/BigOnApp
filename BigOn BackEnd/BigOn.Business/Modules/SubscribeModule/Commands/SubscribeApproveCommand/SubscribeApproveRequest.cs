using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.SubscribeModule.Commands.SubscribeApproveCommand
{
    public class SubscribeApproveRequest : IRequest
    {
        public string Token { get; set; }
    }
}
