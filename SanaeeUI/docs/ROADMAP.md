# ROADMAP.md

## Overview

This roadmap tracks the implementation of the token-driven Egyptian-inspired design system. Priorities are arranged so the app remains functional at every step, with minimal risk.

---

## Phase 1: Theme & Tokens (Done)

- [x] `SanaeeBrand.cs` — flat token set (Light/Dark), shape/typography constants
- [x] `SanaeeTheme.cs` — MudBlazor theme mapping + `CssVariables(SanaeeColorSet c)`
- [x] `sanaee.css` — CSS custom properties consumed everywhere; no hard-coded brand colors
- [x] `MainLayout.razor` — emits CSS vars inline, uses `SanaeeTheme`, gold-trimmed appbar, richer TopBar

---

## Phase 2: Shell, Breadcrumbs & 404 (Done)

- [x] `Breadcrumbs.razor` — route→labels, Dashboard hidden logic
- [x] `NavMenu.razor` + `NavMenu.razor.css` — brand badge + gold-subtitle, group styling
- [x] `NotFound.razor` + server `UseStatusCodePagesWithReExecute("/not-found")`
- [x] TopBar — global search (navigates to `/orders?q=...`), Demo/Live toggle+chip, notifications, theme toggle, profile avatar menu

---

## Phase 3: Component Kit (Done)

- [x] `SectionCard`
- [x] `StatusChip`
- [x] `EntityAvatar`
- [x] `MoneyText`
- [x] `RatingStars`
- [x] `EmptyState`
- [x] `FilterBar`
- [x] `TableSkeleton`
- [x] `ConfirmDialog`
- [x] `DetailRow` updated to use `.detail-row`
- [x] `sanaee-table` class applied across all tables

---

## Phase 4: Page Retrofits (In Progress)

- [x] Dashboard — uses token-driven hero; StatCards present
- [x] Orders — MoneyText + EmptyState
- [x] Offers — MoneyText + EmptyState
- [x] Customers — EntityAvatar + RatingStars; unused initials removed
- [x] Providers — EntityAvatar + RatingStars (null-safe)
- [x] Users — EntityAvatar; initials removed
- [ ] Documents — EmptyState (optional), minor polish
- [ ] Applications — EmptyState
- [ ] ProviderStatus — timeline polish
- [ ] Reviews — RatingStars
- [ ] Categories — card polish
- [ ] ProviderCategories — polish
- [ ] CancellationReasons — polish
- [ ] Wallets — MoneyText
- [ ] Transactions — MoneyText (signed)
- [ ] Payments — MoneyText
- [ ] OrderHistory — polish
- [ ] Addresses — polish
- [ ] Phones — polish
- [ ] Audit — polish

---

## Phase 5: Polish & Production Readiness (Pending)

- [ ] Consistent `EmptyState` on all list pages
- [ ] Use `MoneyText Signed="true"` for transaction amounts (red/green in CSS)
- [ ] TopBar global search — actually filter current page (or keep nav-to-orders)
- [ ] NotFound: include quick links to Dashboard/Orders
- [ ] Command palette (Ctrl+K) for quick navigation (optional)
- [ ] EntityDrawer pattern (side sheet) for heavy detail views (vs dialogs) — optional
- [ ] Accessibility (focus states, alt text, aria labels)
- [ ] Responsive tweaks (Drawer behavior on small screens)
- [ ] Dark mode visual QA
- [ ] RTL/Arabic support (phase-6 candidate) — English only now
- [ ] PWA metadata (manifest/service worker) if needed (was "Web/PWA only")
- [ ] Error boundary polish
- [ ] Light/dark transition + reduced motion respect
- [ ] Remove dead code (Initials methods) — mostly done
- [ ] Docs cross-links

---

## Build & Deployment

- [x] Build: `dotnet build SanaeeUI.sln` — green
- [x] Smoke test: all 21 routes return 200 (SSR)
- [ ] Commit to fork `YossefKhalil/Sanaee-Delivery-UI-UX-` with clear messages
- [ ] Push to fork (gh not installed; use git)
- [ ] Optional: run under https profile or attach to API (Live mode)

---

## Known notes / technical debt

- **Grid:** Using `MudTable` (with `.sanaee-table` styling) instead of `MudDataGrid` wrapper. Implementing a fully generic MudDataGrid wrapper is higher-risk for 21 pages; keep as-is for stability. Can revisit later.
- **Server log:** benign `Failed to determine the https port for redirect` in http profile.
- **SignalR:** Hub `/hubs/chat` exists; chat is UI-only for now (no real-time send). 
- **Live API:** Placeholder client present (`SanaeeApiClient`); demo is fully functional.
- **Theme injection:** CSS vars emitted inline in `MainLayout` (SSR-friendly). This is one source of truth in C#.

---

## Validation checklist

- [x] Single source of truth: tokens only in `SanaeeBrand.cs`
- [x] No brand hex colors in `sanaee.css`/components except via vars
- [x] Egyptian palette (teal+gold) applied across shell/hero
- [x] All tables have `sanaee-table` class, consistent spacing
- [x] All routes build and serve
- [ ] Docs written (DESIGN-SYSTEM.md, PAGES.md, ROADMAP.md) — in progress
- [ ] Commit & push done

End of ROADMAP.md.