using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ShopModule.Queries.ComplexFilterQuery
{
    public class ComplexFilterResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StockKeepingUnit { get; set; }
        public decimal Rate { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }

    }
}
