using AngleSharp.Css.Dom.Events;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BigOn_WebUI.ViewComponents
{
    public class BasketInfoViewComponent : ViewComponent
    {
        private readonly IMediator mediator;

        public BasketInfoViewComponent(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task<IViewComponentResult> InvokeAsync ()
        {
            return View();
        }
    }
}
