using Microsoft.AspNetCore.Mvc;

namespace WebsiteDemo.ViewComponents;

public class TeaserViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var teaser = ("Demo", "Dies ist ein Beispiel-Teaser.");
        return View(teaser);
    }
}
