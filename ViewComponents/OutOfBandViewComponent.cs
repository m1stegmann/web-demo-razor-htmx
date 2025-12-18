using Microsoft.AspNetCore.Mvc;

namespace web_demo.ViewComponents;

public class OutOfBandViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        return View();
    }
}
