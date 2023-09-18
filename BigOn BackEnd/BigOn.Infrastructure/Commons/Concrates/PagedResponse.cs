using BigOn.Infrastructure.Commons.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Commons.Concrates
{
    internal class PagedResponse<T> : IPagedResponse<T>
        where T : class
    {
        public int Page { get; set; }
        public int Count { get; set; }
        public int Size { get; set; }

        public int Pages { get => (int)Math.Ceiling(this.Count * 1M / this.Size); }

        public bool HasNext { get => this.Page < this.Pages; }

        public bool HasPrevious { get => this.Page > 1; }

        public IEnumerable<T> Items { get; set; }

        public PagedResponse(int count, IPageable pageable)
        {
            this.Count = count;
            this.Size = pageable.Size;
            this.Page = pageable.Page > this.Pages ? this.Pages : pageable.Page;
        }
    }
}
