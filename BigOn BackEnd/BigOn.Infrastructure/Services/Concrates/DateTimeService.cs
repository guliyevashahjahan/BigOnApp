using BigOn.Infrastructure.Services.Abstracts;

namespace BigOn.Infrastructure.Services.Concrates
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime ExecutingTime
        {
            get
            {
                return DateTime.Now;
            }
        }
      
    }
    public class UtcDateTimeService : IDateTimeService
    {
        public DateTime ExecutingTime
        {
            get
            {
                return DateTime.UtcNow;
            }
        }

    }
}
