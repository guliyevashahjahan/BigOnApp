using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.BlogPostModule.Commands.BlogPostAddComment
{
    public class BlogPostCommentDto
    {
        public int Id { get; set; }
        public string Comment { get; set; }
        public int PostId { get; set; }
        public int? ParentId { get; set; }
        public string Author { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
