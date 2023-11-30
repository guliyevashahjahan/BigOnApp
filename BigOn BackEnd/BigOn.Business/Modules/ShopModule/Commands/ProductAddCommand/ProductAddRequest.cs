using BigOn.Business.Modules.ShopModule.Queries.ProductCatalogQuery;
using BigOn.Infrastructure.Commons.Concrates;
using BigOn.Infrastructure.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.ShopModule.Commands.ProductAddCommand
{
    public class ProductAddRequest : IRequest<Product>
    {
        public string Name { get; set; }
        public string StockKeepingUnit { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public ImageItem[] Images { get; set; }
        public ProductCatalog[] Catalog { get; set; }
    }
  
}
