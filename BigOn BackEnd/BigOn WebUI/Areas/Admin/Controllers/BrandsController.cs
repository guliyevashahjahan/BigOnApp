using BigOn.Business.Modules.BrandsModule.Commands.BrandEditCommand;
using BigOn.Business.Modules.BrandsModule.Commands.BrandRemoveCommand;
using BigOn.Business.Modules.BrandsModule.Queries.BrandGetAllQuery;
using BigOn.Business.Modules.BrandsModule.Queries.BrandGetByIdQuery;
using BigOn.Data.Persistences;
using BigOn.Infrastructure.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace BigOn_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
        public class BrandsController : Controller
        {
        private readonly IMediator mediator;

        public BrandsController(IMediator mediator)
            {
            this.mediator = mediator;
        }

        [Authorize("admin.brands.index")]
            public async Task<IActionResult> Index(BrandGetAllRequest request)
            {
            var brands =await mediator.Send(request);
                return View(brands);
            }
        [Authorize("admin.brands.create")]
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize("admin.brands.create")]

        public async Task<IActionResult>  Create(BrandGetAllRequest request)
        {
         await  mediator.Send(request);
            return RedirectToAction(nameof(Index));
        }
        [Authorize("admin.brands.details")]

        public async Task<IActionResult> Details(BrandGetByIdRequest request)
        {
          var model = await  mediator.Send(request);
            return View(model);
        }
        [Authorize("admin.brands.edit")]

        public async Task<IActionResult>  Edit(BrandGetByIdRequest request)
        {
            var model = await mediator.Send(request);
            return View(model);
        }

        [HttpPost]
        [Authorize("admin.brands.edit")]

        public async Task<IActionResult> Edit(BrandEditRequest request)
        {
            await mediator.Send(request);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [Authorize("admin.brands.delete")]

        public async Task<IActionResult> Delete(BrandRemoveRequest request)
        {
          await  mediator.Send(request);
         
            var newRequest =new BrandGetAllRequest();
           var brands =  await mediator.Send(newRequest);
          
            return PartialView("_Body", brands);
        }
    }
    }

