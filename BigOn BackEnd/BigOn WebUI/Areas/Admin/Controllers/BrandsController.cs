using BigOn_WebUI.Models.Entities;
using BigOn_WebUI.Models.Persistences;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace BigOn_WebUI.Areas.Admin.Controllers
{
        [Area("Admin")]
        public class BrandsController : Controller
        {
            private readonly DataContext db;

            public BrandsController(DataContext db)
            {
                this.db = db;
            }

            public IActionResult Index()
            {
                var brands = db.Brands.Where(m => m.DeletedBy == null).ToList();
                return View(brands);
            }
            public IActionResult Create()
            {

                return View();
            }
            [HttpPost]
            public IActionResult Create(Brand model)
            {
                db.Brands.Add(model);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            public IActionResult Details(int id)
            {
                var model = db.Brands.FirstOrDefault(m => m.Id == id);
                if (model == null)
                {
                    return NotFound();
                }

                return View(model);
            }
            public IActionResult Edit(int id)
            {
                var model = db.Brands.FirstOrDefault(m => m.Id == id);
                if (model == null)
                {
                    return NotFound();
                }
                return View(model);
            }

            [HttpPost]
            public IActionResult Edit(Brand model)
            {
                db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.Entry(model).Property(m => m.CreatedAt).IsModified = false;
                db.Entry(model).Property(m => m.CreatedBy).IsModified = false;
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            [HttpPost]
            public IActionResult Delete(int id)
            {
                var model = db.Brands.FirstOrDefault(m => m.Id == id && m.DeletedBy == null);
                if (model == null)
                {
                Response.Headers.Add("error", "true");
                string text = HttpUtility.UrlEncode("This item doesn't exist.");
                Response.Headers.Add("message", text);
                return Content("");
                    //return Json(new
                    //{
                    //    error = true,
                    //    message = "This item doesn't exist."

                    //});
                }
                db.Brands.Remove(model);
                db.SaveChanges(); 

            var brands = db.Brands.Where(m => m.DeletedBy == null).ToList();
            return PartialView("_Body",brands);
        }
        }
    }

