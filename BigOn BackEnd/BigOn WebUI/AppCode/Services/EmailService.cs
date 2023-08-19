using BigOn_WebUI.Models.Entities;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace BigOn_WebUI.AppCode.Services
{
    public class EmailService
    {
       readonly EmailOptions options;

        public EmailService(IOptions<EmailOptions> options)
        {
            this.options = options.Value;
        }

        public async Task <bool> SendMailAsync (string to, string subject, string body)
        {
            try
            {
                using (SmtpClient client = new SmtpClient(options.SmtpServer, options.SmtpPort))
                using (MailMessage message = new MailMessage())
                {

                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(options.UserEmail, options.Password);
                    message.Subject = subject;
                    message.Body = body;
                    message.IsBodyHtml = true;
                    message.To.Add(to);
                    message.From = new MailAddress(options.UserEmail, options.DisplayName);

                    await client.SendMailAsync(message);

                }

            }
            catch (Exception)
            {

                return false;
            }
            return true;
        }
    }
}
