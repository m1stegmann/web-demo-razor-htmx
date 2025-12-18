namespace web_demo;

using Microsoft.AspNetCore.Mvc.Razor;

sealed class FlatViewComponentLocationExpander : IViewLocationExpander
{
    public void PopulateValues(ViewLocationExpanderContext context) { }

    public IEnumerable<string> ExpandViewLocations(
        ViewLocationExpanderContext context,
        IEnumerable<string> viewLocations)
    {
        // ViewComponents werden als "Components/{Component}/{View}" gesucht
        if (context.ViewName?.StartsWith("Components/", StringComparison.OrdinalIgnoreCase) == true)
        {
            var parts = context.ViewName.Split('/', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length >= 2)
            {
                var componentName = parts[1]; // z.B. "Teaser"
                return new[] { $"/Components/{componentName}.cshtml" }.Concat(viewLocations);
            }
        }

        return viewLocations;
    }
}