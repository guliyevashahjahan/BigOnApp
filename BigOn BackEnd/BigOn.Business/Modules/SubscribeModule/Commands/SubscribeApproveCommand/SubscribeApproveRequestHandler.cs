using BigOn.Data.Repositories;
using BigOn.Infrastructure.Repositories;
using BigOn.Infrastructure.Services.Abstracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.SubscribeModule.Commands.SubscribeApproveCommand
{
    internal class SubscribeApproveRequestHandler : IRequestHandler<SubscribeApproveRequest>
    {
        private readonly ICryptoService cryptoService;
        private readonly IDateTimeService dateTimeService;
        private readonly ISubscriberRepository subscriberRepository;

        public SubscribeApproveRequestHandler(ICryptoService cryptoService,IDateTimeService dateTimeService,
            ISubscriberRepository subscriberRepository)
        {
            this.cryptoService = cryptoService;
            this.dateTimeService = dateTimeService;
            this.subscriberRepository = subscriberRepository;
        }
        public async Task Handle(SubscribeApproveRequest request, CancellationToken cancellationToken)
        {
            request.Token = cryptoService.Decrypt(request.Token);

            string pattern = @"(?<email>[^-]*)-(?<date>\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}.\d{3})-bigon";
            Match match = Regex.Match(request.Token, pattern);
            if (!match.Success)
            {
                throw new Exception("Token is damaged");
            }
            string email = match.Groups["email"].Value;
            string dateStr = match.Groups["date"].Value;

            if (!DateTime.TryParseExact(dateStr, "yyyy-MM-dd HH:mm:ss.fff", null, DateTimeStyles.None, out DateTime date))
            {
                throw new Exception("Token is damaged");
            }

            var subscriber = subscriberRepository.Get(m => m.Email.Equals(email) && m.CreatedAt == date);

            if (subscriber == null)
            {
                throw new Exception("Token is damaged");
            }

            if (!subscriber.Approved)
            {
                subscriber.Approved = true;
                subscriber.ApprovedAt = dateTimeService.ExecutingTime;
            }
            subscriberRepository.Save();
        }
    }
}
