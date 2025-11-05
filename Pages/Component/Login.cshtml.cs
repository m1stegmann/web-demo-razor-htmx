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
            string status = Request.Query["status"].ToString();

            // Determine status from query parameter or cookie
            bool isLoggedIn;
            if (!string.IsNullOrEmpty(status))
            {
                isLoggedIn = status == "logout";
                Response.Cookies.Append("login", isLoggedIn ? "true" : "false");
            }
            else
            {
                isLoggedIn = Request.Cookies["login"] == "true";
                if (!Request.Cookies.ContainsKey("login"))
                {
                    Response.Cookies.Append("login", "false");
                }
            }

            DisplayText = isLoggedIn ? "Logout" : "Login";
            NextStatus = isLoggedIn ? "login" : "logout";

            // Custom header for HTMX event trigger
            Response.Headers.Append("HX-Trigger", isLoggedIn ? "userLoggedIn" : "userLoggedOut");
        }
    }
}
