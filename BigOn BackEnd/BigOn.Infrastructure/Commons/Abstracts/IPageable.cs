using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Commons.Abstracts
{
    public interface IPageable
    {
        public int Page { get; set; }
        public int Size { get; set; }
    }
}
