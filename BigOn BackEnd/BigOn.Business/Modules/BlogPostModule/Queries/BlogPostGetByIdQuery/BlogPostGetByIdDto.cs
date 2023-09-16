
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetByIdQuery
{
    public class BlogPostGetByIdDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string ImagePath { get; set; }
        public string CategoryName { get; set; }
        public string Body { get; set; }
        public int CategoryId { get; set; }
        public DateTime? PublishedAt { get; set; }
        public string[] Tags { get; set; }

    }
}
