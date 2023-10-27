using AngleSharp.Css;
using AutoMapper;
using BigOn.Business.Modules.BlogPostModule.Queries.BlogPostGetAllQuery;
using BigOn.WebApi.Models.DTOs;

namespace BigOn.WebApi.Mapping
{
    public class ImageConverter : IValueConverter<string, string>
    {
        public string Convert(string sourceMember, ResolutionContext context)
        {
            var host = context.Items["host"];
            return $"{host}/files/images/{sourceMember}";
        }
    }
}
