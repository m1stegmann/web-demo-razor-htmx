using Microsoft.AspNetCore.Mvc;

namespace web_demo.ViewComponents;

public class PaidContentViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        string? loginCookie = Request.Cookies["login"];
        bool isLoggedIn = loginCookie == "true";

        return View(isLoggedIn);
    }
}
