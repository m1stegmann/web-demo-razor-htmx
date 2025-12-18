using Microsoft.AspNetCore.Mvc;

namespace WebsiteDemo.ViewComponents;

public class FavoritesViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        string? loginCookie = Request.Cookies["login"];
        bool isLoggedIn = loginCookie == "true";

        return View(isLoggedIn);
    }
}
