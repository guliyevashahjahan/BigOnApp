using BigOn.Infrastructure.Commons.Concrates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Entities
{
    public class Material : BaseEntity<int>
    {
        public string Name { get; set; }
    }
}
