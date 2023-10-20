using AngleSharp.Io;
using BigOn.Data.Repositories;
using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Extensions;
using BigOn.Infrastructure.Repositories;
using BigOn.Infrastructure.Services.Abstracts;
using BigOn.Infrastructure.Services.Concrates;
using MediatR;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BigOn.Business.Modules.SubscribeModule.Commands.SubscribeTicketCommand
{
    internal class SubscribeTicketRequestHandler : IRequestHandler<SubscribeTicketRequest>
    {
        private readonly ISubscriberRepository subscriberRepository;
        private readonly IDateTimeService dateTimeService;
        private readonly IActionContextAccessor ctx;
        private readonly ICryptoService cryptoService;
        private readonly IEmailService emailService;

        public SubscribeTicketRequestHandler(ISubscriberRepository subscriberRepository,
            IDateTimeService dateTimeService,IActionContextAccessor ctx,
            ICryptoService cryptoService, IEmailService emailService)
        {
            this.subscriberRepository = subscriberRepository;
            this.dateTimeService = dateTimeService;
            this.ctx = ctx;
            this.cryptoService = cryptoService;
            this.emailService = emailService;
        }
        public async Task Handle(SubscribeTicketRequest request, CancellationToken cancellationToken)
        {
            if (!request.Email.IsEmail())
            {
                throw new Exception($"'{request.Email}' is not valid e-mail...");
            }
            var subscriber = subscriberRepository.Get(m => m.Email.Equals(request.Email));
            if (subscriber != null && subscriber.Approved)
            {
                throw new Exception("This e-mail addres is already in use.");
            }
            else if (subscriber != null && !subscriber.Approved)
            {
                throw new Exception("We sent you an email.Please, enter your e-mail account for confirmation.");
            }

            subscriber = new Subscriber();
            subscriber.Email = request.Email;
            subscriber.CreatedAt = dateTimeService.ExecutingTime;
            subscriberRepository.Add(subscriber);
            subscriberRepository.Save();

            string token = cryptoService.Encrypt($"{subscriber.Email}-{subscriber.CreatedAt:yyyy-MM-dd HH:mm:ss.fff}-bigon");

            string url = $"{ctx.ActionContext.HttpContext.Request.Scheme}://{ctx.ActionContext.HttpContext.Request.Host}/subscribe-approve.html?token={token}";
            string message = $"Click the <a href = \"{url}\" >link</a> for confirmation.";

            await emailService.SendMailAsync(subscriber.Email, "BigOn Service", message);

        }
    }
}
