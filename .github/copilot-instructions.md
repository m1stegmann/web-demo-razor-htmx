# Copilot Instructions for web-demo

## Project Overview

This is an **ASP.NET Core 10.0 Razor Pages** demo application showcasing **HTMX** integration patterns. The app demonstrates dynamic content loading, custom event-driven architecture, and out-of-band (OOB) swaps using HTMX with Tailwind CSS for styling.

## Architecture Patterns

### Component-Based Structure

- Components live in `Pages/Component/` as Razor Pages with `@page` directives (e.g., `/component/login`)
- All components set `Layout = null` to return HTML fragments, not full pages
- Components are loaded via HTMX `hx-get` attributes from main pages like `index.cshtml`

### HTMX Custom Event Pattern

The app uses **custom DOM events** for cross-component communication:

```csharp
// _Layout.cshtml - Global event dispatcher
document.body.addEventListener('htmx:beforeSwap', function (event) {
    const loginStatus = event.detail.elt.getAttribute('data-login-status');
    if (loginStatus) {
        const eventName = loginStatus === 'login' ? 'userLoggedOut' : 'userLoggedIn';
        document.body.dispatchEvent(new CustomEvent(eventName));
    }
});
```

Components listen via `hx-trigger="userLoggedIn from:body"` (see `Favorites.cshtml`, `PaidContent.cshtml`).

### Cookie-Based State Management

- Login state stored in `login` cookie (`"true"` or `"false"`)
- Set by `Login.cshtml.cs` via `Response.Cookies.Append()`
- Read by protected components (`Favorites`, `PaidContent`) via `Request.Cookies["login"]`

### HTMX Out-of-Band Swaps

See `htmx1.cshtml` + `OutOfBand.cshtml` - single endpoint updates multiple DOM elements using `hx-swap-oob="innerHTML"`.

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

1. **Namespace**: Root namespace is `web_demo` (hyphenated project name → underscored namespace)
2. **PageModel location**: Keep `.cshtml.cs` files alongside `.cshtml` (except `TeaserModel` in root namespace)
3. **HTMX triggers**: Use `load` for initial fetch, `every Xs` for polling (see `Teaser` component)
4. **Styling**: Tailwind CDN loaded in `_Layout.cshtml` - use utility classes directly in markup

## Project Structure

- `Pages/` - Razor Pages (main views)
  - `Component/` - HTMX-loadable components (fragments)
  - `Shared/_Layout.cshtml` - Base layout with HTMX/Tailwind scripts
- `Program.cs` - Minimal hosting model (no controllers, just Razor Pages + static files)
- `Properties/launchSettings.json` - Dev server config (port 5274)
