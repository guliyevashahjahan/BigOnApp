using BigOn.Infrastructure.Commons.Concrates;

namespace BigOn.Infrastructure.Entities
{
    public class Size : BaseEntity <int>
    {
        public string Name { get; set; }
        public string ShortName { get; set; }


    }
}
