using BigOn.Infrastructure.Commons.Abstracts;
using BigOn.Infrastructure.Commons.Concrates;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Extensions
{
    public static partial class Extension
    {
        static public IPagedResponse<T> ToPaging<T, TKey>(this IQueryable<T> query, IPageable pageable, Expression<Func<T, TKey>> orderExpression)
            where T : class
        {
            var count = query.Count();
            if (orderExpression != null)
            {
                query = query.OrderByDescending(orderExpression);
            }
            int maxPage = (int)Math.Ceiling((count * 1D / pageable.Size));
            if (pageable.Page < maxPage)
            {
               maxPage = pageable.Page;
            }
            var items = query.Skip((maxPage - 1) * pageable.Size).Take(pageable.Size);

            return new PagedResponse<T>(items, count, pageable);
        }


        static public IHtmlContent ToPager<T>(this IUrlHelper urlHelper, IPagedResponse<T> context, string area = "", string controller = "", string action = "index")
            where T : class
        {
            int maxNumberButton = 10;
            var sb = new StringBuilder();

            sb.Append("<nav class='pagination'><ul class='list-inline'>");

            if (context.HasPrevious)
            {
                var link = urlHelper.Action(new UrlActionContext
                {
                    Controller = controller,
                    Action = action,
                    Values = new { area = area, page = context.Page - 1, size = context.Size }
                });
                sb.Append(@$" <li class='prev'>
             <a href='{link}'>
                 <span><i class='fa fa-angle-left' aria-hidden='true'></i></span>
             </a>
         </li>");

            }
            else
            {
                sb.Append(@$" <li class='disabled prev'>
                 <span><i class='fa fa-angle-left' aria-hidden='true'></i></span>
         </li>");

            }

            int min = 1;
            int max = context.Pages;

            if (context.Page > (int)Math.Floor(maxNumberButton / 2D))
            {
                min = context.Page - (int)Math.Floor(maxNumberButton / 2D);
                max = min + maxNumberButton - 1;
            }

            if (max> context.Page)
            {
                max = context.Pages;
                min = max - maxNumberButton + 1;
            }
            for (int i = (min < 1 ? 1 : min); i <= max; i++)
            {
                sb.Append(@$"<li {(context.Page == i ? "class='active'" : "")}>");
                if (context.Page == i)
                {
                    sb.Append(i);
                }
                else
                {
                    var link = urlHelper.Action(new UrlActionContext
                    {
                        Controller = controller,
                        Action = action,
                        Values = new { area = area, page = i, size = context.Size }
                    });
                    sb.Append(@$"<a href='{link}'>{i}</a>");
                }
                sb.Append("</li>");
            }

            if (context.HasNext)
            {
                var link = urlHelper.Action(new UrlActionContext
                {
                    Controller = controller,
                    Action = action,
                    Values = new { area = area, page = context.Page + 1, size = context.Size }
                });
                sb.Append(@$"<li class='next'><a href='{link}' title='Next'>
                   <span aria-hidden='true'><i class='fa fa-angle-right' aria-hidden='true'></i>
                    </span></a></li>");

            }
            else
            {
                sb.Append(@$"<li class=' next'>
                   <span aria-hidden='true'><i class='fa fa-angle-right' aria-hidden='true'></i>
                    </span></li>");

            }

            sb.Append("</ul></nav>");
            return new HtmlString(sb.ToString());
        }


    }
}
