using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ShopModule.Queries.ProductGetAllQuery
{
    public class ProductGetAllDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public decimal Price { get; set; }

        public double Rate { get; set; }
        public string StockKeepingUnit { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
