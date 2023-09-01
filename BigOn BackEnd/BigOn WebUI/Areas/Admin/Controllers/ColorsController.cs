using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BigOn_WebUI.Models.Persistences;
using BigOn_WebUI.Models.Entities;

namespace BigOn_WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ColorsController : Controller
    {
        private readonly DataContext db;

        public ColorsController(DataContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var colors = db.Colors.Where(m=>m.DeletedBy == null).ToList();
            return View(colors);
        }
        public IActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult Create(Color model)
        {
            db.Colors.Add(model);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int id)
        {
            var model = db.Colors.FirstOrDefault(m=>m.Id==id);
            if (model == null)
            {
                return NotFound();
            }

            return View(model);
        }
        public IActionResult Edit(int id)
        {
            var model = db.Colors.FirstOrDefault(m => m.Id == id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Color model)
        {
            db.Entry(model).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.Entry(model).Property(m => m.CreatedAt).IsModified = false;
            db.Entry(model).Property(m=> m.CreatedBy).IsModified = false;
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
          var model =  db.Colors.FirstOrDefault(m => m.Id == id && m.DeletedBy ==null);
            if (model == null)
            {
                return Json(new
                {
                   error=true,
                   message = "This item doesn't exist."

                });
            }
            db.Colors.Remove(model);
            db.SaveChanges();
            return Json(new
            {
                error = false,
                message = "Item deleted successfully"

            });
        }
    }
}
