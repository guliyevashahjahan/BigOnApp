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
            var colors = db.Colors.ToList();
            return View(colors);
        }
        public IActionResult Create()
        {
           
            return View();
        }
        [HttpPost]
        public IActionResult Create(Color model)
        {
            model.CreatedAt = DateTime.Now;
            model.CreatedBy = 1;
            db.Colors.Add(model);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
