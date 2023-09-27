using BigOn.Infrastructure.Commons.Concrates;
using BigOn.Infrastructure.Entities;
using BigOn.Infrastructure.Repositories;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Data.Repositories
{
    internal class BlogPostRepository : GeneralRepository<BlogPost>, IBlogPostRepository
    {
        public BlogPostRepository(DbContext db) : base(db)
        {
        }

        BlogPostTag AddTag(int blogPostId, string tag)
        {
            var tagsTable = db.Set<Tag>();
            var blogPostTagsTable = db.Set<BlogPostTag>();
            var tagEntity = tagsTable.FirstOrDefault(m => m.Text.Equals(tag));
            if (tagEntity == null)
            {
                tagEntity = new Tag
                {
                    Text = tag
                };
                tagsTable.Add(tagEntity);
                db.SaveChanges();
            }
            var blogPostTag = blogPostTagsTable.Where(m => m.BlogPostId == blogPostId && m.TagId == tagEntity.Id).FirstOrDefault();
            if (blogPostTag == null)
            {
                blogPostTag = new BlogPostTag
                {
                    TagId = tagEntity.Id,
                    BlogPostId = blogPostId
                };
                blogPostTagsTable.Add(blogPostTag);
            }

            return blogPostTag;
        }

        public void ResolveTags(BlogPost blogPost, string[] tags)
        {
            if (tags == null || tags.Length ==0)
            {
                return;
            }
            var tagsTable = db.Set<Tag>();
            var blogPostTagsTable = db.Set<BlogPostTag>();
            var assignedTagsQuery = from bpt in blogPostTagsTable
                                    join t in tagsTable on bpt.TagId equals t.Id
                                    where bpt.BlogPostId == blogPost.Id
                                    select new
                                    {
                                       TagsId = bpt.TagId,
                                       BlogPostId = bpt.BlogPostId,
                                       text = t.Text,
                                       BlogPostTag = bpt
                                    };
            var forDeletion = assignedTagsQuery.Where(m=>!tags.Contains(m.text)).Select(m=>m.BlogPostTag).ToList();
            blogPostTagsTable.RemoveRange(forDeletion);
            var forInsertion = tags.Except(assignedTagsQuery.Select(m=>m.text)).ToList();
            foreach (var tag in forInsertion)
            {
                AddTag(blogPost.Id, tag);
            }
        }

        public IQueryable<Tag> GetTagsByBlogPostId(int blogPostId)
        {
            var query = from bpt in db.Set<BlogPostTag>()
                        join t in db.Set<Tag>() on bpt.TagId equals t.Id
                        where bpt.BlogPostId == blogPostId
                        select t;
            return query;
        }

        public IQueryable<Tag> GetUsedTags()
        {
            var query = (from bpt in db.Set<BlogPostTag>()
                        join t in db.Set<Tag>() on bpt.TagId equals t.Id
                        select t).Distinct();
            return query;
        }

        public BlogPostComment AddComment(int postId, int? parentId, string comment)
        {
            var commentsTable = db.Set<BlogPostComment>();
            var commentEntity = new BlogPostComment
            {
                PostId = postId,
                ParentId = parentId,
                Comment = comment
            };
            commentsTable.Add(commentEntity);

            return commentEntity;

        }

        public int CommentCounts(int postId)
        {
           return db.Set<BlogPostComment>().Count(m=>m.PostId == postId && m.DeletedBy == null);
        }
    }
}
