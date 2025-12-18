namespace WebsiteDemo.Infrastructure.Razor;

using Microsoft.AspNetCore.Mvc.Razor;

sealed class ViewComponentLocationExpander : IViewLocationExpander
{
    public void PopulateValues(ViewLocationExpanderContext context) { }

    public IEnumerable<string> ExpandViewLocations(
        ViewLocationExpanderContext context,
        IEnumerable<string> viewLocations)
    {
        // ViewComponents are searched for as "Components/{Component}/{View}"
        if (context.ViewName?.StartsWith("Components/", StringComparison.OrdinalIgnoreCase) == true)
        {
            var parts = context.ViewName.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
            {
                var componentName = parts[1]; // z.B. "Teaser"
                return new[] {
                    $"/ViewComponents/{componentName}View.cshtml",
                    $"/ViewComponents/{componentName}.cshtml",
                    $"/ViewComponents/{componentName}/{componentName}View.cshtml",
                    $"/ViewComponents/{componentName}/{componentName}.cshtml",
                    $"/ViewComponents/{componentName}/Default.cshtml",
                    }.Concat(viewLocations);
            }
        }

        return viewLocations;
    }
}