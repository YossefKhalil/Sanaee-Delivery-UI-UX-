# PAGES.md

## UI/UX Spec — All 21 Routes (Full Detail)

The pages follow a consistent pattern: **Breadcrumbs → PageHeader → (KPI row) → FilterBar → Table → Detail drawer/dialogs**, with **loading/empty/error** states. Data is fetched via `ISanaeeDataService` (Demo by default, Live via API toggle).

---

## 1. `/` — Dashboard

**Purpose:** Operations overview (orders, providers, customers, KYC, revenue, wallet liability, ratings).

**Layout:** Hero + KPI grid + Charts grid (responsive: 12/12-md).
- **Hero** (`hero-card`): title + subtitle (counts), gross revenue, avg rating. Egyptian motif subtle (radial dots).
- **KPIs (MudGrid 3/4)**: Total Orders, In Progress, Completed, Cancelled, Providers (online), Customers, Pending Documents, Wallet Liability. Each uses `StatCard`.
- **Charts**: Line (Revenue last 7d), Donut (Orders by status), Bar (Orders by category), Bar (Cancellation reasons top 6).

**Filters:** None (or date-range 7/30d in future).

**Data mapping:** `Data.GetDashboardStatsAsync()` → `DashboardStats` (with trend, buckets).

**States:** Skeleton grid while null.

**Notes:** Drill-through: Pending Documents → `/documents`, orders buckets → `/orders`, cancellation reasons → `/cancellation-reasons`.

---

## 2. `/orders` — Orders

**Purpose:** Service requests, bids and fulfilment (lifecycle).

**Layout:** Header + FilterBar + `MudTable` (sanaee-table) + detail drawer/dialog.

**Header:** `PageHeader` Title "Orders", Subtitle "Service requests, bids and fulfilment", Count `X / Y`, Action `+ New order` (opens `CreateOrderDialog`).

**Filters (FilterBar):**
- Search (customer/category/problem) — immediate, clearable
- Status select (all `OrderStatus` values)
- Category select (distinct from list)

**Table columns:**
- `#` Id
- Request: problem text (2-line truncate) + `MudChip` Category
- Customer: name + address (secondary)
- Status: chip (uses `StatusFormat.ToColor()/ToIcon()`)
- Offers: count
- Total: `MoneyText Amount="@context.AcceptedOfferTotal"`
- Created: `dd MMM, HH:mm`
- Actions: `Visibility` icon → open detail

**Detail:** `OrderDetailsDialog` (medium/full-width). Tabs/sections: Request, Offers list (accept/reject if pending), Status timeline, Details panel, Fulfilment actions (Start/Complete/Cancel with reason+actor). Accepting offer refreshes data.

**Actions:** Create order (categories+addresses+customers). Cancel requires reason from `CancelReasons` (customer/provider/admin applicable).

**States:** Loading, NoRecords → `EmptyState` ("No orders found").

**API mapping:** list `/orders/admin` (via service), actions accept/reject/start/complete/cancel.

---

## 3. `/offers` — Offers

**Purpose:** Provider bids across orders.

**Layout:** Header + filters + table.

**Header:** Title "Offers", Subtitle "Provider bids across all service orders", Count, Action `Submit offer` (`CreateOfferDialog`).

**Filters:** Search (provider/order id), Status (`OfferStatus`), Order (distinct order ids).

**Table:** #, OrderId, Provider, Labor, Materials, Total (`MoneyText`), Arrival (dd MMM HH:mm), Status chip, Expires (dd MMM HH:mm).

**Detail:** Can open related order. Submit offer requires Providers (active) + Orders (pending).

**States:** `EmptyState` ("No offers found").

**API mapping:** `GetOffersAsync`, `CreateOfferAsync`.

---

## 4. `/chat` — Messages

**Purpose:** Order-scoped conversations.

**Layout:** Master-detail (md=4 threads, md=8 thread). `MudGrid`.

**Threads (left):** List by OrderId; each item: Avatar, Order #, last message preview (truncate 2), time, unread badge (Count). Select binds `_selectedOrder`.

**Thread (right):** Header shows Order # + category/customer/status. Scroll area with message bubbles: self vs other (align based on sender vs first non-system). System messages italic; Location type shows icon. Timestamps + read receipt. Composer: text input + mic/attach/send (UI-only demo; real-time via SignalR hub `/hubs/chat` per group `Order_{id}`).

**States:** Empty conversation text. Loading not required (small demo set).

**API mapping:** `GetMessagesAsync`, grouped by OrderId.

---

## 5. `/customers` — Customers

**Purpose:** Registered customers posting orders.

**Layout:** Header + filter + table.

**Header:** Title "Customers", Count.

**Filters:** Search (name/email/phone).

**Table:** Customer (EntityAvatar + name), Email, Phone (mono), OrdersCount, Rating (`RatingStars` or "No reviews"), Status (Active/Disabled chip), Joined (dd MMM yyyy).

**Detail (drawer recommended):** Profile, Orders list, Addresses, Wallets (future).

**States:** Loading, table styled `.sanaee-table`.

**API mapping:** `GetCustomersAsync`.

---

## 6. `/providers` — Providers

**Purpose:** Service professionals.

**Layout:** Header + filters + table.

**Filters:** Search (name/national number), Status (`ProviderStatus`), Online only (toggle).

**Table:** Provider (EntityAvatar + name), National ID (mono), Skills (CategoriesCount), Rating (`RatingStars` + count), Completed, Status, Availability (toggle → `SetProviderAvailabilityAsync`), View (opens `ProviderDetailsDialog`).

**Detail:** `ProviderDetailsDialog` — Profile, Skills, KYC Documents (with statuses), Recent reviews.

**States:** Snackbar on availability toggle.

**API mapping:** providers, documents, provider-categories, reviews.

---

## 7. `/users` — Users

**Purpose:** Unified accounts (customers/providers/admins).

**KPIs:** Customers, Providers, Administrators (StatCards).

**Filters:** Search name, Type (`UserType`).

**Table:** User (EntityAvatar + name), Id, Type chip, Contact, Detail, Status (Active/Disabled).

**Detail:** Activate/deactivate.

**Data:** Built from customers + providers + single admin entry (no dedicated `GetUsersAsync` endpoint in UI facade; derived).

---

## 8. `/documents` — Provider Documents (KYC)

**Purpose:** KYC verification (NationalId, CriminalRecord, WorkPermit, PersonalPhoto).

**KPIs:** Pending, Approved, Rejected, Re-upload.

**Filters:** Search provider, Status (`DocumentStatus`).

**Table:** #, Provider, Type, Status chip, Document (View), Reviewed by (changedBy + date), Actions (Approve/Reject if pending/reupload). Reject opens `ReasonDialog`.

**Actions:** `ApproveDocumentAsync`, `RejectDocumentAsync(id, reason)`.

**States:** Snackbar on actions.

---

## 9. `/applications` — Applications

**Purpose:** Provider onboarding review history.

**Filters:** Search provider, Status (`ApplicationStatus`).

**Table:** #, Provider, Status, Notes, Changed by, Changed at (dd MMM yyyy HH:mm).

**API mapping:** `GetApplicationHistoriesAsync`.

---

## 10. `/provider-status` — Provider Status Log

**Purpose:** Activation/suspension history.

**Layout:** Vertical `MudTimeline` (or table). Filters: Search, Status (`ProviderStatus`).

**Items:** Provider — Status, Notes, ChangedBy · datetime.

**API mapping:** `GetProviderStatusHistoriesAsync`.

---

## 11. `/reviews` — Reviews

**Purpose:** Mutual ratings.

**KPIs:** Average (0.0), 5-star, Low (≤2★).

**Filters:** Search (reviewer/reviewee/comment), min rating (MudRating).

**Table:** Reviewee, Reviewer, Rating (`RatingStars`), Comment (truncate 2, shows "edited" chip), Order, Date.

**API mapping:** `GetReviewsAsync`.

---

## 12. `/categories` — Categories

**Purpose:** Service taxonomy.

**Layout:** Responsive card grid (sm 6 md 4 lg 3). Filter: search by name.

**Cards:** Icon, Name, top-level/child badge, Description (2-line truncate), Providers count chip, Active/Disabled chip.

**API mapping:** `GetCategoriesAsync`.

---

## 13. `/provider-categories` — Provider Skills

**Purpose:** Provider↔Category mapping with years of experience.

**Filters:** Search provider/category, Category select.

**Table:** Provider, Category chip, Experience (yrs).

**API mapping:** `GetProviderCategoriesAsync`.

---

## 14. `/cancellation-reasons` — Cancellation Reasons

**Purpose:** Reason catalog by actor.

**Filters:** Search, Applies to (`UserType`).

**Table:** #, Reason, Applies to chip, Status (Active/Disabled).

**API mapping:** `GetCancelReasonsAsync`.

---

## 15. `/wallets` — Wallets

**Purpose:** Balances per user.

**KPIs:** Total balance (`MoneyText`/sum), Active, Frozen.

**Filters:** Search user.

**Table:** Wallet #, User, User id, Balance (`MoneyText`), Status chip.

**Detail:** Wallet transactions drawer (future).

**API mapping:** `GetWalletsAsync`.

---

## 16. `/transactions` — Transactions

**Purpose:** Ledger movements.

**KPIs:** Deposits, Withdrawals, Commission, Refunds.

**Filters:** Search description, Type (`TransactionType`).

**Table:** #, Wallet #, Type chip, Description, Amount signed (color by sign), Order (# or —), Date (dd MMM yyyy HH:mm).

**API mapping:** `GetTransactionsAsync`.

---

## 17. `/payments` — Payments

**Purpose:** Collection attempts.

**KPIs:** Collected, Pending, Failed, Refunded (sums).

**Filters:** Search user/reference, Status (`PaymentStatus`), Method (`PaymentMethod`).

**Table:** #, User, Amount (`MoneyText`), Method chip, Type, Status chip, Reference (mono), Created (dd MMM HH:mm).

**API mapping:** `GetPaymentsAsync`.

---

## 18. `/order-history` — Order Timeline

**Purpose:** Lifecycle transitions per order.

**Filters:** Order select (distinct), Status (`OrderStatus`).

**Table:** Order #, Status chip+icon, Notes, Reason, By, When (dd MMM yyyy HH:mm).

**API mapping:** `GetOrderStatusHistoriesAsync`.

---

## 19. `/addresses` — Addresses

**Purpose:** Saved customer locations.

**Filters:** Search city/street, City select.

**Table:** #, UserId, Title, City, Region, Street, Building (building/apartment), Default chip, Map (pin disabled if coords null).

**API mapping:** `GetAddressesAsync`.

---

## 20. `/phones` — Phone Numbers

**Purpose:** Registered contact numbers.

**Filters:** Search phone or user id.

**Table:** #, User id, Phone number (mono), Default chip.

**API mapping:** `GetPhonesAsync`.

---

## 21. `/audit` — Audit Logs

**Purpose:** Administrative actions.

**Filters:** Search performer/reason, Entity (`EntityName`), Action (`ActionType`).

**Table:** #, Entity chip, Entity id, Action chip, Reason, Performed by, Self (icon), IP (mono), When (dd MMM yyyy HH:mm).

**API mapping:** `GetAuditLogsAsync`.

---

## Design rules applied

- Every table uses `Class="sanaee-table"`, `Elevation="0"`, Hover/Striped.
- Empty states via `EmptyState`.
- Money via `MoneyText` (tabular). Ratings via `RatingStars` (gold-tinted). Avatars via `EntityAvatar`.
- Filters wrapped in `FilterBar`.
- Status badges remain semantic (existing `StatusFormat` colors preserved).
- Loading: skeletons on Dashboard; table `Loading` flag elsewhere.
- Responsive breakpoints: Md for most lists, Sm for compact grids.

End of PAGES.md.