# Pokus

A place to find a technique that fits, start focusing, and actually learn — with a bit of humor along the way.

Pokus is a Blazor web app with three pillars:

- **Techniques** — a curated, honestly labeled library of focus and study techniques
- **Focus** — timed focus sessions linked to a technique and a task
- **Flashcards** — active-recall flashcards with optional spaced repetition

Built with C#, Blazor (.NET 10, Interactive Server) and Tailwind CSS v4.

## Running locally

Requirements: [.NET 10 SDK](https://dotnet.microsoft.com/download) and [Node.js](https://nodejs.org/) (for the Tailwind build).

```bash
cd src/Pokus.Web
dotnet run
```

Then open http://localhost:5219.

`dotnet build` installs the npm packages on first run and compiles `Styles/app.css` into `wwwroot/app.css`.
While editing markup, run the Tailwind watcher in a second terminal so new classes appear without a rebuild:

```bash
cd src/Pokus.Web
npm run css:watch
```

## Styling

- Design tokens (colours, type scale, radii, elevation) come from the Figma file and live in [`src/Pokus.Web/Styles/app.css`](src/Pokus.Web/Styles/app.css).
- Colours are CSS variables, so the dark theme swaps values while the Tailwind classes (`bg-canvas`, `text-muted`, `bg-accent`, ...) stay the same.
- Pages are styled with Tailwind utility classes directly in the `.razor` markup; there are no hand-written component stylesheets.

## Design

- **Figma file:** https://www.figma.com/design/QhqG6CmTDdOvYsNYtN1PXA/pokus-for-once?node-id=1-77&t=Ukee8n8c6INNC6Sr-1

## Docs

- [Product specification](docs/Pokus_Spec.md)
- [Activity 01 implementation plan](docs/Implementation_Plan_Activity_01.md)
