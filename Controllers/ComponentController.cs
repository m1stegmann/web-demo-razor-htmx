using Microsoft.AspNetCore.Mvc;

namespace web_demo.Controllers;

public class ComponentController : Controller
{
    [HttpGet("/component/teaser-vc")]
    public IActionResult Teaser()
    {
        return ViewComponent("Teaser");
    }
}
