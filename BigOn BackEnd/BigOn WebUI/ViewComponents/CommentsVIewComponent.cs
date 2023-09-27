using Microsoft.AspNetCore.Mvc;

namespace BigOn_WebUI.ViewComponents
{
    public class CommentsVIewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsycn(int postId)
        {
            return View();
        }
    }
}
