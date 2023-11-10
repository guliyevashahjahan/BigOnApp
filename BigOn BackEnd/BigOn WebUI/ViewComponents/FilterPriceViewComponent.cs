using BigOn.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace BigOn_WebUI.ViewComponents
{
    public class FilterPriceViewComponent : ViewComponent
    {
        private readonly IProductRepository productRepository;

        public FilterPriceViewComponent(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var priceInfo = productRepository.GetAll(m => m.DeletedBy == null)
                .Select(m => new
                {
                    Demo = 1,
                    Price = m.Price
                }).GroupBy(m => m.Demo).Select(m => new
                {
                    Min = m.Min(x => x.Price),
                    Max = m.Max(x => x.Price)
                }).FirstOrDefault();


            if (priceInfo is null || (priceInfo.Min == 0 && priceInfo.Max == 0))
            {
                goto l1;
            }
            ViewBag.PriceInfo = new
            {
                Min =(int)Math.Floor(priceInfo.Min), 
                Max = (int)Math.Ceiling(priceInfo.Max)
            };
            l1:   return View();
        }
    }
}
