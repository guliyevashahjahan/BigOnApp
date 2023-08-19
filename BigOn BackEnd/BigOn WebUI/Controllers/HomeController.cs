using BigOn_WebUI.AppCode.Extensions;
using BigOn_WebUI.AppCode.Services;
using BigOn_WebUI.Models.Entities;
using BigOn_WebUI.Models.Persistences;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;
using System.Security.Policy;
using System.Text.RegularExpressions;
using static System.Net.WebRequestMethods;

namespace BigOn_WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext db;
        private readonly EmailService emailService;
        public HomeController(DataContext db, EmailService emailService )
        {
            this.db = db;
            this.emailService = emailService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Subscribe(string email)
        {
            if (!email.IsEmail())
            {
                return Json(new
                {
                    error = true,
                    message = $"'{email}' is not valid e-mail..."
                });
            }
          var subscriber = await db.Subscribers.FirstOrDefaultAsync(m=>m.Email.Equals(email));
            if (subscriber != null && subscriber.Approved)
            {
                return Json(new
                {
                    error = true,
                    message = "This e-mail addres is already in use."
                });
            }
            else if (subscriber != null && !subscriber.Approved)
            {
                return Json(new
                {
                    error=false,
                    message = "We sent you an email.Please, enter your e-mail account for confirmation."
                });
            }

            subscriber = new Subscriber();
            subscriber.Email = email;
            subscriber.CreatedAt = DateTime.Now;
          await  db.Subscribers.AddAsync(subscriber);
           await db.SaveChangesAsync();

            string url = "https://localhost:7238/home/subscribe-approve?token=test";
            string message = $"Salam zehmet olmasa klik ele <a href = \"{url}\" >link</a> for confirmation.";

          await  emailService.SendMailAsync(subscriber.Email,"BigOn Service",message);

            return Json(new
            {
                error=false,
                message = "We sent you an email.Please, enter your e-mail account for confirmation."
            });
        }
        public IActionResult SubscribeApprove()
        {
            return Content("You've successfully subscribed!");
        }
    }

}
