using SanaeeUI.Client.Models;

namespace SanaeeUI.Client.Services;

// Deterministic in-memory dataset that mirrors the shape of the Sanaee-Delivery
// database. It lets the UI/UX be demonstrated without a running SQL Server +
// seeded backend. Switching to the live API replaces this with real HTTP data.

public static class DemoData
{
    private static readonly string[] Categories =
    {
        "Plumbing", "Electrical", "Carpentry", "AC Maintenance", "Painting",
        "Appliance Repair", "Cleaning", "Moving & Delivery"
    };

    private static readonly string[] Cities = { "Cairo", "Giza", "Alexandria", "Mansoura", "Tanta" };
    private static readonly string[] Regions = { "Nasr City", "Maadi", "Heliopolis", "Dokki", "Smouha" };
    private static readonly string[] FirstNames = { "Ahmed", "Mohamed", "Mostafa", "Youssef", "Omar", "Khaled", "Sara", "Nour", "Mona", "Hassan", "Ali", "Salma", "Karim", "Hoda", "Tarek", "Rania" };
    private static readonly string[] LastNames = { "Hassan", "Ibrahim", "Mahmoud", "Said", "Fathy", "Adel", "Sami", "Gamal", "Nabil", "Zaki" };
    private static readonly string[] Problems =
    {
        "Kitchen sink is leaking badly under the cabinet",
        "Power outage in the living room, breaker keeps tripping",
        "Air conditioner not cooling, making loud noise",
        "Need to repaint the apartment, two bedrooms",
        "Washing machine stops mid-cycle with error code",
        "Bedroom door is jammed and won't close properly",
        "Water heater not working, no hot water",
        "Need help moving furniture to a new flat",
        "Fridge freezing over, thermostat issue",
        "Ceiling fan wobbling and needs mounting fix"
    };

    private static readonly Random Rng = new(20261014);

    public static List<OrderListItem> Orders { get; } = BuildOrders();
    public static List<OfferListItem> Offers { get; } = BuildOffers();
    public static List<ProviderListItem> Providers { get; } = BuildProviders();
    public static List<CustomerListItem> Customers { get; } = BuildCustomers();
    public static List<CategoryListItem> CategoriesList { get; } = BuildCategories();
    public static List<CancelReasonListItem> CancelReasons { get; } = BuildCancelReasons();
    public static List<ProviderDocumentListItem> Documents { get; } = BuildDocuments();
    public static List<WalletListItem> Wallets { get; } = BuildWallets();
    public static List<WalletTransactionListItem> WalletTransactions { get; } = BuildWalletTransactions();
    public static List<PaymentListItem> Payments { get; } = BuildPayments();
    public static List<ReviewListItem> Reviews { get; } = BuildReviews();
    public static List<AddressListItem> Addresses { get; } = BuildAddresses();
    public static List<MessageListItem> Messages { get; } = BuildMessages();
    public static List<PhoneListItem> Phones { get; } = BuildPhones();
    public static List<ProviderCategoryListItem> ProviderCategories { get; } = BuildProviderCategories();
    public static List<ApplicationHistoryListItem> ApplicationHistories { get; } = BuildApplicationHistories();
    public static List<ProviderStatusHistoryListItem> ProviderStatusHistories { get; } = BuildProviderStatusHistories();
    public static List<OrderStatusHistoryListItem> OrderStatusHistories { get; } = BuildOrderStatusHistories();
    public static List<AuditLogListItem> AuditLogs { get; } = BuildAuditLogs();

    private static string Name(int i) => $"{FirstNames[i % FirstNames.Length]} {LastNames[i % LastNames.Length]}";

    private static List<OrderListItem> BuildOrders()
    {
        var list = new List<OrderListItem>();
        var statuses = new[]
        {
            OrderStatus.Pending, OrderStatus.Pending, OrderStatus.Pending,
            OrderStatus.OfferAccepted, OrderStatus.OfferAccepted,
            OrderStatus.InProgress, OrderStatus.InProgress,
            OrderStatus.Completed, OrderStatus.Completed, OrderStatus.Completed, OrderStatus.Completed,
            OrderStatus.Cancelled, OrderStatus.Cancelled
        };

        for (var i = 1; i <= 42; i++)
        {
            var status = statuses[i % statuses.Length];
            var catIdx = i % Categories.Length;
            var city = Cities[i % Cities.Length];
            var region = Regions[i % Regions.Length];
            list.Add(new OrderListItem
            {
                Id = i,
                Status = status,
                ProblemDescription = Problems[i % Problems.Length],
                Category = Categories[catIdx],
                Customer = Name(i),
                Address = $"{region}, {city}",
                CreatedAt = DateTime.Now.AddDays(-(i % 45)).AddHours(-(i % 12)),
                VisitingTime = DateTime.Now.AddDays((i % 5) + 1).AddHours(9 + (i % 8)),
                OffersCount = 1 + (i % 5),
                AcceptedOfferId = status is OrderStatus.Pending or OrderStatus.Cancelled ? null : 100 + i,
                AcceptedOfferTotal = status is OrderStatus.Pending or OrderStatus.Cancelled ? null : 300 + (i * 17 % 900)
            });
        }
        return list;
    }

    private static List<OfferListItem> BuildOffers()
    {
        var list = new List<OfferListItem>();
        var id = 1;
        foreach (var order in Orders)
        {
            for (var k = 0; k < order.OffersCount && k < 4; k++)
            {
                var labor = 150 + ((id * 37) % 600);
                var material = (id % 3 == 0) ? (decimal?)(50 + (id * 13 % 250)) : null;
                var status = order.AcceptedOfferId == 100 + order.Id && k == 0
                    ? OfferStatus.Accepted
                    : order.Status == OrderStatus.Cancelled ? OfferStatus.Rejected
                    : (id % 7 == 0 ? OfferStatus.Expired : OfferStatus.Pending);
                list.Add(new OfferListItem
                {
                    Id = id,
                    OrderId = order.Id,
                    Provider = Name(id + 3),
                    LaborCost = labor,
                    MaterialCost = material,
                    ArrivalTime = DateTime.Now.AddDays(1 + (id % 3)).AddHours(id % 6),
                    Status = status,
                    ExpiresAt = DateTime.Now.AddHours(30 + (id % 18)),
                    Notes = id % 2 == 0 ? "Price includes inspection and 30-day warranty." : null
                });
                id++;
            }
        }
        return list;
    }

    private static List<ProviderListItem> BuildProviders()
    {
        var statuses = new[] { ProviderStatus.Active, ProviderStatus.Active, ProviderStatus.Active, ProviderStatus.Suspended, ProviderStatus.Inactive };
        var list = new List<ProviderListItem>();
        for (var i = 1; i <= 16; i++)
        {
            list.Add(new ProviderListItem
            {
                Id = i,
                UserId = 100 + i,
                Name = Name(i + 5),
                NationalNumber = $"2980{i:D2}{i * 7 % 100:D2}0{1000 + i * 13 % 8999}",
                IsAvailable = i % 3 != 0,
                Status = statuses[i % statuses.Length],
                AverageRating = Math.Round(3.4 + (i * 0.13 % 1.6), 1),
                TotalReviewCount = 4 + (i * 6 % 60),
                CategoriesCount = 1 + (i % 3),
                CompletedOrders = 3 + (i * 9 % 40)
            });
        }
        return list;
    }

    private static List<CustomerListItem> BuildCustomers()
    {
        var list = new List<CustomerListItem>();
        for (var i = 1; i <= 24; i++)
        {
            list.Add(new CustomerListItem
            {
                Id = i,
                Name = Name(i),
                Email = $"customer{i}@example.com",
                Phone = $"+20 1{(i % 2 == 0 ? 0 : 1)}{(i * 1234567 % 89999999) + 10000000}",
                OrdersCount = 1 + (i * 3 % 9),
                AverageRating = i % 4 == 0 ? null : Math.Round(3.8 + (i * 0.11 % 1.1), 1),
                IsActive = i % 7 != 0,
                CreatedAt = DateTime.Now.AddDays(-(i * 5 % 180))
            });
        }
        return list;
    }

    private static List<CategoryListItem> BuildCategories()
    {
        var list = new List<CategoryListItem>();
        for (var i = 0; i < Categories.Length; i++)
        {
            list.Add(new CategoryListItem
            {
                Id = i + 1,
                Name = Categories[i],
                Description = $"Certified professionals for {Categories[i].ToLower()} services.",
                IconUrl = $"https://api.dicebear.com/7.x/icons/svg?seed={Categories[i]}",
                IsActive = i % 9 != 7,
                ParentCategoryId = null,
                ProvidersCount = 2 + (i * 3 % 12)
            });
        }
        list.Add(new CategoryListItem { Id = 100, Name = "Emergency Plumbing", Description = "24/7 urgent plumbing call-outs", IsActive = true, ParentCategoryId = 1, ProvidersCount = 4 });
        list.Add(new CategoryListItem { Id = 101, Name = "Lighting & Fixtures", Description = "Install and repair light fixtures", IsActive = true, ParentCategoryId = 2, ProvidersCount = 3 });
        return list;
    }

    private static List<CancelReasonListItem> BuildCancelReasons()
    {
        return new List<CancelReasonListItem>
        {
            new() { Id = 1, Text = "Found another provider", ApplicableTo = UserType.Customer, IsActive = true },
            new() { Id = 2, Text = "No longer needed", ApplicableTo = UserType.Customer, IsActive = true },
            new() { Id = 3, Text = "Price too high", ApplicableTo = UserType.Customer, IsActive = true },
            new() { Id = 4, Text = "Provider unavailable at requested time", ApplicableTo = UserType.Provider, IsActive = true },
            new() { Id = 5, Text = "Address not reachable", ApplicableTo = UserType.Provider, IsActive = true },
            new() { Id = 6, Text = "Job requires different tools", ApplicableTo = UserType.Provider, IsActive = true },
            new() { Id = 7, Text = "Suspicious activity reported", ApplicableTo = UserType.Admin, IsActive = true },
            new() { Id = 8, Text = "Policy violation", ApplicableTo = UserType.Admin, IsActive = true },
            new() { Id = 9, Text = "Duplicate order", ApplicableTo = UserType.Admin, IsActive = false }
        };
    }

    private static List<ProviderDocumentListItem> BuildDocuments()
    {
        var types = new[] { DocumentType.NationalId, DocumentType.CriminalRecord, DocumentType.WorkPermit, DocumentType.PersonalPhoto };
        var statuses = new[] { DocumentStatus.Approved, DocumentStatus.Pending, DocumentStatus.Pending, DocumentStatus.ReUploadRequired, DocumentStatus.Rejected, DocumentStatus.Expired };
        var list = new List<ProviderDocumentListItem>();
        var id = 1;
        for (var p = 1; p <= 16; p++)
        {
            foreach (var t in types)
            {
                list.Add(new ProviderDocumentListItem
                {
                    Id = id,
                    Provider = Providers[(p - 1) % Providers.Count].Name,
                    Type = t,
                    Status = statuses[id % statuses.Length],
                    DocumentUrl = $"https://placehold.co/600x400?text={t}+{p}",
                    ChangedBy = id % 3 == 0 ? "Admin" : null,
                    ChangedAt = id % 3 == 0 ? DateTime.Now.AddDays(-(id % 20)) : null
                });
                id++;
            }
        }
        return list;
    }

    private static List<WalletListItem> BuildWallets()
    {
        var list = new List<WalletListItem>();
        for (var i = 1; i <= 20; i++)
        {
            list.Add(new WalletListItem
            {
                Id = i,
                UserId = i,
                User = Name(i),
                Balance = Math.Round((decimal)((i * 137.45) % 4200), 2),
                IsActive = i % 6 != 0
            });
        }
        return list;
    }

    private static List<WalletTransactionListItem> BuildWalletTransactions()
    {
        var types = new[] { TransactionType.Deposit, TransactionType.Withdrawal, TransactionType.OrderPayment, TransactionType.OrderEarning, TransactionType.PlatformCommission, TransactionType.Refund, TransactionType.AdminAdjustment };
        var list = new List<WalletTransactionListItem>();
        for (var i = 1; i <= 40; i++)
        {
            var type = types[i % types.Length];
            list.Add(new WalletTransactionListItem
            {
                Id = i,
                WalletId = 1 + (i % 20),
                Amount = Math.Round((decimal)((i * 53.7) % 950), 2),
                Type = type,
                Description = type switch
                {
                    TransactionType.Deposit => "Wallet top-up via Fawry",
                    TransactionType.Withdrawal => "Withdrawal to Vodafone Cash",
                    TransactionType.OrderPayment => "Payment for order",
                    TransactionType.OrderEarning => "Earnings from completed order",
                    TransactionType.PlatformCommission => "Platform commission deducted",
                    TransactionType.Refund => "Refund issued after cancellation",
                    _ => "Manual adjustment by admin"
                },
                CreatedAt = DateTime.Now.AddDays(-(i % 30)).AddHours(-(i % 20)),
                OrderId = type is TransactionType.OrderPayment or TransactionType.OrderEarning or TransactionType.PlatformCommission or TransactionType.Refund ? 1 + (i % 42) : null
            });
        }
        return list;
    }

    private static List<PaymentListItem> BuildPayments()
    {
        var methods = new[] { PaymentMethod.Cash, PaymentMethod.CreditCard, PaymentMethod.Wallet, PaymentMethod.Fawry, PaymentMethod.VodafoneCash };
        var statuses = new[] { PaymentStatus.Success, PaymentStatus.Success, PaymentStatus.Success, PaymentStatus.Pending, PaymentStatus.Failed, PaymentStatus.Refunded };
        var list = new List<PaymentListItem>();
        for (var i = 1; i <= 36; i++)
        {
            var method = methods[i % methods.Length];
            list.Add(new PaymentListItem
            {
                Id = i,
                User = Name(i),
                Amount = Math.Round((decimal)((i * 91.3) % 1500 + 80), 2),
                Method = method,
                Status = statuses[i % statuses.Length],
                Type = i % 5 == 0 ? PaymentType.WalletTopUp : PaymentType.OrderPayment,
                TransactionReference = $"TXN-{DateTime.Now:yyyyMMdd}-{1000 + i}",
                CreatedAt = DateTime.Now.AddDays(-(i % 28)).AddHours(-(i % 18)),
                PaidAt = i % 3 == 0 ? DateTime.Now.AddDays(-(i % 28)).AddHours(-(i % 18) + 1) : null,
                OrderId = 1 + (i % 42)
            });
        }
        return list;
    }

    private static List<ReviewListItem> BuildReviews()
    {
        var comments = new[] { "On time and very professional.", "Fixed the issue quickly, fair price.", "Good work but arrived a bit late.", "Excellent service, will book again.", "Tidy and friendly.", null, "Had to call back, but resolved eventually." };
        var list = new List<ReviewListItem>();
        for (var i = 1; i <= 30; i++)
        {
            list.Add(new ReviewListItem
            {
                Id = i,
                Reviewee = Name(i + 2),
                Reviewer = Name(i + 8),
                Rating = 3 + (i % 3),
                Comment = comments[i % comments.Length],
                CreatedAt = DateTime.Now.AddDays(-(i * 2 % 60)),
                IsEdited = i % 8 == 0,
                OrderId = 1 + (i % 42)
            });
        }
        return list;
    }

    private static List<AddressListItem> BuildAddresses()
    {
        var list = new List<AddressListItem>();
        for (var i = 1; i <= 22; i++)
        {
            list.Add(new AddressListItem
            {
                Id = i,
                UserId = 1 + (i % 24),
                Title = i % 4 == 0 ? "Work" : "Home",
                City = Cities[i % Cities.Length],
                Region = Regions[i % Regions.Length],
                Street = $"{1 + (i % 40)} El-Merghany St.",
                BuildingNumber = (i % 60 + 1).ToString(),
                ApartmentNumber = (i % 12 + 1).ToString(),
                IsDefault = i % 5 == 0,
                Latitude = 30.0 + (i % 9) * 0.03,
                Longitude = 31.2 + (i % 9) * 0.03
            });
        }
        return list;
    }

    private static List<MessageListItem> BuildMessages()
    {
        var list = new List<MessageListItem>();
        var id = 1;
        for (var o = 1; o <= 8; o++)
        {
            var participants = new[] { Name(o), Name(o + 10) };
            var count = 4 + (o % 4);
            for (var m = 0; m < count; m++)
            {
                list.Add(new MessageListItem
                {
                    Id = id,
                    OrderId = o,
                    Sender = participants[m % 2],
                    Recipient = participants[(m + 1) % 2],
                    Content = m % 4 == 3 ? null : (m switch
                    {
                        0 => "Hi, I saw your order. I can arrive tomorrow morning.",
                        1 => "Great, what would the total cost be?",
                        2 => "Labor is 350 EGP, materials around 120 EGP.",
                        _ => "Sounds good, please come at 10 AM."
                    }),
                    Type = m % 4 == 3 ? MessageType.Location : MessageType.Text,
                    SentAt = DateTime.Now.AddMinutes(-(count - m) * 12),
                    ReadAt = m < count - 1 ? DateTime.Now.AddMinutes(-(count - m) * 12 + 3) : null,
                    IsSystemMessage = m == 0 && o % 3 == 0
                });
                id++;
            }
        }
        return list;
    }

    private static List<PhoneListItem> BuildPhones()
    {
        var list = new List<PhoneListItem>();
        for (var i = 1; i <= 18; i++)
        {
            list.Add(new PhoneListItem { Id = i, UserId = 1 + (i % 24), PhoneNumber = $"+20 1{(i % 2 == 0 ? 0 : 1)}{10000000 + i * 76543 % 89999999}", IsDefault = i % 4 == 0 });
        }
        return list;
    }

    private static List<ProviderCategoryListItem> BuildProviderCategories()
    {
        var list = new List<ProviderCategoryListItem>();
        var id = 1;
        foreach (var p in Providers)
        {
            for (var c = 0; c < p.CategoriesCount; c++)
            {
                list.Add(new ProviderCategoryListItem
                {
                    Id = id,
                    Provider = p.Name,
                    Category = Categories[(p.Id + c) % Categories.Length],
                    YearsOfExperience = 1 + ((p.Id * 3 + c) % 15)
                });
                id++;
            }
        }
        return list;
    }

    private static List<ApplicationHistoryListItem> BuildApplicationHistories()
    {
        var statuses = new[] { ApplicationStatus.Pending, ApplicationStatus.Approved, ApplicationStatus.ActionRequired, ApplicationStatus.Rejected };
        var list = new List<ApplicationHistoryListItem>();
        var id = 1;
        foreach (var p in Providers)
        {
            list.Add(new ApplicationHistoryListItem
            {
                Id = id,
                Provider = p.Name,
                Status = statuses[p.Id % statuses.Length],
                Notes = p.Status == ProviderStatus.Active ? "Documents verified" : "Awaiting updated documents",
                ChangedAt = DateTime.Now.AddDays(-(p.Id * 2 % 40)),
                ChangedBy = "Admin"
            });
            id++;
        }
        return list;
    }

    private static List<ProviderStatusHistoryListItem> BuildProviderStatusHistories()
    {
        var statuses = new[] { ProviderStatus.Active, ProviderStatus.Suspended, ProviderStatus.Inactive };
        var list = new List<ProviderStatusHistoryListItem>();
        var id = 1;
        foreach (var p in Providers)
        {
            list.Add(new ProviderStatusHistoryListItem
            {
                Id = id,
                Provider = p.Name,
                Status = statuses[p.Id % statuses.Length],
                Notes = p.Status == ProviderStatus.Suspended ? "Suspended after customer complaints" : "Status updated by admin",
                ChangedAt = DateTime.Now.AddDays(-(p.Id * 3 % 50)),
                ChangedBy = "Admin"
            });
            id++;
        }
        return list;
    }

    private static List<OrderStatusHistoryListItem> BuildOrderStatusHistories()
    {
        var list = new List<OrderStatusHistoryListItem>();
        var id = 1;
        foreach (var o in Orders)
        {
            list.Add(new OrderStatusHistoryListItem { Id = id++, OrderId = o.Id, Status = OrderStatus.Pending, Notes = "Order created", CreatedAt = o.CreatedAt, By = o.Customer });
            if (o.Status >= OrderStatus.OfferAccepted)
                list.Add(new OrderStatusHistoryListItem { Id = id++, OrderId = o.Id, Status = OrderStatus.OfferAccepted, Notes = "Offer accepted", CreatedAt = o.CreatedAt.AddHours(2), By = o.Customer });
            if (o.Status >= OrderStatus.InProgress)
                list.Add(new OrderStatusHistoryListItem { Id = id++, OrderId = o.Id, Status = OrderStatus.InProgress, Notes = "Provider started work", CreatedAt = o.CreatedAt.AddHours(20), By = "Provider" });
            if (o.Status == OrderStatus.Completed)
                list.Add(new OrderStatusHistoryListItem { Id = id++, OrderId = o.Id, Status = OrderStatus.Completed, Notes = "Work completed", CreatedAt = o.CreatedAt.AddHours(26), By = "Provider" });
            if (o.Status == OrderStatus.Cancelled)
                list.Add(new OrderStatusHistoryListItem { Id = id++, OrderId = o.Id, Status = OrderStatus.Cancelled, Notes = "Cancelled", CreatedAt = o.CreatedAt.AddHours(1), By = "Customer", Reason = "Found another provider" });
        }
        return list;
    }

    private static List<AuditLogListItem> BuildAuditLogs()
    {
        var actions = new[] { ActionType.Activate, ActionType.DeActivate, ActionType.Delete, ActionType.Restore, ActionType.Cancellation };
        var entities = new[] { EntityName.User, EntityName.Wallet, EntityName.Order };
        var list = new List<AuditLogListItem>();
        for (var i = 1; i <= 28; i++)
        {
            list.Add(new AuditLogListItem
            {
                Id = i,
                EntityName = entities[i % entities.Length],
                EntityId = 1 + (i * 3 % 40),
                Action = actions[i % actions.Length],
                Reason = i % 3 == 0 ? "Requested by support ticket #" + (4000 + i) : null,
                CreatedDate = DateTime.Now.AddDays(-(i % 35)).AddHours(-(i % 15)),
                PerformedBy = i % 4 == 0 ? "System" : "Admin",
                IsSelfAction = i % 5 == 0,
                IpAddress = $"197.4{i % 100}.{i * 7 % 255}.{(i * 13) % 255}"
            });
        }
        return list;
    }

    public static DashboardStats BuildStats()
    {
        var completed = Orders.Count(o => o.Status == OrderStatus.Completed);
        var paid = Payments.Where(p => p.Status == PaymentStatus.Success).Sum(p => p.Amount);
        var stats = new DashboardStats
        {
            TotalOrders = Orders.Count,
            OpenOrders = Orders.Count(o => o.Status == OrderStatus.Pending),
            InProgressOrders = Orders.Count(o => o.Status == OrderStatus.InProgress),
            CompletedOrders = completed,
            CancelledOrders = Orders.Count(o => o.Status == OrderStatus.Cancelled),
            TotalProviders = Providers.Count,
            OnlineProviders = Providers.Count(p => p.IsAvailable),
            PendingDocuments = Documents.Count(d => d.Status == DocumentStatus.Pending),
            TotalCustomers = Customers.Count,
            TotalRevenue = Math.Round(paid, 2),
            WalletLiability = Math.Round(Wallets.Sum(w => w.Balance), 2),
            AverageRating = Math.Round(Reviews.Average(r => r.Rating), 2)
        };

        stats.OrdersByStatus = Enum.GetValues<OrderStatus>()
            .Select(s => new OrderStatusBucket { Status = s, Count = Orders.Count(o => o.Status == s) })
            .ToList();

        stats.OrdersByCategory = Orders.GroupBy(o => o.Category)
            .Select(g => new CategoryBucket { Category = g.Key, Count = g.Count() })
            .OrderByDescending(c => c.Count)
            .ToList();

        stats.CancellationReasons = CancelReasons
            .Select(r => new CancelReasonBucket { Reason = r.Text, Count = 1 + (r.Id * 7 % 12) })
            .ToList();

        for (var i = 6; i >= 0; i--)
        {
            var day = DateTime.Now.AddDays(-i);
            var value = Payments.Where(p => p.Status == PaymentStatus.Success && p.CreatedAt.Date == day.Date).Sum(p => p.Amount);
            stats.RevenueTrend.Add(new RevenuePoint { Label = day.ToString("ddd"), Value = Math.Round(value, 2) });
        }

        return stats;
    }
}
