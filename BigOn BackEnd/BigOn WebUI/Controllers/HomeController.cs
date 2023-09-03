using BigOn.Data.Persistences;
using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Extensions;
using BigOn.Infrastructure.Repositories;
using BigOn.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;


namespace BigOn_WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISubscriberRepository subscriberRepository;
        private readonly IEmailService emailService;
        public HomeController(ISubscriberRepository subscriberRepository, IEmailService emailService )
        {
            this.subscriberRepository = subscriberRepository;
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
          var subscriber  = subscriberRepository.Get(m=>m.Email.Equals(email));
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
            subscriberRepository.Add(subscriber);
            subscriberRepository.Save();

            string token = $"#demo-{subscriber.Email}-{subscriber.CreatedAt:yyyy-MM-dd HH:mm:ss.fff}-bigon";

            token = HttpUtility.UrlEncode(token);
            string url = $"{Request.Scheme}://{Request.Host}/subscribe-approve?token={token}";
            string message = $"Click the <a href = \"{url}\" >link</a> for confirmation.";

          await  emailService.SendMailAsync(subscriber.Email,"BigOn Service",message);

            return Json(new
            {
                error=false,  
                message = "Check your e-mail account for confirmation."
            });
        }

        [Route("/subscribe-approve")]
        public async Task<IActionResult>SubscribeApprove(string token)
        {
            string pattern = @"#demo-(?<email>[^-]*)-(?<date>\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}.\d{3})-bigon";
         Match match=  Regex.Match(token,pattern);
            if (!match.Success)
            {
                return Content("Token is damaged");
            }
            string email = match.Groups["email"].Value;
            string dateStr = match.Groups["date"].Value;

            if (!DateTime.TryParseExact(dateStr, "yyyy-MM-dd HH:mm:ss.fff",null,DateTimeStyles.None,out DateTime date))
            {
                return Content("Token is damaged-email");
            }

            var subscriber = subscriberRepository.Get(m => m.Email.Equals(email) && m.CreatedAt == date);

            if (subscriber == null)
            {
                return Content("Token is damaged-date");
            }

            if (!subscriber.Approved)
            {
                subscriber.Approved = true;
                subscriber.ApprovedAt = DateTime.Now;
            }
            subscriberRepository.Save();

            return Content($"Success \n Email: {email} \n Date: {date}");
        }
    }

}
