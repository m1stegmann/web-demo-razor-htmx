# Copilot Instructions for web-demo

## Project Overview

This is an **ASP.NET Core 10.0 Razor Pages** demo application showcasing **HTMX** integration patterns with **ViewComponents**. The app demonstrates dynamic content loading using ViewComponents as HTMX-loadable fragments, custom event-driven architecture, and out-of-band (OOB) swaps using HTMX with Tailwind CSS for styling.

## Architecture Patterns

### ViewComponent-Based Architecture

The application uses **ASP.NET Core ViewComponents** as reusable UI fragments loaded dynamically via HTMX:

- **ViewComponents** live in `ViewComponents/` folder, organized by component name (e.g., `ViewComponents/Login/`)
- Each ViewComponent consists of:
  - `XxxViewComponent.cs` - C# logic class inheriting from `ViewComponent`
  - `Xxx.cshtml` - Razor view template (located via custom `ViewComponentLocationExpander`)
- ViewComponents are namespace-organized under `WebsiteDemo.ViewComponents`

### ComponentController Pattern

The `ComponentController` class provides HTTP endpoints that return ViewComponents as HTML fragments:

```csharp
// Controllers/ComponentController.cs
[HttpGet("/component/login")]
public IActionResult Login() {
    return ViewComponent("Login");
}
```

- All component endpoints follow the pattern: `/component/{name}` (lowercase)
- Each endpoint invokes `ViewComponent()` to render the corresponding ViewComponent
- ViewComponents are referenced by their class name (without "ViewComponent" suffix)
- Returns raw HTML fragments (no layout) suitable for HTMX swapping

### HTMX Integration

Main Razor Pages (e.g., `testpage.cshtml`) load ViewComponents dynamically via HTMX:

```html
<div hx-get="/component/content" hx-trigger="load, userLoggedIn from:body">
  <!-- Placeholder content -->
</div>
```

- Use `hx-get="/component/{name}"` to fetch ViewComponent HTML
- Triggers: `load` (on page load), `every Xs` (polling), custom events
- Components listen to custom DOM events (see below) for reactive updates

### Custom Event-Driven Communication

The app uses **custom DOM events** dispatched via HTMX headers for cross-component communication:

```csharp
// LoginViewComponent.cs
HttpContext.Response.Headers.Append("HX-Trigger",
    isLoggedIn ? "userLoggedIn" : "userLoggedOut");
```

ViewComponents can trigger events via the `HX-Trigger` response header. Other components listen via:

```html
<div
  hx-get="/component/favorites"
  hx-trigger="userLoggedIn from:body, userLoggedOut from:body"
></div>
```

Events bubble to `document.body` and can trigger re-fetches in multiple components simultaneously.

### Cookie-Based State Management

- Login state stored in `login` cookie (`"true"` or `"false"`)
- Set by `LoginViewComponent` via `HttpContext.Response.Cookies.Append()`
- Read by protected components (`FavoritesViewComponent`, `PaidContentViewComponent`) via `Request.Cookies["login"]`
- ViewComponents access request context through `HttpContext` property

### ViewComponent Location Resolution

Custom `ViewComponentLocationExpander` resolves ViewComponent views from the `ViewComponents/` folder:

```csharp
// Infrastructure/Razor/ViewComponentLocationExpander.cs
// Maps "Components/{Component}/Default" → "/ViewComponents/{Component}/{Component}.cshtml"
```

Registered in `Program.cs`:

```csharp
builder.Services.Configure<RazorViewEngineOptions>(o => {
    o.ViewLocationExpanders.Add(new ViewComponentLocationExpander());
});
```

### HTMX Out-of-Band Swaps

ViewComponents can return multiple HTML fragments targeting different DOM elements using `hx-swap-oob="innerHTML"`:

```html
<!-- OutOfBandViewComponent -->
<div id="target1">Content 1</div>
<div id="target2" hx-swap-oob="innerHTML">Content 2</div>
```

Single endpoint updates multiple page areas in one request.

## Development Workflow

**Run the app:**

```bash
dotnet watch run  # Auto-reload on file changes (port 5274)
# OR use VS Code task: "watch"
```

**Build:**

```bash
dotnet build
# OR use VS Code task: "build"
```

**Publish:**

```bash
dotnet publish
# OR use VS Code task: "publish"
```

## Key Conventions

1. **Namespace**: Root namespace is `WebsiteDemo` (not `web_demo`)
2. **ViewComponent structure**:
   - Class: `ViewComponents/{Name}/{Name}ViewComponent.cs`
   - View: `ViewComponents/{Name}/{Name}.cshtml`
   - Namespace: `WebsiteDemo.ViewComponents`
3. **Controller endpoints**: Always lowercase URLs: `/component/paidcontent`, `/component/login`
4. **HTMX triggers**: Use `load` for initial fetch, `every Xs` for polling, custom events for reactive updates
5. **Styling**: Tailwind CSS loaded via local file in `wwwroot/js/tailwindcss-3.4.1.js` - use utility classes directly in markup
6. **HTMX**: Local file at `wwwroot/js/htmx-2.0.3.js`

## Project Structure

- `Pages/` - Razor Pages (main views)
  - `Shared/_Layout.cshtml` - Base layout with HTMX/Tailwind scripts
  - `index.cshtml`, `testpage.cshtml` - Main pages containing HTMX triggers
- `ViewComponents/` - ViewComponent fragments
  - `{Name}/{Name}ViewComponent.cs` - C# logic
  - `{Name}/{Name}.cshtml` - Razor view
- `Controllers/ComponentController.cs` - HTTP endpoints for ViewComponents
- `Infrastructure/Razor/ViewComponentLocationExpander.cs` - Custom view resolution
- `Program.cs` - App configuration: RazorPages + Controllers + ViewComponent location expansion
- `Properties/launchSettings.json` - Dev server config (port 5274)
- `wwwroot/js/` - Static JavaScript files (HTMX, Tailwind)
