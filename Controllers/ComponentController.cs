using Microsoft.AspNetCore.Mvc;

namespace WebsiteDemo.Controllers;

public class ComponentController : Controller
{
    [HttpGet("/component/teaser")]
    public IActionResult Teaser()
    {
        return ViewComponent("Teaser");
    }

    [HttpGet("/component/basket")]
    public IActionResult Basket()
    {
        return ViewComponent("Basket");
    }

    [HttpGet("/component/content")]
    public IActionResult Content()
    {
        return ViewComponent("Content");
    }

    [HttpGet("/component/favorites")]
    public IActionResult Favorites()
    {
        return ViewComponent("Favorites");
    }

    [HttpGet("/component/paidcontent")]
    public IActionResult PaidContent()
    {
        return ViewComponent("PaidContent");
    }

    [HttpGet("/component/oob")]
    public IActionResult OutOfBand()
    {
        return ViewComponent("OutOfBand");
    }

    [HttpGet("/component/login")]
    public IActionResult Login()
    {
        return ViewComponent("Login");
    }
}
