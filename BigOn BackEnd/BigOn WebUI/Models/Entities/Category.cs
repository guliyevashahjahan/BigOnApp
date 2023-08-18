using BigOn_WebUI.Models.Entities.Commons;

namespace BigOn_WebUI.Models.Entities
{
    public class Category :BaseEntity<int>
    {
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
}
