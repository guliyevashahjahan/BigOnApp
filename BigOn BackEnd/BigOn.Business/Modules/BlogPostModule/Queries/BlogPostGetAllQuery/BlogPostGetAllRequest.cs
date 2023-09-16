
using BigOn.Infrastructure.Commons.Abstracts;
using BigOn.Infrastructure.Commons.Concrates;
using MediatR;

namespace BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetAllQuery
{
    public class BlogPostGetAllRequest : Pageable, IRequest<IPagedResponse<BlogPostGetAllDto>>
    {
        public override int Size
        {
            get
            {
                return base.Size < 12 ? 12 : base.Size;
            }
            set
            {
                base.Size = value < 12 ? 12 : value;
            }
        }
    }
}
