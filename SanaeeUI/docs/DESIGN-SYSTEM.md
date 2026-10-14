# DESIGN-SYSTEM.md

## Overview

The Sanaee Delivery UI follows a **modular, token-driven** design system built on top of **MudBlazor**. The look & feel is **Egyptian-inspired** (Nile teal + antique gold, with a warm sand background) but kept enterprise-friendly for an admin/operations UI.

Everything is centralized in `SanaeeUI.Client.Theme/` so you can re-skin the entire app by editing a single file. **No color values are hard-coded** outside the token set.

---

## 1. Design goals

- **Easy to modify**: All colors/shape live in a single, flat token set (`SanaeeColorSet`) and are exposed as both (a) MudBlazor palettes and (b) CSS custom properties (`--sanaee-*`).
- **Predictable & consistent**: Shared components enforce spacing, radius, status chips, empty states, and table styling.
- **Responsive**: Shell (TopBar + Drawer) adapts between desktop/mobile; pages use the same filter/table/detail pattern.
- **Theme-aware**: Light/Dark modes share the token structure. The current active set is emitted inline into the document by `MainLayout`.
- **English-only for now**: `RightToLeft`/Arabic is noted in ROADMAP.

---

## 2. Token source of truth

### File: `SanaeeUI.Client/Theme/SanaeeBrand.cs`

- `SanaeeBrand.FontFamily`, `BorderRadius`, `CardRadius` — base shape/typography
- `SanaeeBrand.Light` — instance of `SanaeeColorSet` for light mode
- `SanaeeBrand.Dark` — instance of `SanaeeColorSet` for dark mode
- `SanaeeBrand.Name` — theme name (display only)

**To re-skin:** change values in `Light` and `Dark`. No other file should need edits for colors.

### File: `SanaeeUI.Client/Theme/SanaeeTheme.cs`

- `Create()` — builds a `MudTheme` mapping `SanaeeColorSet` → `PaletteLight`/`PaletteDark` and `LayoutProperties`/`Typography`.
- `Current(bool dark)` — returns the active token set.
- `CssVariables(SanaeeColorSet c)` — returns a string of `:root { --sanaee-* ... }` variables. Emitted by `MainLayout.razor` via `<style>...</style>` so **CSS always reads from a single source**.

### CSS: `SanaeeUI/wwwroot/sanaee.css`

Uses `var(--sanaee-*)` with reasonable fallbacks. It defines layout helpers, the Egyptian-themed hero (subtle radial pattern + gold hairline), stat cards, filter-bar, empty-state, `.sanaee-table`, detail-row styles, etc. No hex colors for the brand live here.

---

## 3. Color palette (Egyptian-inspired)

| Token | Light | Dark | Purpose |
|---|---|---|---|
| Primary | `#0B6E6E` | `#35BDBD` | Nile teal — primary actions, nav active, brand |
| OnPrimary | `#FFFFFF` | `#04302F` | Text on primary |
| Secondary | `#C9A227` | `#E3C766` | Antique gold — accents only (hero trim, badges, rating gold) |
| OnSecondary | `#2A2200` | `#2A2200` | Text on gold |
| Tertiary | `#1E5FA8` | `#5C8FE0` | Lapis — informational accents |
| OnTertiary | `#FFFFFF` | `#06182F` | Text on tertiary |
| Background | `#F5F0E6` | `#0E1214` | Warm sand background |
| Surface | `#FFFFFF` | `#161B1D` | Cards, tables, dialogs |
| Drawer | `#FFFFFF` | `#12171A` | Sidebar background |
| Appbar | `#0B6E6E` | `#0B2B2B` | Top bar (gold hairline below) |
| AppbarText | `#FFFFFF` | `#EAF7F7` | Appbar text/icon color |
| TextPrimary | `#211E19` | `#EDE7DA` | Primary body text |
| TextSecondary | `#6B6355` | `#A99F8C` | Secondary text, captions |
| Border | `#E7DFCF` | `#2A2F31` | Lines, dividers, card borders |
| Success/Warning/Error/Info | `#2E7D32`/`#B26B00`/`#C0392B`/`#1E5FA8` | `#3DCB6C`/`#E3A93E`/`#FF6B5C`/`#5C8FE0` | Semantic states |
| Gold/Sand | `#C9A227`/`#F5F0E6` | `#E3C766`/`#0E1214` | Brand tokens exposed as vars |

**Usage note:** Gold is used very sparingly. The top bar has a subtle `inset 0 -2px 0 var(--sanaee-gold)` hairline; rating stars get `.rating-gold`; the hero card has a gold radial highlight. Primary drives most actions.

---

## 4. Layout & Shell

### `MainLayout.razor` (server-side layout)
- Injects CSS vars inline: `<style>@_cssVariables</style>`
- `MudThemeProvider` with `SanaeeTheme.Create()` and `IsDarkMode` state
- `MudAppBar`: deep-teal background (`var(--sanaee-appbar)`), gold bottom hairline, brand "Sanaee Delivery", Demo/Live chip+toggle, global search (navigates to `/orders?q=...` on Enter), notifications menu, theme toggle, profile avatar menu
- `MudDrawer`: width 272px, brand badge with gold-subtitle, grouped `MudNavMenu`
- `MudMainContent`: has `<Breadcrumbs/>` then `@Body`. Uses sand background with a very subtle radial glow.
- `Breadcrumbs.razor`: maps route segments → labels (see labels map in component). Returns empty on `/`.

### `NavMenu.razor` + `NavMenu.razor.css`
- Brand shows icon badge + "Sanaee Delivery" + `HOME SERVICES PLATFORM` (gold-colored small text)
- 7 groups: Marketplace, People, KYC & Trust, Catalog, Finance, Operations, System. All use Material icons.
- Active links get teal-tinted background (`color-mix(in srgb, var(--sanaee-primary) 14%, transparent)`)

### `NotFound.razor`
Route `/not-found`; shown via `UseStatusCodePagesWithReExecute("/not-found")` in server `Program.cs`.

---

## 5. Shared component kit (`SanaeeUI.Client.Shared/`)

All components are simple, token-aware. Many rely on `sanaee.css` classes.

| Component | Purpose | Props (key) |
|---|---|---|
| `PageHeader` | Page title + subtitle + optional count chip + actions | `Title`, `Subtitle`, `Count`, `Actions` |
| `SectionCard` | Consistent card wrapper with header+actions+subtitle | `Title`, `Subtitle`, `Actions`, `ChildContent`, `Elevation` (0 default), `Class` |
| `StatCard` | KPI tile with icon + hover-lift + optional caption | `Title`, `Value`, `Icon`, `Color`, `Caption` |
| `StatusChip` | Neutral chip for text (can be extended to map enums) | `Text`, `Color`, `Icon`, `Size`, `Variant`, `Class` |
| `EntityAvatar` | Initials avatar; uses teal background by default | `Name`, `Color`, `Size`, `Variant` |
| `MoneyText` | Tabular EGP display (null → "—"); optional `Signed` | `Amount`, `Currency`, `Signed`, `ColorCss` |
| `RatingStars` | Gold-tinted read-only stars + count/value | `Value` (double), `Count`, `ShowValue`, `Size` |
| `DetailRow` | Label/value pair with subtle dashed divider | `Label`, `Value` |
| `EmptyState` | Consistent empty UI (icon, title, message, optional action) | `Icon`, `Title`, `Message`, `Action` |
| `FilterBar` | Rounded border container for filter controls | `FilterContent` |
| `TableSkeleton` | Lightweight table-like skeleton | `Columns`, `Rows` |
| `ConfirmDialog` | Generic confirm/cancel dialog | `Title`, `Message`, `ConfirmText`, `CancelText`, `ConfirmColor` |
| `CreateOrderDialog`, `CreateOfferDialog`, `OrderDetailsDialog`, `ProviderDetailsDialog`, `ReasonDialog` | Entity/action dialogs (existing, kept) |

**Global usage norms:**
- Filter blocks → `FilterBar`
- Tables → `MudTable` with `Class="sanaee-table"` (no Elevation 1; subtle)
- No-records → `EmptyState`
- Money/ratings → `MoneyText`/`RatingStars`
- Avatars → `EntityAvatar`

---

## 6. Theming in action (implementation notes)

- `_Imports.razor` (both) don't need Theme usings — components use it implicitly or the namespace is included where needed. MainLayout has `@using SanaeeUI.Client.Theme`.
- `_appbarStyle` forces the AppBar background to `var(--sanaee-appbar)` and adds gold hairline; inline style overrides MudBlazor's default color class.
- `sanaee.css` uses `color-mix` (modern; fine for .NET 10 target).
- The theme is **modular**: to create "gold/terracotta" variant, copy `SanaeeBrand.Light/Dark` to a new set and switch `SanaeeTheme.Current(...)` — minimal change.

---

## 7. Example: how to add a new component

1. Create `SanaeeUI.Client/Shared/MyWidget.razor`
2. Use CSS vars: `background:var(--sanaee-surface); color:var(--sanaee-text); border:1px solid var(--sanaee-border); border-radius:var(--sanaee-card-radius);`
3. Reuse existing primitives (`SectionCard`, `MoneyText`) when possible
4. Add docs notes if needed

No new hard-coded hexes.

---

## Conventions

- Keep tokens flat (don't nest MudBlazor types in the public token API).
- Prefer token vars over inline Mud palette magic.
- Use `tabular-nums` for all numeric/currency displays.
- Tables: striped + hover; zebra color from `--sanaee-primary` with alpha.
- Empty states are informative, not alarming.

End of DESIGN-SYSTEM.md.