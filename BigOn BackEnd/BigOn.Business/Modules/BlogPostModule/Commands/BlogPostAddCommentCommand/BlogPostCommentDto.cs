

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
