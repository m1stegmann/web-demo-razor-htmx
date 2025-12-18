using Microsoft.AspNetCore.Mvc;

namespace web_demo.ViewComponents;

public class TeaserViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        var teaser = ("Demo", "Dies ist ein Beispiel-Teaser.");
        return View(teaser);
    }
}
