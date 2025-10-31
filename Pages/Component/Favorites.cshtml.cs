using Microsoft.AspNetCore.Mvc.RazorPages;

namespace web_demo.Pages.Component
{
    public class FavoritesModel : PageModel
    {
        public bool IsLoggedIn { get; set; }

        public void OnGet()
        {
            string? loginCookie = Request.Cookies["login"];
            IsLoggedIn = loginCookie == "true";
        }
    }
}
