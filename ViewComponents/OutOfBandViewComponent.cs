using Microsoft.AspNetCore.Mvc;

namespace WebsiteDemo.ViewComponents;

public class OutOfBandViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
