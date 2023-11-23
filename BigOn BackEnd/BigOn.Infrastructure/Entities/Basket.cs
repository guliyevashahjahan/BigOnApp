using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Entities
{
    public class Basket
    {
        public int UserId { get; set; }
        public int CatalogId { get; set; }
        public decimal Quantity { get; set; }
    }
}
