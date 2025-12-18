# ASP.NET Core + HTMX + ViewComponents Demo

This repository demonstrates **ASP.NET Core ViewComponents** with **HTMX** for building dynamic,
interactive web applications using server-side rendering and minimal JavaScript.

## Concept Overview

### ViewComponent Architecture

- **ViewComponents** are reusable C# classes in `ViewComponents/` that render HTML fragments
- Each component has a `XxxViewComponent.cs` class and corresponding `Xxx.cshtml` view
- ViewComponents are served via `ComponentController` at `/component/{name}` endpoints
- Components return pure HTML fragments (no layout) for HTMX to swap into the page

### HTMX Integration

- Main Razor Pages (`index.cshtml`, `testpage.cshtml`) use HTMX to dynamically load components
- Components are fetched with `hx-get="/component/{name}"` attributes
- Supports triggers: `load` (initial fetch), `every Xs` (polling), custom events (reactive updates)

### Event-Driven Communication

- ViewComponents dispatch custom DOM events via `HX-Trigger` response headers
- Example: `LoginViewComponent` triggers `userLoggedIn`/`userLoggedOut` events
- Other components listen via `hx-trigger="userLoggedIn from:body"` for reactive updates
- Multiple components can react to the same event simultaneously

### State Management

- Login state stored in `login` cookie (`"true"` or `"false"`)
- Set by `LoginViewComponent` via `HttpContext.Response.Cookies.Append()`
- Read by protected components (`FavoritesViewComponent`, `PaidContentViewComponent`)

### Out-of-Band Swaps

- ViewComponents can update multiple DOM areas in one request using `hx-swap-oob="innerHTML"`
- See `OutOfBandViewComponent` for example of multi-target updates

## Running the Demo

```bash
dotnet watch run  # Auto-reload on file changes (port 5274)
```

Or use VS Code tasks: **build**, **watch**, or **publish**

## Key Files

- `Controllers/ComponentController.cs` - HTTP endpoints that return ViewComponents
- `ViewComponents/{Name}/{Name}ViewComponent.cs` - Component logic (C# classes)
- `ViewComponents/{Name}/{Name}.cshtml` - Component views (Razor templates)
- `Infrastructure/Razor/ViewComponentLocationExpander.cs` - Custom view resolution
- `Pages/Shared/_Layout.cshtml` - Global HTMX + Tailwind setup
- `Pages/testpage.cshtml` - Example page with HTMX-loaded components
- `Program.cs` - App configuration (RazorPages + Controllers + ViewComponent location expansion)

## Why This Pattern?

- **Server-side rendering**: No large JavaScript framework needed
- **Reusable components**: ViewComponents are testable, type-safe C# classes
- **Progressive enhancement**: Pages work without JavaScript, enhanced with HTMX
- **Clear separation**: Controllers handle routing, ViewComponents handle rendering
- **Minimal client code**: HTMX handles all dynamic behavior declaratively

## Project Structure

```
ViewComponents/          # Reusable UI fragments
  Login/
    LoginViewComponent.cs
    Login.cshtml
  Favorites/
    FavoritesViewComponent.cs
    Favorites.cshtml
Controllers/             # HTTP endpoints for components
  ComponentController.cs
Pages/                   # Main Razor Pages
  index.cshtml
  testpage.cshtml
  Shared/_Layout.cshtml
Infrastructure/Razor/    # Custom view resolution
wwwroot/js/              # HTMX & Tailwind (local files)
```

## License

This demo is provided as-is for educational purposes.
