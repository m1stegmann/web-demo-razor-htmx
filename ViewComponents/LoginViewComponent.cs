using Microsoft.AspNetCore.Mvc;

namespace web_demo.ViewComponents;

public class LoginViewComponent : ViewComponent
{
    public IViewComponentResult Invoke()
    {
        string currentPath = Request.Path;
        string status = Request.Query["status"].ToString();

        // Determine status from query parameter or cookie
        bool isLoggedIn;
        if (!string.IsNullOrEmpty(status))
        {
            isLoggedIn = status == "logout";
            HttpContext.Response.Cookies.Append("login", isLoggedIn ? "true" : "false");
        }
        else
        {
            isLoggedIn = Request.Cookies["login"] == "true";
            if (!Request.Cookies.ContainsKey("login"))
            {
                HttpContext.Response.Cookies.Append("login", "false");
            }
        }

        string displayText = isLoggedIn ? "Logout" : "Login";
        string nextStatus = isLoggedIn ? "login" : "logout";

        // Custom header for HTMX event trigger
        HttpContext.Response.Headers.Append("HX-Trigger", isLoggedIn ? "userLoggedIn" : "userLoggedOut");

        var model = (displayText, nextStatus, currentPath);
        return View(model);
    }
}
