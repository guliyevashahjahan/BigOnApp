using BigOn_WebUI.Models.Entities.Commons;

namespace BigOn_WebUI.Models.Entities
{
    public class Color : BaseEntity<int>
    {
        public string Name { get; set; }
        public string HexCode { get; set; }
    }
}
