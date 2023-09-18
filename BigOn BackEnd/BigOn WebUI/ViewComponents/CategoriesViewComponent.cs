using BigOn.Business.Modules.CategoryModule.Queries.CategoryGetAllQuery;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BigOn_WebUI.ViewComponents
{
    public class CategoriesViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public CategoriesViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var response = await mediator.Send(new CategoryGetAllRequest());
            return View(response);  
        }
    }
}
