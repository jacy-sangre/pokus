# Pokus — Master Product Specification

**Document type:** Product Requirements Specification (master)
**Product name:** Pokus (formerly "SlackOff")
**Version:** 2.0
**Status:** Draft
**Platform:** Web application — C# / .NET / Blazor
**Conventions:** The keywords SHALL, SHALL NOT, SHOULD, and MAY are used as in RFC 2119. Every requirement has a unique ID and a release tag: `[MVP]` (required for the first complete release) or `[LATER]` (deferred). Per-activity implementation plans reference these IDs and state which ones they implement, simplify, or defer.

---

## 1. Overview

### 1.1 Concept
Pokus is a web application where users **discover practical focus and learning techniques, start a focus session, and actively learn through flashcards**.

It carries over the personality of the original SlackOff (witty, playful, user-controlled) but the product is no longer about screen-time tracking. It is about intentional focus and effective learning.

### 1.2 Identity Statement
> A place to find a technique that fits, start focusing, and actually learn — with a bit of humor along the way.

Pokus SHALL NOT become a generic study planner or a generic productivity dashboard. The three pillars are:

| Pillar | Description |
|--------|-------------|
| P-A: Techniques | A curated, honestly-labeled library of focus, productivity, studying, and memory techniques |
| P-B: Focus | Timed focus sessions that can be linked to a technique and a task |
| P-C: Flashcards | Active-recall flashcards with optional spaced repetition and optional AI card generation |

### 1.3 Problem Statement
- Students and self-learners often rely on passive methods (re-reading, highlighting) and are unaware of better-supported alternatives.
- Productivity advice online mixes well-supported methods with popular but weakly supported ones, without saying which is which.
- Timer apps, flashcard apps, and technique guides live in separate tools, so users must stitch them together.
- Many productivity tools feel like generic dashboards, or push gamification (streaks, points) that can distract from the work itself.

### 1.4 Target Users
| Persona | Description | Main needs |
|---------|-------------|------------|
| Student | Secondary school, college, or exam-review student | Study methods, flashcards, focus timers, exam preparation |
| Self-learner | Learning a skill (programming, language, certification) | Memory techniques, concept-understanding techniques, spaced review |
| Distracted worker | Knowledge worker or freelancer who struggles to start or sustain focus | Focus and planning techniques, low-friction timers |

Primary launch audience: students, particularly Filipino students (hence the Filipino message tone).

### 1.5 Design Principles
| ID | Principle |
|----|-----------|
| DP-1 | **User control:** Users choose techniques, durations, tone, and whether to use AI. Pokus SHALL NOT force one prescribed method. |
| DP-2 | **Honest evidence labeling:** Every technique SHALL carry an evidence classification (Section 3). Pokus SHALL NOT claim that every technique is scientifically proven. |
| DP-3 | **No gamification:** No XP, levels, badges, characters, points, leaderboards, or artificial streak mechanics. |
| DP-4 | **Descriptive, not prescriptive:** Recommendations and insights describe options and observations. They SHALL NOT claim a single optimal method or diagnose the user. |
| DP-5 | **Humor with respect:** Messages are playful and relatable, occasionally sarcastic, and SHALL NOT insult or guilt-trip the user. |
| DP-6 | **AI is optional:** All flashcard features SHALL work fully without AI. |
| DP-7 | **Simple, modern, clean UI:** Avoid excessive metrics and clutter. |
| DP-8 | **Web-native:** Only capabilities available to a normal web application are used. |

### 1.6 Explicit Non-Goals
Pokus SHALL NOT include features that depend on:
- Android UsageStatsManager, iOS Screen Time APIs, or any OS-level usage monitoring
- Monitoring or controlling other apps or sites (TikTok, Instagram, YouTube, etc.)
- Native overlays or native background services
- XP, levels, badges, mascots, or streak mechanics

---

## 2. Glossary

| Term | Definition |
|------|------------|
| Technique | A named focus, productivity, study, memory, or planning method in the library |
| Evidence level | Classification of how well a technique is supported (Section 3) |
| Focus session | A user-started timed work period, optionally linked to a technique and a task |
| Preset | A predefined focus/break timing (e.g., Pomodoro 25/5) |
| Subject | Top-level grouping of decks (e.g., "Theory of Computation") |
| Deck | A collection of flashcards |
| Card | A flashcard with a front (prompt) and back (answer) |
| Review | One attempt to recall a card, ending in "Knew it" or "Forgot" |
| Due card | A card whose next review time has arrived |
| Tone | The selected personality of message text |
| Draft card | An AI-generated card not yet saved by the user |

---

## 3. Evidence Classification

Every technique SHALL be assigned exactly one evidence level. The UI SHALL display it as a label and SHALL provide a short explanation on request.

| Level ID | Label | Meaning |
|----------|-------|---------|
| `research_supported` | Research-supported | Backed by multiple controlled studies or reviews, though effect size and context still matter |
| `commonly_used` | Commonly used method | Widely used and structured, with limited or indirect formal evidence |
| `practical_heuristic` | Practical heuristic | A rule of thumb or framework that many find useful; not formally tested as a technique |
| `mixed_or_limited` | Mixed or limited evidence | Popular, but evidence is mixed, indirect, or thin |

Rules:
- EV-1: A technique's evidence level SHALL be accompanied by a one- or two-sentence `EvidenceNote` explaining the label.
- EV-2: Where the source material does not support a stronger claim, the lower level SHALL be used.
- EV-3: The About page SHALL explain these labels and state that evidence classifications are simplified summaries, not medical or educational advice.
- EV-4: Technique content SHALL be reviewed by the maintainer against its cited sources before release.

---

## 4. User Flows

### 4.1 First-time user
1. Landing page explains Pokus in one screen. Actions: **Get started**, **Log in**.
2. Register (email and password) or log in.
3. Short welcome: choose a message tone (default Witty) and optionally state a goal ("studying for an exam", "staying focused", "learning a skill"). Both skippable.
4. Land on the Dashboard with quick-start actions.

### 4.2 "I need help choosing" flow
1. User opens **Find a technique** and selects a situation (e.g., "I need to memorize something").
2. Pokus shows 2–4 suggested techniques with a one-line reason each and their evidence labels.
3. User opens a technique, reads the guide, and chooses **Start a session with this** or **Make flashcards**.

### 4.3 Focus flow
1. User chooses a preset (or custom), optionally a technique, task, and notes.
2. Session runs: timer, pause/resume, complete/cancel.
3. On completion, a simple summary is shown, with options to start a break, start another session, or review due flashcards.

### 4.4 Flashcard flow
1. User creates a subject and deck.
2. User adds cards manually, or opens **Generate with AI**, enters notes or a topic, reviews and edits drafts, and saves the ones they want.
3. User starts a review. For each card: see the prompt, reveal the answer, mark **Knew it** or **Forgot**.
4. Review history and next due dates are updated.

---

## 5. Functional Requirements

### 5.1 Authentication and Accounts (FR-AUTH)

| ID | Requirement | Scope |
|----|-------------|-------|
| FR-AUTH-1 | The app SHALL use ASP.NET Core Identity with email and password registration and login. | MVP |
| FR-AUTH-2 | All user data (sessions, decks, cards, reviews, favorites, settings) SHALL be private to its owner. Every query SHALL be scoped by owner ID. | MVP |
| FR-AUTH-3 | The technique library and the About page SHALL be viewable without logging in. | MVP |
| FR-AUTH-4 | Users SHALL be able to log out, change their password, and delete their account and all associated data. | MVP |
| FR-AUTH-5 | Email confirmation and password reset SHOULD be supported. | LATER |
| FR-AUTH-6 | External login (e.g., Google) MAY be supported. | LATER |
| FR-AUTH-7 | A guest mode with local-only data MAY be supported. | LATER |

### 5.2 Onboarding (FR-ONB)

| ID | Requirement | Scope |
|----|-------------|-------|
| FR-ONB-1 | After first registration, the app SHALL offer a skippable welcome step to choose a tone. | MVP |
| FR-ONB-2 | The welcome step MAY ask for a primary goal (exam study, staying focused, learning a skill) to seed the dashboard's suggestions. | LATER |

### 5.3 Dashboard (FR-DSH)

The dashboard SHALL stay light. It SHALL NOT show more than the items below.

| ID | Requirement | Scope |
|----|-------------|-------|
| FR-DSH-1 | Show a **quick-start** row: Start focus session, Review due cards, Find a technique, Create flashcards. | MVP |
| FR-DSH-2 | Show the **current focus session** (technique, task, time remaining) with a link back to it when one is active. | MVP |
| FR-DSH-3 | Show **cards due for review** (count) and the decks they belong to. | MVP |
| FR-DSH-4 | Show the user's **flashcard decks** (name, card count, due count). | MVP |
| FR-DSH-5 | Show **recently used techniques** and **favorite techniques**. | MVP |
| FR-DSH-6 | Show **recent study/focus sessions** (last 5). | MVP |
| FR-DSH-7 | Show **weekly focus time** as a single figure with a small daily bar chart. | MVP |
| FR-DSH-8 | Show a **learning activity** summary: cards reviewed this week. | MVP |
| FR-DSH-9 | Show one short **witty greeting** or nudge in the selected tone. | MVP |

### 5.4 Technique Library (FR-TEC)

**Content model.** Each technique SHALL have:

| Field | Required | Description |
|-------|----------|-------------|
| Name | Yes | Display name |
| Slug | Yes | URL-safe identifier |
| Short description | Yes | 1–2 sentences |
| Useful for | Yes | What problem it addresses |
| How to use | Yes | Ordered steps |
| Suggested session length | Where relevant | e.g., "25 min focus / 5 min break" |
| Best use cases | Yes | List |
| Limitations and caveats | Yes | List |
| Related techniques | Yes | Links to other techniques |
| Difficulty | Yes | `easy`, `moderate`, `advanced` |
| Evidence level and note | Yes | See Section 3 |
| Categories | Yes | One or more (see below) |
| Sources and references | Where appropriate | Author, title, year, optional link |

**Categories:** Focus, Productivity, Studying, Memory, Note-taking, Planning, Time management, Exam preparation.

| ID | Requirement | Scope |
|----|-------------|-------|
| FR-TEC-1 | The library SHALL contain the techniques in Appendix A, seeded from application data. | MVP |
| FR-TEC-2 | The user SHALL be able to browse techniques as a card grid or list. | MVP |
| FR-TEC-3 | The user SHALL be able to search by name and keyword (name, description, useful-for). | MVP |
| FR-TEC-4 | The user SHALL be able to filter by category, evidence level, difficulty, and session length. | MVP |
| FR-TEC-5 | Each technique SHALL have a detail page showing all fields in the content model. | MVP |
| FR-TEC-6 | Related techniques SHALL be shown as links on the detail page. | MVP |
| FR-TEC-7 | Logged-in users SHALL be able to favorite/bookmark techniques and view a favorites list. | MVP |
| FR-TEC-8 | The detail page SHALL offer **Start a session with this** (opens the focus setup pre-filled) for techniques with a timing, and **Make flashcards** for learning techniques. | MVP |
| FR-TEC-9 | Logged-in users MAY add private notes to a technique. | LATER |
| FR-TEC-10 | Users MAY submit suggestions for new techniques. | LATER |
| FR-TEC-11 | Curated "bundles" (e.g., "Exam prep: Active Recall + Spaced Repetition + Practice Testing") SHOULD be shown as suggestions. Bundles SHALL be presented as options, not prescriptions. | MVP |

### 5.5 Personalized Recommendations (FR-REC)

Recommendations are rule-based and lightweight. They are driven by the user's selection, not by hidden profiling.

| ID | Requirement | Scope |
|----|-------------|-------|
| FR-REC-1 | The app SHALL provide a **Find a technique** wizard with the situations in the table below. | MVP |
| FR-REC-2 | Each situation SHALL map to 2–4 techniques via seeded rules (`RecommendationRule`). | MVP |
| FR-REC-3 | Each suggestion SHALL show a one-line reason and its evidence label. | MVP |
| FR-REC-4 | Wording SHALL be descriptive ("These are commonly used for this") and SHALL NOT claim a universally best method. | MVP |
| FR-REC-5 | The user MAY combine situations (e.g., time available plus goal). | LATER |

| Situation | Suggested techniques (initial mapping) |
|-----------|----------------------------------------|
| I have 30 minutes | Pomodoro, Timeboxing, Two-minute rule, Single-tasking |
| I need to memorize something | Active Recall, Spaced Repetition, Leitner System, Method of Loci |
| I keep getting distracted | Distraction management, 5-minute rule, Single-tasking, Deep Work |
| I need to understand a difficult concept | Feynman Technique, Self-explanation, Elaborative Interrogation, Dual Coding |
| I need to plan a large task | Task chunking, Time blocking, Implementation intentions, WOOP |
| I'm studying for an exam | Active Recall, Spaced Repetition, Practice Testing, Interleaving |
| I can't get started | 5-minute rule, Two-minute rule, Eat the Frog, Implementation intentions |
| I'm taking notes in class | Cornell Note-Taking, Dual Coding |

### 5.6 Focus Sessions (FR-FOC)

**Presets**

| Preset ID | Focus / Break | Notes |
|-----------|---------------|-------|
| pomodoro | 25 / 5 | Default |
| short_25 | 15 / 5 | "15/5" |
| fifty_two_seventeen | 52 / 17 | "52/17" |
| ninety_twenty | 90 / 20 | "90/20" |
| short_focus | 10 / 2 | Short focus session |
| long_focus | 60 / 10 | Long focus session |
| custom | user-defined | Focus and break minutes set by the user |

| ID | Requirement | Scope |
|----|-------------|-------|
| FR-FOC-1 | The user SHALL be able to start a session from any preset above. | MVP |
| FR-FOC-2 | Session setup SHALL allow choosing a technique (optional), a task description (optional), and notes (optional). | MVP |
| FR-FOC-3 | During a session, the screen SHALL show: countdown timer, current phase (focus or break), current technique, current task, and a notes field. | MVP |
| FR-FOC-4 | The user SHALL be able to pause, resume, complete early, and cancel a session. | MVP |
| FR-FOC-5 | Timer state SHALL be derived from stored timestamps (start time, accumulated pause time) so that a page refresh or reconnect does not lose the session. | MVP |
| FR-FOC-6 | When a focus phase ends, the app SHALL indicate it in-page (message and optional sound) and offer **Start break** or **Finish**. | MVP |
| FR-FOC-7 | On completion, the app SHALL show a simple summary: total focus time, technique, task, notes, and pauses. It SHALL NOT show scores, ratings, or rewards. | MVP |
| FR-FOC-8 | The summary SHALL offer: Start another session, Take a break, Review due cards. | MVP |
| FR-FOC-9 | Only one active session per user SHALL exist at a time. | MVP |
| FR-FOC-10 | A focus history page SHALL list past sessions with date, duration, technique, and task. | MVP |
| FR-FOC-11 | The browser tab title SHOULD show the remaining time while a session runs. | MVP |
| FR-FOC-12 | Multiple focus/break cycles per session (auto-advance through cycles) MAY be supported. | LATER |
| FR-FOC-13 | Optional browser notifications (Web Notifications API, with user permission) MAY announce phase changes. | LATER |
| FR-FOC-14 | Users MAY save custom presets. | LATER |

### 5.7 Flashcards (FR-FLC)

**Organization:** Subject → Deck → Card. Cards MAY also have tags (topics).

| ID | Requirement | Scope |
|----|-------------|-------|
| FR-FLC-1 | The user SHALL be able to create, rename, and delete subjects. | MVP |
| FR-FLC-2 | The user SHALL be able to create, rename, move, and delete decks. | MVP |
| FR-FLC-3 | The user SHALL be able to create cards manually with a front, a back, and optional tags. | MVP |
| FR-FLC-4 | The user SHALL be able to edit and delete cards. | MVP |
| FR-FLC-5 | The user SHALL be able to move a card to a different deck. | MVP |
| FR-FLC-6 | The user SHALL be able to search cards by text and filter by subject, deck, tag, and status (new, learning, due, suspended). | MVP |
| FR-FLC-7 | The **review interface** SHALL follow an active-recall pattern: show the front only, let the user attempt recall, reveal the back on request, then ask **Knew it** or **Forgot**. | MVP |
| FR-FLC-8 | Each review SHALL be stored in review history (card, time, result, time taken). | MVP |
| FR-FLC-9 | Review sessions SHALL support: a whole deck, a subject, or "all due cards". | MVP |
| FR-FLC-10 | Review SHALL support keyboard use: Space = reveal, 1 = Forgot, 2 = Knew it. | MVP |
| FR-FLC-11 | **Scheduling mode (optional):** each deck SHALL have a setting **Use spaced repetition** (on by default). When on, results update the next due date per Section 5.7.1. When off, the deck is reviewed in simple shuffled order and no due dates are used. | MVP |
| FR-FLC-12 | The user SHALL be able to suspend a card (excluded from review) and unsuspend it. | LATER |
| FR-FLC-13 | Bulk import of cards from CSV SHOULD be supported. Export to CSV/JSON SHOULD be supported. | LATER |
| FR-FLC-14 | Cards MAY have an image on the front or back. | LATER |
| FR-FLC-15 | A four-level rating (Again, Hard, Good, Easy) MAY replace the two-button result. | LATER |
| FR-FLC-16 | Cloze-deletion cards MAY be supported. | LATER |

#### 5.7.1 Spaced Repetition Scheduling (MVP rule)
The MVP SHALL use a simple Leitner-style interval ladder, which is easy to explain and test.

| Box | Interval until next review |
|-----|----------------------------|
| 0 (new / just forgotten) | 1 day (or same day for first-time learning) |
| 1 | 1 day |
| 2 | 3 days |
| 3 | 7 days |
| 4 | 14 days |
| 5 | 30 days |

Rules:
- SR-1: **Knew it** moves the card up one box (maximum box 5) and sets `DueAt = now + interval(box)`.
- SR-2: **Forgot** moves the card to box 0 and sets `DueAt` to the current time plus 10 minutes (so it reappears in the same session) or to the next day, as a system setting.
- SR-3: New cards start in box 0 and are due immediately.
- SR-4: The scheduling logic SHALL live in one isolated service so that it can be replaced by SM-2 or FSRS later without touching the UI.
- SR-5: The UI SHALL describe the schedule plainly ("You'll see this again in 3 days") and SHALL NOT present it as a score.

### 5.8 AI Flashcard Generation (FR-AI)

| ID | Requirement | Scope |
|----|-------------|-------|
| FR-AI-1 | The app SHALL offer **Generate with AI** as an optional entry point on the flashcards pages. If AI is not configured or the user prefers not to use it, no feature is blocked. | MVP |
| FR-AI-2 | Input sources: (a) pasted notes or text, (b) study material text, (c) a topic entered by the user. Example: "Generate flashcards about the five components of a finite automaton." | MVP |
| FR-AI-3 | Controls: number of cards (e.g., 5–30), difficulty (basic, intermediate, advanced), question type (definition, concept/explanation, "why/how", true-false, mixed), topic/label, and target deck. | MVP |
| FR-AI-4 | The generator SHALL return **draft cards** (front, back) that are shown in an editable list. Nothing is saved automatically. | MVP |
| FR-AI-5 | The user SHALL be able to edit, delete, select, or regenerate individual drafts before saving. Only selected drafts are saved to the chosen deck. | MVP |
| FR-AI-6 | Cards SHALL be concise: one idea per card, short answers. | MVP |
| FR-AI-7 | The AI provider call SHALL be made server-side. API keys SHALL NOT be exposed to the browser. | MVP |
| FR-AI-8 | The AI service SHALL request structured output (JSON), validate it against a schema, and handle malformed output with a clear retry message. | MVP |
| FR-AI-9 | Input length SHALL be capped, and per-user rate limits (e.g., requests per day) SHALL be enforced. | MVP |
| FR-AI-10 | Pasted text SHALL be treated as data, not as instructions (prompt-injection awareness). The system prompt SHALL instruct the model to generate cards only from the supplied material or topic. | MVP |
| FR-AI-11 | The UI SHALL state that pasted text is sent to a third-party AI provider, and SHALL remind the user that generated cards can contain errors and must be checked. | MVP |
| FR-AI-12 | For a topic-only request, the UI SHALL show a note that the content comes from the model's general knowledge and should be verified. | MVP |
| FR-AI-13 | The provider SHALL be hidden behind an interface (`IFlashcardGenerator`) so that it can be swapped or disabled by configuration. | MVP |
| FR-AI-14 | File upload (PDF, DOCX) as an input source MAY be supported. | LATER |
| FR-AI-15 | AI-generated "explain this card" or "make this card easier" helpers MAY be supported. | LATER |

### 5.9 Analytics (FR-ANL)

Analytics SHALL stay simple and personal. There SHALL be no scoring, ranking, or comparison with other users.

| ID | Requirement | Scope |
|----|-------------|-------|
| FR-ANL-1 | Focus time per day and per week (bar chart). | MVP |
| FR-ANL-2 | Number of completed sessions (per week). | MVP |
| FR-ANL-3 | Most-used techniques (top 5, by session count). | MVP |
| FR-ANL-4 | Flashcards reviewed (per day and per week). | MVP |
| FR-ANL-5 | Cards remembered vs forgotten (proportion, per week and per deck). | MVP |
| FR-ANL-6 | Study activity over time (focus minutes plus cards reviewed, last 4 weeks). | MVP |
| FR-ANL-7 | Review consistency: days with at least one review in the last 30 days, shown as a plain calendar-style grid with no streak counter. | LATER |
| FR-ANL-8 | Range selector: 7 days, 30 days, 90 days. | LATER |
| FR-ANL-9 | Descriptive insights (e.g., "Most of your focus sessions start between 8 and 10 PM."). Insights SHALL be factual and SHALL NOT diagnose or judge. | LATER |
| FR-ANL-10 | Export of the user's data (CSV/JSON). | LATER |

### 5.10 Witty Messaging (FR-MSG)

Pokus retains the SlackOff personality. Messages appear in-page around focus sessions, procrastination moments, and flashcard reviews.

| ID | Requirement | Scope |
|----|-------------|-------|
| FR-MSG-1 | The app SHALL maintain a message library keyed by `category` and `tone`, with multiple messages per pair for variety. | MVP |
| FR-MSG-2 | Tones: **Neutral, Casual, Witty, Filipino, Chaotic**. Default: Witty. | MVP |
| FR-MSG-3 | The user SHALL be able to change tone at any time in Settings. Neutral SHALL contain no jokes. | MVP |
| FR-MSG-4 | Messages SHALL be playful and relatable, MAY be mildly sarcastic, and SHALL NOT insult, shame, or guilt-trip the user. | MVP |
| FR-MSG-5 | The same message SHALL NOT be shown twice in a row in the same category. | MVP |
| FR-MSG-6 | Messages SHALL be in-page only (banners, toasts, empty states, summary text). No push or system notifications in the MVP. | MVP |
| FR-MSG-7 | A "gentle nudge" MAY appear on the Dashboard if the user has opened it and has not started a session for a set period (e.g., 10 minutes). It SHALL be dismissible and SHALL appear at most once per visit. The user SHALL be able to turn nudges off. | MVP |
| FR-MSG-8 | Users MAY submit their own custom messages. | LATER |

**Message categories**

| Category ID | When shown |
|-------------|-----------|
| `dashboard_greeting` | Dashboard load |
| `nudge_start` | Idle nudge (no session started) |
| `session_start` | A session begins |
| `session_mid` | Optional mid-session encouragement |
| `break_start` | Focus phase ends |
| `session_complete` | Summary screen |
| `session_cancel` | User cancels a session |
| `cards_due` | Due cards exist |
| `review_complete` | Flashcard review ends |
| `empty_state` | Empty decks, history, or favorites |

Sample messages per tone are in Appendix B.

### 5.11 Settings (FR-SET)

| ID | Requirement | Scope |
|----|-------------|-------|
| FR-SET-1 | Message tone (Neutral, Casual, Witty, Filipino, Chaotic). | MVP |
| FR-SET-2 | Gentle nudges on/off and idle delay. | MVP |
| FR-SET-3 | Default focus preset and sound on/off. | MVP |
| FR-SET-4 | Theme: light, dark, system. | MVP |
| FR-SET-5 | Flashcards: default "Use spaced repetition" for new decks; "Forgot" reappearance rule (10 minutes or next day). | MVP |
| FR-SET-6 | Account: change password, delete account and data. | MVP |
| FR-SET-7 | Language selection (English first; Filipino UI later). | LATER |

### 5.12 About and Privacy (FR-ABT)

| ID | Requirement | Scope |
|----|-------------|-------|
| FR-ABT-1 | An About page SHALL explain the product's philosophy (no gamification, user control), the evidence labels, and how references are used. | MVP |
| FR-ABT-2 | A Privacy section SHALL state what is stored (account, sessions, decks, cards, reviews, settings), that data is not sold, and that AI generation sends the user's pasted text to a third-party provider. | MVP |

---

## 6. Non-Functional Requirements

| ID | Requirement |
|----|-------------|
| NFR-1 | **Accessibility:** WCAG 2.1 AA target: contrast, keyboard operation, visible focus, semantic HTML, labels for form fields, and reduced-motion support. Status (e.g., due, over) SHALL NOT rely on color alone. |
| NFR-2 | **Responsive:** Usable from 360 px wide phones to desktop. |
| NFR-3 | **Performance:** Main pages load in under 2 seconds on a typical connection; card review interactions respond in under 100 ms. |
| NFR-4 | **Security:** HTTPS, anti-forgery tokens, server-side validation of all inputs, owner-scoped queries, secrets in configuration (never in source control), rate limits on AI endpoints. |
| NFR-5 | **Privacy:** Collect only what the features need. No third-party analytics or ad trackers in the MVP. |
| NFR-6 | **Maintainability:** Business logic in services; components stay thin; core logic (scheduling, aggregation, message picking, recommendation) covered by unit tests. |
| NFR-7 | **Styling:** Hand-written CSS with design tokens (CSS custom properties). No CSS framework. (Carried over from the course requirement of Activity 01; can be revisited later.) |
| NFR-8 | **Content quality:** Technique text SHALL be original wording, not copied from sources. |

---

## 7. Data Model

Types are indicative. All user-owned entities carry `OwnerId` (FK to the Identity user) and are filtered by it.

### 7.1 Entities

| Entity | Purpose | Key fields |
|--------|---------|------------|
| ApplicationUser | Identity user | Id, Email, DisplayName (optional), CreatedAt |
| UserSettings | Per-user preferences | UserId (PK/FK), Tone, NudgesEnabled, NudgeIdleMinutes, DefaultPresetId, SoundEnabled, Theme, DefaultUseSpacedRepetition, ForgotReappearRule |
| Technique | Library entry | Id, Slug, Name, ShortDescription, UsefulFor, HowToUse (ordered list, stored as JSON or child table), SuggestedSessionLength, BestUseCases (list), Limitations (list), Difficulty, EvidenceLevel, EvidenceNote, IsPublished |
| TechniqueCategory | Category lookup | Id, Name, Slug |
| TechniqueCategoryLink | Many-to-many | TechniqueId, CategoryId |
| TechniqueRelation | Related techniques | TechniqueId, RelatedTechniqueId |
| TechniqueSource | References | Id, TechniqueId, Citation, Url (nullable), Year |
| UserFavoriteTechnique | Bookmarks | UserId, TechniqueId, CreatedAt |
| RecommendationRule | Situation to techniques | Id, SituationKey, Label, TechniqueId, Reason, SortOrder |
| FocusPreset | Timer presets | Id, Key, Name, FocusMinutes, BreakMinutes, IsSystem, OwnerId (nullable) |
| FocusSession | A focus session | Id, OwnerId, PresetId (nullable), TechniqueId (nullable), Task, Notes, PlannedFocusSeconds, PlannedBreakSeconds, StartedAt, EndedAt (nullable), PausedTotalSeconds, PausedAt (nullable), Status (`active`, `paused`, `completed`, `cancelled`), ActualFocusSeconds |
| Subject | Top-level grouping | Id, OwnerId, Name |
| Deck | Card collection | Id, OwnerId, SubjectId (nullable), Name, Description, UseSpacedRepetition, CreatedAt |
| Flashcard | A card | Id, OwnerId, DeckId, Front, Back, Tags (list or child table), IsAiGenerated, IsSuspended, CreatedAt, UpdatedAt |
| CardScheduleState | Scheduling state (1:1 with Flashcard) | FlashcardId (PK/FK), Box, DueAt, LastReviewedAt, ReviewCount, LapseCount |
| CardReview | Review history | Id, OwnerId, FlashcardId, ReviewedAt, Result (`knew`, `forgot`), ResponseMs, BoxBefore, BoxAfter |
| AiGenerationLog | Audit and rate-limiting | Id, OwnerId, RequestedAt, InputType (`notes`, `text`, `topic`), InputLength, RequestedCount, ReturnedCount, Succeeded |
| MessageTemplate | Witty message library | Id, Category, Tone, Text, IsActive |

### 7.2 Relationships
- User 1:N Subject, Deck, Flashcard, FocusSession, CardReview, UserFavoriteTechnique.
- Subject 1:N Deck. Deck 1:N Flashcard. Flashcard 1:1 CardScheduleState. Flashcard 1:N CardReview.
- Technique N:M TechniqueCategory. Technique N:M Technique (TechniqueRelation). Technique 1:N TechniqueSource.
- FocusSession N:1 FocusPreset (optional), N:1 Technique (optional).

### 7.3 Data Rules
- DR-1: Deleting a deck SHALL delete its cards, schedule states, and reviews (after confirmation).
- DR-2: Deleting a subject SHALL NOT delete decks; decks become "Unsorted" unless the user confirms deleting them too.
- DR-3: Deleting a technique from the library SHALL be an admin/seed operation only; user favorites are removed with it.
- DR-4: Deleting an account SHALL delete all data owned by the user.
- DR-5: Library data (Technique, TechniqueCategory, TechniqueRelation, TechniqueSource, RecommendationRule, MessageTemplate, system FocusPreset) is seeded through migrations or seed code, and is read-only for regular users.

---

## 8. Pages and Components

### 8.1 Routes
| Route | Page | Auth |
|-------|------|------|
| `/` | Landing (logged out) or Dashboard (logged in) | Public / User |
| `/login`, `/register` | Authentication | Public |
| `/techniques` | Technique library (search, filters, grid) | Public |
| `/techniques/{slug}` | Technique detail | Public |
| `/techniques/favorites` | Favorite techniques | User |
| `/find` | "Find a technique" wizard | Public |
| `/focus` | Focus setup | User |
| `/focus/session` | Active session | User |
| `/focus/summary/{id}` | Session summary | User |
| `/focus/history` | Focus history | User |
| `/flashcards` | Subjects and decks overview | User |
| `/flashcards/decks/{id}` | Deck detail (card list, add/edit) | User |
| `/flashcards/decks/{id}/review` | Review interface | User |
| `/flashcards/review` | Review all due cards | User |
| `/flashcards/generate` | AI generation (input, review drafts, save) | User |
| `/flashcards/search` | Search and filter all cards | User |
| `/analytics` | Analytics | User |
| `/settings` | Settings | User |
| `/about` | About, evidence labels, privacy | Public |

### 8.2 Shared Components
| Component | Purpose |
|-----------|---------|
| `AppShell` / `NavBar` | Layout, navigation, tone-aware greeting |
| `TechniqueCard` | Grid item: name, short description, evidence badge, categories |
| `EvidenceBadge` | Evidence label with tooltip |
| `FilterBar` | Search and filters |
| `FocusTimer` | Countdown display and controls |
| `PresetPicker` | Preset and custom timing |
| `SessionSummary` | End-of-session summary |
| `DeckCard` | Deck tile with counts |
| `FlashcardEditor` | Create/edit form |
| `ReviewCard` | Flip/reveal card with Knew/Forgot buttons |
| `DraftCardList` | Editable AI drafts |
| `BarChart` | CSS/SVG bar chart |
| `MessageBanner` / `Toast` | Witty messages |
| `EmptyState` | Friendly empty views |
| `ConfirmDialog` | Destructive-action confirmation |

---

## 9. Technical Architecture

### 9.1 Stack
| Layer | Choice |
|-------|--------|
| Framework | ASP.NET Core with Blazor Web App (current .NET LTS), Interactive Server render mode |
| Language | C# |
| Data access | Entity Framework Core (code-first migrations) |
| Database | SQLite for development and the student MVP; SQL Server or PostgreSQL if hosted at larger scale |
| Auth | ASP.NET Core Identity (cookie auth) |
| API | REST endpoints only where useful (e.g., AI generation endpoint, future export). Blazor components otherwise call services directly. |
| AI | Provider behind `IFlashcardGenerator`; HTTP call from the server; structured JSON output |
| Styling | Plain CSS with tokens; CSS isolation (`.razor.css`) for components |
| Testing | xUnit for services; optional bUnit for components |
| Hosting | Any ASP.NET Core host (e.g., Azure App Service). Secrets via environment variables or user secrets. |

### 9.2 Solution Structure (kept simple)
```
Pokus/
├─ Pokus.sln
├─ src/Pokus.Web/
│  ├─ Program.cs
│  ├─ Components/ (Layout, Pages, Shared)
│  ├─ Data/ (AppDbContext, Migrations, Seed)
│  ├─ Domain/ (entities, enums)
│  ├─ Services/ (see 9.3)
│  └─ wwwroot/css/
├─ tests/Pokus.Tests/
└─ docs/ (this spec, implementation plans, design links)
```
A single web project is sufficient for the MVP. Splitting into Domain/Application/Infrastructure projects is optional later.

### 9.3 Services
| Service | Responsibility |
|---------|----------------|
| `ITechniqueService` | Query, search, filter techniques; favorites |
| `IRecommendationService` | Situation to techniques |
| `IFocusService` | Start, pause, resume, complete, cancel sessions; history |
| `IDeckService`, `ICardService` | CRUD for subjects, decks, cards; search |
| `IReviewService` | Build review queues; record results |
| `ISchedulingService` | Interval ladder logic (isolated, replaceable) |
| `IFlashcardGenerator` | AI draft generation |
| `IAnalyticsService` | Aggregations for charts |
| `IMessageService` | Pick message by category and tone; avoid immediate repeats |
| `ISettingsService` | Read/write user settings |
| `IClock` | Time abstraction for tests |

### 9.4 Key Technical Decisions
- TD-1: **Timer accuracy:** Persist timestamps, compute remaining time on demand, and tick the UI with a `PeriodicTimer`. Never store a decrementing counter.
- TD-2: **Scheduling isolation:** Only `ISchedulingService` knows interval rules (SR-4).
- TD-3: **AI safety:** Server-side calls, schema validation, input caps, per-user rate limits (FR-AI-7 to FR-AI-10).
- TD-4: **Data seeding:** Technique library and messages stored as JSON/C# seed data and loaded through EF migrations, so content can be edited without code changes to the UI.
- TD-5: **Authorization:** Every service method that touches user data takes the current user's ID and filters by it.

---

## 10. MVP Definition

The first complete release SHALL include:

| Area | Included |
|------|----------|
| Accounts | Register, login, logout, delete account |
| Techniques | Full seeded library (Appendix A), search, category/evidence/difficulty filters, detail pages, related techniques, favorites, bundles |
| Recommendations | "Find a technique" wizard with the 8 seeded situations |
| Focus | All presets and custom timer, technique/task/notes, pause/resume/complete/cancel, summary, history |
| Flashcards | Subjects, decks, manual cards, edit/delete/move, search/filter, active-recall review, Knew/Forgot, review history, optional spaced repetition (interval ladder) |
| AI | Optional generation from notes, text, or topic with controls, editable drafts, save selected |
| Dashboard | Items in Section 5.3 |
| Analytics | Items FR-ANL-1 to FR-ANL-6 |
| Messaging | Five tones, message library, in-page display, gentle nudge |
| Settings and About | As specified |

## 11. Future Features (LATER)

- Email confirmation, password reset, external login, guest mode
- Multi-cycle sessions and auto-advance; browser notifications; custom presets
- Suspend cards, CSV import/export, images on cards, cloze cards, four-level ratings
- Advanced scheduling (SM-2 or FSRS) via the isolated scheduling service
- AI file upload, "explain this card", AI difficulty adjustment
- Review-consistency grid, 30/90-day ranges, descriptive insights, data export
- Private technique notes, technique suggestions, custom messages
- Filipino UI language, PWA installability, shared decks between users

## 12. Delivery Phases

Delivery is split into course activities. Each activity SHALL have its own implementation plan document that (a) lists the spec IDs it implements, simplifies, and defers, and (b) carries its own phases, commit checkpoints, and acceptance checklist.

| Phase | Scope (provisional) | Plan document |
|-------|---------------------|---------------|
| Activity 01 | UI foundation: Figma design plus Blazor/pure-CSS prototype with seeded in-memory data (technique library, focus timer, flashcard basics, messaging) | `Implementation_Plan_Activity_01.md` |
| Next activities | To be defined once briefs are available (candidates: database and Identity, persistent flashcards and review, AI generation, analytics) | One plan per activity |

---

## Appendix A — Technique Catalog (seed list)

Difficulty: E = easy, M = moderate, A = advanced. Evidence: RS = research-supported, CU = commonly used method, PH = practical heuristic, ML = mixed or limited.

| # | Technique | Categories | Evidence | Suggested length | Diff. |
|---|-----------|-----------|----------|------------------|-------|
| 1 | Pomodoro Technique | Focus, Time management | CU | 25 min focus / 5 min break | E |
| 2 | 52/17 Method | Focus, Time management | ML | 52 min / 17 min | E |
| 3 | Timeboxing | Time management, Planning | CU | Fixed box, e.g., 30–90 min | E |
| 4 | Time Blocking | Planning, Time management | CU | Day/week planning | M |
| 5 | Deep Work | Focus, Productivity | CU | 60–90 min blocks | A |
| 6 | Flow State Techniques | Focus | ML | 30–90 min | M |
| 7 | Two-Minute Rule | Productivity, Planning | PH | 2 min | E |
| 8 | Getting Things Done (GTD) | Planning, Productivity | CU | Ongoing system; weekly review | A |
| 9 | Eat the Frog | Productivity, Planning | PH | First work block of the day | E |
| 10 | Parkinson's Law | Time management | PH | Applied per task | E |
| 11 | Ultradian Rhythm Sessions | Focus, Time management | ML | 90 min / 20 min | M |
| 12 | Single-Tasking | Focus, Productivity | PH | Any block | E |
| 13 | Batching | Productivity, Time management | PH | 30–60 min batches | E |
| 14 | Implementation Intentions | Planning, Productivity | RS | Setup: 2–5 min | E |
| 15 | WOOP | Planning | RS | Setup: 5–10 min | M |
| 16 | 5-Minute Rule | Focus, Productivity | PH | 5 min to start | E |
| 17 | Distraction Management | Focus | PH | Setup: 5–10 min | E |
| 18 | Task Chunking | Planning, Productivity | PH | Planning: 10–15 min | E |
| 19 | Active Recall | Studying, Memory, Exam preparation | RS | 20–40 min | E |
| 20 | Spaced Repetition | Studying, Memory, Exam preparation | RS | Short daily reviews (10–20 min) | M |
| 21 | Feynman Technique | Studying | ML | 20–40 min per concept | M |
| 22 | Interleaving | Studying, Exam preparation | RS | 30–60 min mixed practice | M |
| 23 | Elaborative Interrogation | Studying | RS | 15–30 min | M |
| 24 | Dual Coding | Studying, Memory, Note-taking | RS | 15–30 min | M |
| 25 | Retrieval Practice | Studying, Memory | RS | 15–30 min | E |
| 26 | Blurting | Studying, Exam preparation | ML | 10–20 min | E |
| 27 | Leitner System | Memory, Studying | CU | Daily box review | E |
| 28 | Cornell Note-Taking | Note-taking, Studying | ML | During lecture and 10 min review | E |
| 29 | Method of Loci | Memory | RS | 15–30 min setup | A |
| 30 | Self-Explanation | Studying | RS | Throughout study | M |
| 31 | Practice Testing | Studying, Exam preparation | RS | 30–60 min | E |

Notes:
- "Practice Testing" and "Retrieval Practice" overlap heavily and SHALL cross-link. Active Recall is the everyday name for the same core idea.
- Each entry's `EvidenceNote` should justify the label. Examples: Implementation Intentions is supported by meta-analytic evidence for goal attainment; Method of Loci is supported mainly for memorizing ordered lists; Pomodoro is widely used but has little direct controlled evidence; 52/17 originates from workplace time-tracking data, not peer-reviewed trials; Cornell note-taking evidence is mixed; Feynman Technique is popular and related to self-explanation, but has limited direct testing.
- Additional techniques can be added later (e.g., SQ3R, mind mapping, concept maps, study groups, body doubling).

## Appendix B — Sample Messages by Tone

All samples SHALL comply with FR-MSG-4 (playful, never insulting or guilt-tripping).

| Category | Neutral | Casual | Witty | Filipino | Chaotic |
|----------|---------|--------|-------|----------|---------|
| `nudge_start` | Ready to start a session? | Want to start something small? | Five minutes. That's all we're asking. Probably. | Simulan na natin? Kahit 5 minutes lang. | TIMER. BUTTON. YOU. LET'S GO. |
| `nudge_start` (2) | Your notes are open. | Your notes are right there. | Your notes are still open. They're starting to feel ignored. | Bukas pa 'yung notes mo, boss. | YOUR NOTES ARE LOOKING AT YOU. |
| `dashboard_greeting` | Welcome back. | Hey, welcome back. | Congratulations, you opened the study page. Now the hard part. | Welcome back! Tara, aral. | YOU'RE HERE! THAT'S HALF THE BATTLE (IT'S NOT). |
| `session_start` | Session started. | Alright, timer's on. | Timer's running. Your phone can wait. | Sige, simulan na. | GO GO GO (CALMLY). |
| `break_start` | Focus phase complete. Take a break. | Nice, break time. | Break time. Hydrate. Stretch. Not TikTok. | Break muna! Inom ng tubig. | BREAK! BODY! WATER! |
| `session_complete` | Session complete. | Nice work, that's a wrap. | Your deadline called back. It sounds relieved. | Tapos na! Ayos 'yan. | YOU DID A THING. |
| `session_cancel` | Session cancelled. | No worries, we can try again. | Cancelled. The timer will pretend it didn't see that. | Sige, balik tayo mamaya. | ABORTED. NO SHAME. |
| `cards_due` | 12 cards are due. | You've got 12 cards waiting. | 12 cards are due. They've been very patient. | May 12 cards na naghihintay. | 12 CARDS. THEY'RE LINING UP. |
| `review_complete` | Review complete. | Nice, that's the deck done. | Deck cleared. Your brain earned a snack. | Tapos na ang review! | DECK. DEFEATED. (HUMBLY.) |
| `empty_state` | No sessions yet. | Nothing here yet. | Nothing here. It's like a fresh notebook. | Wala pa dito. Simulan na natin. | EMPTY! FULL OF POTENTIAL! |

## Appendix C — Reference Seeds (verify before publishing)

These are starting references for `TechniqueSource` entries. The maintainer SHALL confirm each citation against the original source (EV-4).

| Topic | Reference |
|-------|-----------|
| Learning techniques review (practice testing, distributed practice, interleaving, elaborative interrogation, self-explanation, and others) | Dunlosky, J., et al. (2013). Improving Students' Learning With Effective Learning Techniques. Psychological Science in the Public Interest, 14(1). |
| Implementation intentions | Gollwitzer, P. M. (1999). Implementation intentions: Strong effects of simple plans. American Psychologist. Also Gollwitzer & Sheeran (2006) meta-analysis. |
| WOOP / mental contrasting | Oettingen, G. (2014). Rethinking Positive Thinking. Also woopmylife.org. |
| Pomodoro | Cirillo, F. The Pomodoro Technique (originator's materials). |
| Deep Work | Newport, C. (2016). Deep Work. |
| GTD and Two-Minute Rule | Allen, D. (2001). Getting Things Done. |
| Eat the Frog | Tracy, B. (2001). Eat That Frog! |
| Parkinson's Law | Parkinson, C. N. (1955). Parkinson's Law. The Economist. |
| Flow | Csikszentmihalyi, M. (1990). Flow: The Psychology of Optimal Experience. |
| Ultradian rhythms | Kleitman, N. (basic rest-activity cycle concept); popularized in productivity writing. |
| Dual coding | Paivio, A. (dual coding theory); Mayer, R. E. (multimedia learning). |
| Leitner System | Leitner, S. (1972). So lernt man lernen. |
| Cornell Notes | Pauk, W. (How to Study in College). |
| Feynman Technique | Popular framework attributed to Richard Feynman's teaching style; no single primary study. |
| Method of loci | Classical mnemonic; modern reviews in memory-training research. |
