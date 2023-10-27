using AutoMapper;
using BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetAllQuery;
using BigOn.Infrastructure.Commons.Concrates;
using BigOn.WebApi.Models.DTOs;

namespace BigOn.WebApi.Mapping
{
    public class BlogPostProfile : Profile
    {
        public BlogPostProfile()
        {
            CreateMap<BlogPostGetAllDto, BlogPostDto>().
                ForMember(dest=>dest.Name,src=>src.MapFrom(m=>m.Title)).
                ForMember(dest=>dest.Image,src=>src.ConvertUsing(new ImageConverter(),m=>m.ImagePath)).
                ForMember(dest=>dest.PublishedAt,src=>src.ConvertUsing(new DateConverter(),m=>m.PublishedAt));
            

            CreateMap<PagedResponse<BlogPostGetAllDto>, PagedResponse<BlogPostDto>>();


        }
    }
}
