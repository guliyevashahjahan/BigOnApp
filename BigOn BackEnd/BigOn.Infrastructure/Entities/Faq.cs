using BigOn.Infrastructure.Commons.Concrates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Entities
{
    public class Faq : BaseEntity<int>
    {
        public string Question { get; set; }
        public string Answer { get; set; }

    }
}
