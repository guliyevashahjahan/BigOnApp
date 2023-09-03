using BigOn.Business.Modules.BrandsModule.Commands.BrandEditCommand;
using BigOn.Business.Modules.BrandsModule.Commands.BrandRemoveCommand;
using BigOn.Business.Modules.BrandsModule.Queries.BrandGetAllQuery;
using BigOn.Business.Modules.BrandsModule.Queries.BrandGetByIdQuery;
using BigOn.Data.Persistences;
using BigOn.Infrastructure.Entities;
using MediatR;
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

            public async Task<IActionResult> Index(BrandGetAllRequest request)
            {
            var brands =await mediator.Send(request);
                return View(brands);
            }
        public  IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult>  Create(BrandGetAllRequest request)
        {
         await  mediator.Send(request);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(BrandGetByIdRequest request)
        {
          var model = await  mediator.Send(request);
            return View(model);
        }
        public async Task<IActionResult>  Edit(BrandGetByIdRequest request)
        {
            var model = await mediator.Send(request);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(BrandEditRequest request)
        {
            await mediator.Send(request);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(BrandRemoveRequest request)
        {
          await  mediator.Send(request);
            //var model = db.Brands.FirstOrDefault(m => m.Id == id && m.DeletedBy == null);
            //if (model == null)
            //{
            //    Response.Headers.Add("error", "true");
            //    string text = HttpUtility.UrlEncode("This item doesn't exist.");
            //    Response.Headers.Add("message", text);
            //    return Content("");
            //    //return Json(new
            //    //{
            //    //    error = true,
            //    //    message = "This item doesn't exist."

            //    //});
            //}
            //db.Brands.Remove(model);
            //db.SaveChanges();

            // var brands = db.Brands.Where(m => m.DeletedBy == null).ToList();
            var newRequest =new BrandGetAllRequest();
           var brands =  await mediator.Send(newRequest);
          
            return PartialView("_Body", brands);
        }
    }
    }

