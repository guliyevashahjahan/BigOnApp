using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Commons.Abstracts
{
    public interface IPagedResponse<T>
        where T : class
    {
        public int Page { get; set; }
        public int Count { get; set; }
        public int Size { get; set; }
        public int Pages { get; }
        public bool HasNext { get; }
        public bool HasPrevious { get; }
        public IEnumerable<T> Items { get; set; }
    }
}
