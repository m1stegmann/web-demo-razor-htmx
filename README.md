Short demo: Razor Pages + HTMX

This repository demonstrates a simple pattern mixing ASP.NET Core Razor Pages for full pages
and HTMX for dynamically fetching and swapping component fragments.

Concept overview

- Razor full pages: Use Razor Pages for complete views and routing (standard `.cshtml` pages).
- HTMX components: Put small, reusable HTML fragments under `Pages/Component/` and return
  them with `Layout = null` so they can be requested and inserted into existing pages.
- Event-driven updates: The app uses custom DOM events dispatched from the global layout
  to notify components about state changes (for example login/logout). Components can
  listen with `hx-trigger="eventName from:body"` to react to those events.
- Cookie-based state: Simple login state is stored in a cookie (named `login`) and
  server-side component pages read this cookie to decide what to render.
- Out-of-band swaps: Use HTMX `hx-swap-oob="innerHTML"` to update multiple parts of the
  DOM from a single endpoint.

Running the demo

- Build and run with `dotnet watch run` from the project root.

Files to look at

- `Pages/_Layout.cshtml`: global HTMX + Tailwind setup and event dispatcher.
- `Pages/Component/*`: HTMX-loadable components like `Login`, `Favorites`, `PaidContent`, `Teaser`, etc.
- `Pages/index.cshtml`: main page that loads components via HTMX `hx-get`/`hx-trigger` attributes.

Why this pattern

- Keeps full page routes simple while enabling highly interactive components without a
  large JavaScript framework.
- Encourages server-side rendering of small, testable fragments and reduces client complexity.

License

- This demo is provided as-is for educational purposes.
