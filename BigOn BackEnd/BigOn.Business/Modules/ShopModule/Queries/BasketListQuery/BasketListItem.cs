using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ShopModule.Queries.BasketListQuery
{
    public class BasketListItem
    {
        public int CatalogId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
