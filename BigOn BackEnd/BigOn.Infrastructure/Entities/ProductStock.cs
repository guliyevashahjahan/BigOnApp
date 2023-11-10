using BigOn.Infrastructure.Commons.Concrates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Entities
{
    public class ProductStock : BaseEntity<int>
    {
        public int CatalogId { get; set; }
        public string DocumentNo { get; set; }
        public decimal Quantity { get; set; }
    }
}
