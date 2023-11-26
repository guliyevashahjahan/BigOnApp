using BigOn.Infrastructure.Repositories;
using BigOn.Infrastructure.Services.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace BigOn.Business.Modules.ShopModule.Queries.BasketListQuery
{
    internal class BasketListRequestHandler : IRequestHandler<BasketListRequest, IEnumerable<BasketListItem>>
    {
        private readonly IProductRepository productRepository;
        private readonly IIdentityService identityService;
        private readonly ISizeRepository sizeRepository;
        private readonly IColorRepository colorRepository;
        private readonly IMaterialRepository materialRepository;

        public BasketListRequestHandler(IProductRepository productRepository,IIdentityService identityService,
            ISizeRepository sizeRepository,
            IColorRepository colorRepository,
            IMaterialRepository materialRepository)
        {
            this.productRepository = productRepository;
            this.identityService = identityService;
            this.sizeRepository = sizeRepository;
            this.colorRepository = colorRepository;
            this.materialRepository = materialRepository;
        }
        public async Task<IEnumerable<BasketListItem>> Handle(BasketListRequest request, CancellationToken cancellationToken)
        {
            var query = from b in productRepository.GetBasket(identityService.GetPrincipalId().Value)
                           join pc in productRepository.GetCatalog() on b.CatalogId equals pc.Id
                           join p in productRepository.GetAll() on pc.ProductId equals p.Id
                           join s in sizeRepository.GetAll() on pc.SizeId equals s.Id
                           join m in materialRepository.GetAll() on pc.MaterialId equals m.Id
                           join c in colorRepository.GetAll() on pc.ColorId equals c.Id
                           join img in productRepository.GetImages(m => m.IsMain) on p.Id equals img.ProductId
                           select new BasketListItem
                           {
                               CatalogId = pc.Id,
                               ProductId = p.Id,
                               Name = $"{p.Name} {s.ShortName} {m.Name} {c.Name}",
                               Quantity = b.Quantity,
                               ImagePath = img.Name,
                               Price = pc.Price == null ? p.Price : pc.Price.Value,
                           };

            return query.ToList();
        }
    }
}
