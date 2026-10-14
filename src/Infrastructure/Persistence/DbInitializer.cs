//using Core.Domain.Entities; // 👈 تعديل الـ Namespace حسب المجلد لديك
//using Core.Domain.Enums;
//using Infrastructure.Persistence;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;

//namespace Persistence.Seed
//{
//    public static class DbInitializer
//    {
//        public static async Task SeedAsync(AppDbContext context, ILogger? logger = null)
//        {
//            try
//            {
//                // 1. تطبيق الـ Migrations المعلقة إن وجدت
//                if (context.Database.GetPendingMigrations().Any())
//                {
//                    await context.Database.MigrateAsync();
//                }

//                // 2. استدعاء دوال الـ Seed بالترتيب الصحيح للعلاقات
//                var (admin, client, providerUser) = await SeedUsersAndWalletsAsync(context);
//                if (admin is null || client is null || providerUser is null) return;

//                await SeedUserPhonesAndAddressesAsync(context, client, providerUser);
//                var categories = await SeedCategoriesAsync(context);
//                var cancelReason = await SeedCancelationReasonsAsync(context);
//                var provider = await SeedProviderAsync(context, providerUser, admin, categories);

//                await SeedOrderSystemAsync(context, client, provider, categories.First(), cancelReason, admin);
//            }
//            catch (Exception ex)
//            {
//                logger?.LogError(ex, "An error occurred while seeding the Awraqy database.");
//                throw;
//            }
//        }

//        // --- 1. Users & Wallets ---
//        private static async Task<(User? admin, User? client, User? provider)> SeedUsersAndWalletsAsync(AppDbContext context)
//        {
//            if (await context.Users.AnyAsync())
//            {
//                var existingAdmin = await context.Users.FirstOrDefaultAsync(u => u.Email == "admin@awraqy.com");
//                var existingClient = await context.Users.FirstOrDefaultAsync(u => u.Email == "john.client@example.com");
//                var existingProvider = await context.Users.FirstOrDefaultAsync(u => u.Email == "alex.craftsman@example.com");
//                return (existingAdmin, existingClient, existingProvider);
//            }

//            var admin = new User
//            {
//                FirstName = "System",
//                LastName = "Admin",
//                Email = "admin@awraqy.com",
//                Password = "HashedPassword123!", // يفضل تحويلها لـ Hash إن وجد Hashing Service
//                Type = UserType.Admin, // UserType.Admin
//                CreatedAt = DateTime.UtcNow
//            };

//            var client = new User
//            {
//                FirstName = "John",
//                LastName = "Doe",
//                Email = "john.client@example.com",
//                Password = "HashedPassword123!",
//                Type = UserType.Customer, // UserType.Client
//                CreatedAt = DateTime.UtcNow,
//                AverageRating = 4.9f,
//                TotalReviewCount = 5
//            };

//            var providerUser = new User
//            {
//                FirstName = "Alex",
//                LastName = "Smith",
//                Email = "alex.craftsman@example.com",
//                Password = "HashedPassword123!",
//                Type = UserType.Provider, // UserType.Provider
//                CreatedAt = DateTime.UtcNow,
//                AverageRating = 4.8f,
//                TotalReviewCount = 12
//            };

//            await context.Users.AddRangeAsync(admin, client, providerUser);
//            await context.SaveChangesAsync();

//            // إنشاء Wallets للمستخدمين
//            var wallets = new List<Wallet>
//            {
//                new Wallet { Balance = 1000.00m, UserId = admin.UserId },
//                new Wallet { Balance = 250.00m, UserId = client.UserId },
//                new Wallet { Balance = 500.00m, UserId = providerUser.UserId }
//            };

//            await context.Wallets.AddRangeAsync(wallets);
//            await context.SaveChangesAsync();

//            return (admin, client, providerUser);
//        }

//        // --- 2. Phones & Addresses ---
//        private static async Task SeedUserPhonesAndAddressesAsync(AppDbContext context, User client, User providerUser)
//        {
//            if (!await context.UserPhones.AnyAsync())
//            {
//                var phones = new List<UserPhone>
//                {
//                    new UserPhone { PhoneNumber = "+201012345678", UserId = client.UserId },
//                    new UserPhone { PhoneNumber = "+201098765432", UserId = providerUser.UserId }
//                };
//                await context.UserPhones.AddRangeAsync(phones);
//            }

//            if (!await context.Addresses.AnyAsync())
//            {
//                var addresses = new List<Address>
//                {
//                    new Address
//                    {
//                        Title = "Home Address",
//                        City = "Cairo",
//                        Region = "Maadi",
//                        Street = "Road 9",
//                        BuildingNumber = "15",
//                        ApartmentNumber = "4B",
//                        Notes = "Near Metro Station",
//                        Latitude = 30.0444,
//                        Longitude = 31.2357,
//                        IsDefault = true,
//                        UserId = client.UserId
//                    }
//                };
//                await context.Addresses.AddRangeAsync(addresses);
//            }

//            await context.SaveChangesAsync();
//        }

//        // --- 3. Categories (Main & Sub) ---
//        private static async Task<List<Category>> SeedCategoriesAsync(AppDbContext context)
//        {
//            if (await context.Categories.AnyAsync())
//            {
//                return await context.Categories.ToListAsync();
//            }

//            var parentPlumbing = new Category
//            {
//                Name = "Plumbing Services",
//                Description = "All residential and commercial plumbing maintenance.",
//                IconUrl = "https://example.com/icons/plumbing.png",
//                IsActive = true
//            };

//            var parentElectrical = new Category
//            {
//                Name = "Electrical Works",
//                Description = "General electrical repair and installation services.",
//                IconUrl = "https://example.com/icons/electrical.png",
//                IsActive = true
//            };

//            await context.Categories.AddRangeAsync(parentPlumbing, parentElectrical);
//            await context.SaveChangesAsync();

//            // Sub Categories
//            var subCategories = new List<Category>
//            {
//                new Category
//                {
//                    Name = "Pipe Leak Repair",
//                    Description = "Fixing wall and floor water leaks.",
//                    ParentCategoryId = parentPlumbing.CategoryId,
//                    IsActive = true
//                },
//                new Category
//                {
//                    Name = "Lighting & Wiring Setup",
//                    Description = "Installing lamps, LED strips, and switches.",
//                    ParentCategoryId = parentElectrical.CategoryId,
//                    IsActive = true
//                }
//            };

//            await context.Categories.AddRangeAsync(subCategories);
//            await context.SaveChangesAsync();

//            return await context.Categories.ToListAsync();
//        }

//        // --- 4. Cancelation Reasons ---
//        private static async Task<CancelationReason> SeedCancelationReasonsAsync(AppDbContext context)
//        {
//            if (await context.CancelationReasons.AnyAsync())
//            {
//                return await context.CancelationReasons.FirstAsync();
//            }

//            var reason = new CancelationReason
//            {
//                Text = "Technician delayed for more than 45 minutes",
//                ApplicableTo = UserType.Provider, // UserType
//                IsActive = true
//            };

//            await context.CancelationReasons.AddAsync(reason);
//            await context.SaveChangesAsync();
//            return reason;
//        }

//        // --- 5. Provider & Provider Details ---
//        private static async Task<Provider> SeedProviderAsync(AppDbContext context, User providerUser, User admin, List<Category> categories)
//        {
//            var provider = await context.Providers.FirstOrDefaultAsync(p => p.UserId == providerUser.UserId);
//            if (provider == null)
//            {
//                provider = new Provider
//                {
//                    NationalNumber = "29801011234567",
//                    AverageRating = 4.8f,
//                    TotalReviewCount = 12,
//                    CreatedAt = DateTime.UtcNow,
//                    UserId = providerUser.UserId
//                };

//                await context.Providers.AddAsync(provider);
//                await context.SaveChangesAsync();
//            }

//            if (!await context.ProviderCategories.AnyAsync())
//            {
//                var providerCat = new ProviderCategory
//                {
//                    YearsOfExperience = 6,
//                    ProviderId = provider.ProviderId,
//                    CategoryId = categories.First().CategoryId
//                };
//                await context.ProviderCategories.AddAsync(providerCat);
//            }

//            if (!await context.ProviderDocuments.AnyAsync())
//            {
//                var doc = new ProviderDocument
//                {
//                    Type = DocumentType.CriminalRecord, // DocumentType
//                    Status = DocumentStatus.Approved, // DocumentStatus
//                    DocumentUrl = "https://example.com/docs/national_id.pdf",
//                    ProviderId = provider.ProviderId
//                };
//                await context.ProviderDocuments.AddAsync(doc);
//            }

//            if (!await context.ApplicationHistories.AnyAsync())
//            {
//                var appHistory = new ApplicationHistory
//                {
//                    Status = ApplicationStatus.Approved, // ApplicationStatus
//                    Notes = "Application reviewed and approved by admin.",
//                    ChangedAt = DateTime.UtcNow,
//                    ProviderId = provider.ProviderId,
//                    ChangedBy = admin.UserId
//                };
//                await context.ApplicationHistories.AddAsync(appHistory);
//            }

//            if (!await context.ProviderStatusHistories.AnyAsync())
//            {
//                var statusHistory = new ProviderStatusHistory
//                {
//                    Status = ProviderStatus.Approved, // ProviderStatus
//                    Notes = "Account set to active.",
//                    ChangedAt = DateTime.UtcNow,
//                    ProviderId = provider.ProviderId,
//                    ChangedBy = admin.UserId
//                };
//                await context.ProviderStatusHistories.AddAsync(statusHistory);
//            }

//            await context.SaveChangesAsync();
//            return provider;
//        }

//        // --- 6. Orders, Offers, Messages, Payments, Reviews & Wallet Transactions ---
//        private static async Task SeedOrderSystemAsync(
//            AppDbContext context,
//            User client,
//            Provider provider,
//            Category category,
//            CancelationReason cancelReason,
//            User admin)
//        {
//            if (await context.Orders.AnyAsync()) return;

//            var address = await context.Addresses.FirstOrDefaultAsync(a => a.UserId == client.UserId);
//            if (address == null) return;

//            // 1. Create Order
//            var order = new Order
//            {
//                VisitingTime = DateTime.UtcNow.AddDays(1),
//                ProblemPhoto = "https://example.com/photos/problem.jpg",
//                ProblemDescription = "Major water leakage under the main kitchen sink.",
//                Status = OrderStatus.InProgress, // OrderStatus
//                CreatedAt = DateTime.UtcNow,
//                IsClientReviewed = true,
//                IsProviderReviewed = true,
//                UserId = client.UserId,
//                CategoryId = category.CategoryId,
//                AddressId = address.AddressId
//            };

//            await context.Orders.AddAsync(order);
//            await context.SaveChangesAsync();

//            // 2. Create Offer from Provider
//            var offer = new Offer
//            {
//                ArrivalTime = DateTime.UtcNow.AddDays(1).AddHours(2),
//                LaborCost = 150.00m,
//                MaterialCost = 50.00m,
//                Notes = "Price includes pipe replacement and 1-month warranty.",
//                Status = OfferStatus.Accepted, // OfferStatus
//                ExpiresAt = DateTime.UtcNow.AddHours(12),
//                CreatedAt = DateTime.UtcNow,
//                ProviderId = provider.ProviderId,
//                OrderId = order.OrderId
//            };

//            await context.Offers.AddAsync(offer);
//            await context.SaveChangesAsync();

//            // Link Accepted Offer to Order
//            order.AcceptedOfferId = offer.OfferId;
//            context.Orders.Update(order);

//            // 3. Messages
//            var message = new Message
//            {
//                Content = "Hello, I will arrive tomorrow at 2:00 PM.",
//                Type = MessageType.Text, // MessageType
//                SentAt = DateTime.UtcNow,
//                IsRead = true,
//                SenderId = provider.UserId,
//                OrderId = order.OrderId
//            };
//            await context.Messages.AddAsync(message);

//            // 4. Order Status History
//            var orderHistory = new OrderStatusHistory
//            {
//                Status = OrderStatus.Completed, // OrderStatus
//                Notes = "Order completed successfully.",
//                CreatedAt = DateTime.UtcNow,
//                UserId = client.UserId,
//                OrderId = order.OrderId,
//                ReasonId = null
//            };
//            await context.OrderStatusHistories.AddAsync(orderHistory);

//            // 5. Payment
//            var payment = new Payment
//            {
//                Amount = 200.00m,
//                Method = PaymentMethod.CreditCard, // PaymentMethod
//                Status = PaymentStatus.Success, // PaymentStatus
//                Type = PaymentType.OrderPayment, // PaymentType
//                TransactionReference = "TXN-AWRAQY-998877",
//                PaidAt = DateTime.UtcNow,
//                UserId = client.UserId,
//                OrderId = order.OrderId
//            };
//            await context.Payments.AddAsync(payment);

//            // 6. Review
//            var review = new Review
//            {
//                StarsCount = 5,
//                Notes = "Excellent service, fixed the leak very quickly!",
//                CreatedAt = DateTime.UtcNow,
//                RevieweeId = provider.UserId,
//                ReviewerId = client.UserId,
//                OrderId = order.OrderId
//            };
//            await context.Reviews.AddAsync(review);

//            // 7. Wallet Transaction
//            var clientWallet = await context.Wallets.FirstOrDefaultAsync(w => w.UserId == client.UserId);
//            if (clientWallet != null)
//            {
//                var transaction = new WalletTransaction
//                {
//                    Amount = 200.00m,
//                    Type = TransactionType.OrderPayment, // TransactionType
//                    Description = $"Payment deduction for Order #{order.OrderId}",
//                    CreatedAt = DateTime.UtcNow,
//                    WalletId = clientWallet.WalletId,
//                    OrderId = order.OrderId
//                };
//                await context.WalletTransactions.AddAsync(transaction);
//            }

//            await context.SaveChangesAsync();
//        }
//    }
//}