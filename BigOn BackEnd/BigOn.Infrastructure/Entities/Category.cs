using BigOn.Infrastructure.Commons.Concrates;

namespace BigOn.Infrastructure.Entities
{
    public class Category :BaseEntity<int>
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
