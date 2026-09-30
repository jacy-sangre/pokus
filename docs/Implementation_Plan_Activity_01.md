# Pokus — Implementation Plan: Activity 01

**Document type:** Implementation Plan (one per activity)
**Project:** Pokus (formerly "SlackOff")
**Activity:** 01 — Catch-up activity: Blazor website, pure CSS, Figma first
**Version:** 1.0
**Status:** Draft
**Master document:** `Pokus_Spec.md` (requirement IDs such as `FR-TEC-3` refer to that file)
**Conventions:** SHALL = mandatory, SHOULD = recommended, MAY = optional. Priority tiers: **P0** must exist to submit, **P1** should be done, **P2** cut first if time runs out.

---

## 1. Assignment Context

| Item | Value |
|------|-------|
| Goal | Make commits to a new repository by building a new website with Blazor on C# and pure CSS |
| Design | The expected app SHALL be designed in Figma before coding |
| Deliverables | (1) Figma project link, (2) GitHub repository link |
| Deadline | End of 2026-09-30, as recorded from the assignment. Confirm against the course announcement. |
| Format | Originally a 3-hour lab; time-boxes below are sized to roughly that plus buffer |
| Commit rules | A commit message etiquette document will be provided. See Section 8. |

### 1.1 Hard Constraints
| ID | Constraint |
|----|------------|
| C-1 | Styling SHALL be plain CSS only. No Bootstrap, Tailwind, Sass, or component libraries. |
| C-2 | The Bootstrap files that ship with the Blazor template SHALL be removed. |
| C-3 | JavaScript SHOULD be avoided. Any use SHALL be limited to small interop calls (e.g., a sound or `localStorage`). |
| C-4 | The Figma design SHALL exist before Blazor UI work begins. |
| C-5 | Commits SHALL follow the etiquette document once it is available. |
| C-6 | Commits SHALL be made incrementally throughout the work, not in one batch. |

---

## 2. Scope of Activity 01

Activity 01 builds the **front-end foundation of Pokus** as a working prototype. There is no database, no login, and no real AI call in this activity. Data is seeded and held in memory behind service interfaces so Entity Framework Core, Identity, and an AI provider can be added in later activities without rewriting the UI.

### 2.1 Coverage Table

| Spec area | Spec IDs | Activity 01 status |
|-----------|----------|--------------------|
| Technique library: browse, search, filters, detail, related, evidence badges | FR-TEC-1 to 6 | **Implement** (P0). Seed 12 fully written techniques; the other 19 are added in a later activity. |
| Technique favorites | FR-TEC-7 | **Simplify** (P1): stored in memory for the session |
| Start session from a technique | FR-TEC-8 | **Implement** (P1); "Make flashcards" link goes to the deck page |
| Bundles | FR-TEC-11 | **Implement** (P2), static |
| Find a technique wizard | FR-REC-1 to 4 | **Implement** (P1) with the 8 seeded situations |
| Focus presets, custom timer | FR-FOC-1 | **Implement** (P0) |
| Session setup: technique, task, notes | FR-FOC-2 | **Implement** (P0) |
| Timer screen, pause/resume/complete/cancel | FR-FOC-3, 4 | **Implement** (P0) |
| Timestamp-based timer | FR-FOC-5 | **Implement** (P0) in the service; survives navigation, not full page reload |
| Phase-end prompt, summary, follow-up actions | FR-FOC-6 to 8 | **Implement** (P0) |
| One active session | FR-FOC-9 | **Implement** (P1) |
| Focus history | FR-FOC-10 | **Simplify** (P1): in-memory list |
| Tab title countdown | FR-FOC-11 | **Defer** (needs JS interop) unless time allows (P2) |
| Subjects, decks, manual cards, edit/delete | FR-FLC-1 to 5 | **Implement** (P1) in memory |
| Card search and filter | FR-FLC-6 | **Simplify** (P2): text search only |
| Active-recall review interface, Knew/Forgot, keyboard shortcuts | FR-FLC-7, 10 | **Implement** (P1) |
| Review history | FR-FLC-8 | **Simplify** (P1): in memory |
| Optional spaced repetition ladder | FR-FLC-11, SR-1 to 5 | **Implement** (P1): `ISchedulingService` with unit tests |
| AI generation | FR-AI-1 to 13 | **Simplify** (P2): UI only, using a **fake generator** that returns sample drafts; demonstrates the draft-review-save flow. Real API call deferred. |
| Dashboard | FR-DSH-1 to 9 | **Simplify** (P0/P1): quick-start, current session, due cards, decks, recent techniques, weekly focus (seeded plus live in-memory data) |
| Analytics | FR-ANL-1 to 6 | **Simplify** (P2): simple CSS bar chart for weekly focus time and cards remembered vs forgotten |
| Messaging: tones and library | FR-MSG-1 to 6 | **Implement** (P0/P1): five tones, message library, in-page display |
| Gentle nudge | FR-MSG-7 | **Implement** (P2) |
| Settings: tone, theme, default preset | FR-SET-1, 3, 4 | **Implement** (P1) |
| About and evidence explanation | FR-ABT-1 | **Implement** (P1) |
| Accessibility, responsive, CSS tokens | NFR-1, 2, 7 | **Implement** (P0) |
| Authentication and accounts | FR-AUTH-* | **Defer** |
| Onboarding | FR-ONB-* | **Defer** |
| Database, EF Core, owner scoping | Spec Section 7 | **Defer** |
| Real AI provider, rate limits | FR-AI-7 to 10 | **Defer** |
| Privacy page, account deletion | FR-ABT-2, FR-AUTH-4 | **Defer** |
| All LATER items | — | **Defer** |

### 2.2 Carry-Over to Later Activities
- Services are defined as interfaces (e.g., `ITechniqueService`, `IFocusService`, `ICardService`, `IReviewService`, `ISchedulingService`, `IFlashcardGenerator`, `IMessageService`) with in-memory implementations. Later activities replace the implementations with EF Core and real AI.
- Model classes follow the entity names in Spec Section 7.

---

## 3. Technology Decisions

| Area | Decision |
|------|----------|
| Framework | Blazor Web App, current .NET LTS, created with `dotnet new blazor`, Interactive Server render mode |
| Language | C# |
| State | In-memory singleton services. `ProtectedLocalStorage` or JS interop persistence only as a P2 item. |
| Styling | Plain CSS. Global `tokens.css` and `base.css`, plus component-scoped `.razor.css` files. |
| Charts | CSS-only bars (no chart library) |
| Timer | `PeriodicTimer` to tick the UI; remaining time computed from stored timestamps; `InvokeAsync(StateHasChanged)`; dispose timers with `IDisposable` |
| Data seed | C# seed classes (or a JSON file loaded at startup) for techniques, recommendation rules, and messages |
| Tests | xUnit for `ISchedulingService`, `IMessageService`, and focus timer calculations (P1) |
| Source control | Git and GitHub |

---

## 4. Architecture for Activity 01

```
Pokus/
├─ Pokus.sln
├─ README.md
├─ docs/
│  ├─ Pokus_Spec.md
│  ├─ Implementation_Plan_Activity_01.md
│  └─ design/                 (Figma link, exported frames)
├─ src/Pokus.Web/
│  ├─ Program.cs
│  ├─ Components/
│  │  ├─ Layout/              (MainLayout, NavMenu)
│  │  ├─ Pages/               (Home, Techniques, TechniqueDetail, Find, Focus,
│  │  │                        FocusSession, FocusSummary, Flashcards, DeckDetail,
│  │  │                        Review, Generate, Analytics, Settings, About)
│  │  └─ Shared/              (TechniqueCard, EvidenceBadge, FilterBar, FocusTimer,
│  │                           ReviewCard, DraftCardList, BarChart, MessageBanner, EmptyState)
│  ├─ Domain/                 (Technique, FocusSession, Deck, Flashcard, CardReview, ...)
│  ├─ Services/               (interfaces + in-memory implementations)
│  ├─ Data/                   (seed data: techniques, messages, rules)
│  └─ wwwroot/css/            (tokens.css, base.css, app.css)
└─ tests/Pokus.Tests/
```

### 4.1 Seeded Techniques (12)
Pomodoro, 52/17, Time Blocking, Deep Work, Two-Minute Rule, Implementation Intentions, Active Recall, Spaced Repetition, Feynman Technique, Interleaving, Cornell Note-Taking, Method of Loci.

Each SHALL include all content-model fields from Spec Section 5.4, an evidence level and note, and related-technique links. Text SHALL be written in original wording.

### 4.2 Seeded Messages
Ten categories from Spec 5.10, at least two messages per tone per category for `nudge_start`, `session_start`, `break_start`, `session_complete`, and `cards_due`; one per tone for the rest. Samples: Spec Appendix B.

---

## 5. Design Phase (Figma) — Phase 1

Design work SHALL be finished before Blazor UI work begins (C-4).

### 5.1 Steps
| Step | Task | Output |
|------|------|--------|
| D-1 | Create the Figma file "Pokus" with pages: Tokens, Components, Screens, Prototype | Figma file |
| D-2 | Define tokens: color palette (light and dark), type scale, spacing (8-px grid), radius, elevation | Variables/styles |
| D-3 | Build components: button, chip, evidence badge, technique card, deck card, flashcard, timer, banner/toast, nav, filter bar, empty state | Component library |
| D-4 | Design the screens in 5.2 (desktop ~1280 px, the primary target for a web app) | Frames |
| D-5 | Design Log in and Create account (~1280 px); the auth flow itself is built in a later activity | Frames |
| D-6 | Link frames into a clickable prototype for the main flows | Prototype |
| D-7 | Set sharing to "anyone with the link can view" and copy the link | Submission link |
| D-8 | Export key frames to `docs/design/`; add the Figma link to `README.md` | Repo docs |

### 5.2 Screens
0. Log in and Create account (design only in Activity 01)
1. Landing/Dashboard
2. Technique library (grid, search, filters)
3. Technique detail
4. Find a technique wizard and result
5. Focus setup
6. Focus session (running and paused)
7. Focus summary
8. Decks overview
9. Deck detail with card list and card editor
10. Review (front, revealed back, Knew/Forgot)
11. Generate with AI (input, drafts list)
12. Analytics
13. Settings (tone, theme)

### 5.3 Design Rules
- Clean, modern, calm; humor lives in the copy, not in mascots or decoration.
- No gamification elements (no XP, badges, streak counters, characters).
- Evidence labels use text plus an icon, not color alone.
- One typeface family with two weights; generous spacing; restrained accent color.
- Dark theme uses the same tokens with overridden values.
- Web app: every screen is designed at 1280 px with a sidebar (sign-in screens use a split layout without one).

---

## 6. Build Phases

Time-boxes are estimates. Tiers show what to cut first.

| Phase | Name | Est. | Tier | Tasks | Done when |
|-------|------|------|------|-------|-----------|
| 0 | Repository setup | 15 min | P0 | Create GitHub repo; `.gitignore` (VisualStudio/.NET), `README.md`; clone; add `docs/`; first commit | Repo exists with initial commit |
| 1 | Figma design | 75–90 min | P0 | Section 5 | Figma link opens logged out |
| 2 | Scaffold | 20 min | P0 | `dotnet new blazor`; remove Bootstrap and demo pages; create folders; add `tokens.css`, `base.css`; layout and nav; empty pages routed | App runs with custom CSS only |
| 3 | Domain, services, seed data | 30 min | P0 | Entities, enums, service interfaces, in-memory implementations, technique seed (12), messages, rules; register in DI | Services resolve; technique list returns 12 items |
| 4 | Technique library | 40 min | P0 | Grid, search, filters (category, evidence, difficulty), detail page, evidence badge, related links | Matches Figma; filters combine correctly |
| 5 | Focus system | 45 min | P0 | Presets, setup form, timer with pause/resume/complete/cancel, phase-end prompt, summary, history | A 25/5 session (with a short test preset) runs end to end |
| 6 | Messaging and settings | 25 min | P0/P1 | `IMessageService`, banner component, tone selector, theme toggle, default preset | Changing tone changes visible messages |
| 7 | Dashboard | 25 min | P0/P1 | Quick-start, current session, due cards, decks, recent techniques, weekly focus bars | Reflects live in-memory data |
| 8 | Flashcards | 50 min | P1 | Subjects/decks/cards CRUD (in memory), review UI with reveal, Knew/Forgot, keyboard shortcuts, scheduling service with tests, review history | Review updates due dates per SR-1 to SR-3 |
| 9 | Find a technique | 20 min | P1 | Wizard with 8 situations, results with reasons | Each situation returns 2–4 techniques |
| 10 | Fake AI generation flow | 25 min | P2 | Input form with controls, `FakeFlashcardGenerator`, editable drafts, save selected to a deck, warning text | Drafts can be edited and saved; manual flow unaffected |
| 11 | Analytics and About | 20 min | P2 | CSS bar charts, About page with evidence labels | Charts render from in-memory data |
| 12 | Polish | 25 min | P1 | Responsive layouts (390 px and 1280 px), dark theme, focus outlines, reduced motion, empty states | Usable at both widths; keyboard navigable |
| 13 | Submission | 10 min | P0 | README (run steps, screenshots, Figma link); final push; verify links | Both links open when logged out |

### 6.1 CSS Guidelines
- Tokens as custom properties on `:root`; dark theme via `[data-theme="dark"]` (and optionally `prefers-color-scheme`).
- Layout with flexbox and grid; relative units for type and spacing.
- BEM-style class names (`card__title--muted`).
- Component styles in `.razor.css`; shared primitives in `base.css`.
- Respect `prefers-reduced-motion`.

### 6.2 Key Implementation Notes
- **Timer:** store `StartedAt`, `PausedTotalSeconds`, and `PausedAt`. Remaining = planned duration minus (now minus StartedAt minus paused time). The `PeriodicTimer` only triggers re-rendering.
- **Scheduling:** `ISchedulingService.Apply(card, result, now)` returns the new box and due date. Rules per Spec 5.7.1. Unit-test each box transition and the "Forgot" rule.
- **Message picking:** filter by category and tone; avoid repeating the last message in the same category.
- **Fake AI:** deterministic sample drafts that respect the requested count. The UI SHALL include the notices from FR-AI-11 and FR-AI-12 even though the generator is fake.

---

## 7. Testing and Verification

| Area | Method |
|------|--------|
| Scheduling ladder (Knew/Forgot transitions, due dates) | xUnit |
| Timer calculations (pause, resume, completion) | xUnit with `IClock` fake |
| Message picking (tone, category, no immediate repeat) | xUnit |
| Technique filtering | xUnit |
| UI flows | Manual walk-through against Figma frames |
| Responsiveness | Manual check at 390 px and 1280 px |
| Pure-CSS compliance | Search project for `bootstrap`, `tailwind`, `.scss`; expect none |

### 7.1 Acceptance Checklist
- [ ] Figma file opens via link with view access and contains the screens in 5.2
- [ ] GitHub repo is accessible and shows many small, meaningful commits
- [ ] `dotnet run` starts without errors
- [ ] No CSS framework present; template Bootstrap removed
- [ ] Technique library lists 12 techniques with working search and filters
- [ ] Every technique shows an evidence label and note
- [ ] Focus session runs with pause, resume, complete, cancel, and shows a summary
- [ ] Tone selector changes message text; Neutral has no jokes
- [ ] Flashcards can be created, edited, deleted, and reviewed with Knew/Forgot
- [ ] No XP, badges, or streaks anywhere in the UI
- [ ] AI generation (if built) is clearly optional and drafts are editable before saving
- [ ] README includes run instructions and the Figma link
- [ ] Commit messages conform to the etiquette document

---

## 8. Version Control Plan

### 8.1 Repository
- Suggested name: `pokus` (final name is the user's choice).
- Visibility: as required by the course; the instructor SHALL be able to open it.
- Default branch: `main`. Use short-lived `feature/<name>` branches only if the course requires them.

### 8.2 Commit Message Etiquette — PENDING
The etiquette document will be provided. Until then:
- Write messages in the imperative mood; one logical change per commit.
- Keep commits small (one task, not a whole phase).
- **When the etiquette document arrives it overrides this subsection.** Update 8.2 and reword the checkpoint list in 8.3 to its required format (prefixes, casing, length, scope, body, references). Unpushed history can be reworded with an interactive rebase.

### 8.3 Planned Commit Checkpoints (placeholder wording)
| # | Checkpoint |
|---|------------|
| 1 | Initial commit: `.gitignore`, README |
| 2 | Add spec and implementation plan to `docs/` |
| 3 | Add Figma link and exported frames |
| 4 | Scaffold Blazor Web App |
| 5 | Remove Bootstrap and template demo pages |
| 6 | Add CSS tokens and base styles |
| 7 | Add domain models and enums |
| 8 | Add service interfaces and in-memory implementations |
| 9 | Add technique seed data |
| 10 | Add message and recommendation seed data |
| 11 | Add main layout and navigation |
| 12 | Add technique library page with cards |
| 13 | Add search and filters |
| 14 | Add technique detail page and evidence badge |
| 15 | Add focus presets and setup page |
| 16 | Add focus timer service |
| 17 | Add focus session page with pause and resume |
| 18 | Add focus summary and history |
| 19 | Add message service and banner component |
| 20 | Add settings page with tone and theme |
| 21 | Add dashboard |
| 22 | Add deck and card management |
| 23 | Add scheduling service with unit tests |
| 24 | Add flashcard review interface |
| 25 | Add Find a technique wizard |
| 26 | Add AI generation UI with fake generator |
| 27 | Add analytics charts and About page |
| 28 | Add dark theme and responsive styles |
| 29 | Update README with run instructions and screenshots |

### 8.4 Commit Rules
- Commit after each checkpoint; push at least after each phase.
- Do not commit `bin/`, `obj/`, `.vs/`, or user-specific files.
- Do not commit secrets. Activity 01 needs none; the real AI key is a later concern.

---

## 9. Risks and Mitigations

| Risk | Impact | Mitigation |
|------|--------|------------|
| Deadline is close | Incomplete submission | Follow the tier order; push after every phase; P2 phases (10, 11) are cut first |
| Figma takes longer than planned | Delays coding | Limit to the 13 screens plus the two sign-in screens |
| Timer does not refresh the UI | Broken focus page | Use `InvokeAsync(StateHasChanged)`; dispose timers |
| Writing 12 quality technique entries takes time | Delays Phase 3 | Write the 5 most important first (Pomodoro, Active Recall, Spaced Repetition, Feynman, Implementation Intentions); add the rest as time allows |
| In-memory data lost on refresh | Confusing demo | State this in the README; add optional persistence only if time remains |
| Commit format unknown | Rework | Keep commits small; reword later |
| Scope creep from the master spec | Missed deadline | Stay within Section 2.1 |

---

## 10. Suggested Work Blocks

Given the recorded deadline (end of 2026-09-30), work is planned in blocks rather than fixed clock times.

| Block | Work | Approx. time |
|-------|------|--------------|
| Block A | Phase 0, Phase 1 (Figma), Phase 2 | ~2 h |
| Block B | Phases 3, 4, 5 | ~2 h |
| Block C | Phases 6, 7, 8 | ~1.75 h |
| Block D | Phases 9, 12, 13 (P1/P0 finish) | ~1 h |
| Optional | Phases 10, 11 (P2) | ~45 min |

**Minimum viable submission (if time is very short):** Phase 0, Phase 1 (fewer screens), Phase 2, Phase 3 (with 5 techniques), Phase 4, Phase 5, Phase 13. This still delivers a Figma design, a working Blazor site with the technique library and a focus timer, and a commit history.

---

## 11. Submission Checklist
1. Open the Figma link in a private window and confirm it loads with view access.
2. Open the GitHub repository link in a private window and confirm it loads.
3. Confirm the commit history shows incremental work.
4. Submit both links through the course channel before the deadline.
