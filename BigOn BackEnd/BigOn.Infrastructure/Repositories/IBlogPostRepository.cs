using BigOn.Infrastructure.Commons.Abstracts;
using BigOn.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Repositories
{
    public interface IBlogPostRepository : IRepository<BlogPost>
    {
        void ResolveTags(BlogPost blogPost, string[] tags);
        IQueryable<Tag> GetTagsByBlogPostId(int blogPostId);

        IQueryable<Tag> GetUsedTags();

        BlogPostComment AddComment(int postId, int? parentId, string comment);

        int CommentCounts(int postId);

        IQueryable<BlogPostComment> GetComments(int postId);

    }
}
