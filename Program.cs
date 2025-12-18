var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorPages();
builder.Services.AddControllers();

var app = builder.Build();
app.UseStaticFiles();
app.MapRazorPages();
app.MapControllers();
app.Run();