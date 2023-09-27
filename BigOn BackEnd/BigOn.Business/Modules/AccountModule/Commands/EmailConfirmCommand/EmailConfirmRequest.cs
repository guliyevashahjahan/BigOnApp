using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.AccountModule.Commands.EmailConfirmCommand
{
    public class EmailConfirmRequest : IRequest
    {
        public string Email { get; set; }
        public string Token { get; set; }
    }
}
