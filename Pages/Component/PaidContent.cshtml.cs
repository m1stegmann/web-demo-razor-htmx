using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web_demo.Pages.Component
{
    public class PaidContentModel : PageModel
    {
        public bool IsLoggedIn { get; set; }

        public void OnGet()
        {
            var loginCookie = Request.Cookies["login"];
            IsLoggedIn = loginCookie == "true";
        }
    }
}
