using Microsoft.AspNetCore.Mvc;

namespace web_demo.ViewComponents;

public class BasketViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
