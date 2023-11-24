using BigOn.Infrastructure.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ShopModule.Commands.SetRateCommand
{
    public class SetRateRequest : IRequest<string>
    {
        public int ProductId { get; set; }
        public int Rate { get; set; }
    }
}
