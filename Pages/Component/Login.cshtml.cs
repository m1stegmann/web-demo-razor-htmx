using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web_demo.Pages.Component
{
    public class LoginModel : PageModel
    {
        public string DisplayText { get; set; } = "Login";
        public string NextStatus { get; set; } = "logout";
        public string CurrentPath { get; set; } = "";

        public void OnGet()
        {
            CurrentPath = Request.Path;
            var status = Request.Query["status"].ToString();
            DisplayText = status == "logout" ? "Logout" : "Login";
            NextStatus = status == "logout" ? "login" : "logout";

            // Cookie setzen
            var isLoggedIn = status == "logout";
            Response.Cookies.Append("login", isLoggedIn ? "true" : "false");

            // Event auslösen, damit Content.cshtml neu lädt
            Response.Headers.Append("HX-Trigger", "loginStatusChanged");
        }
    }
}
