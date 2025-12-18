using Microsoft.AspNetCore.Mvc;

namespace WebsiteDemo.ViewComponents;

public class BasketViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
