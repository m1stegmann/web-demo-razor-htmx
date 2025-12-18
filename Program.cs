using Microsoft.AspNetCore.Mvc.Razor;
using WebsiteDemo.Infrastructure.Razor;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.Configure<RazorViewEngineOptions>(o =>
{
    o.ViewLocationExpanders.Add(new ViewComponentLocationExpander());
});

var app = builder.Build();
app.UseStaticFiles();
app.MapRazorPages();
app.MapControllers();
app.Run();