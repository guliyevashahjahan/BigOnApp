using BigOn.Infrastructure.Commons.Concrates;

namespace BigOn.Infrastructure.Entities
{
    public class Color : BaseEntity<int>
    {
        public string Name { get; set; }
        public string HexCode { get; set; }
    }
}
