using BigOn_WebUI.Models.Entities.Commons;

namespace BigOn_WebUI.Models.Entities
{
    public class Size : BaseEntity <int>
    {
        public string Name { get; set; }
        public string ShortName { get; set; }


    }
}
