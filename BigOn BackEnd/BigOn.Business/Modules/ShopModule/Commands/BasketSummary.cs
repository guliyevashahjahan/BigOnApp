using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ShopModule.Commands
{
    public class BasketSummary
    {
        public int Count { get; set; }
        public decimal Total { get; set; }
    }
}
