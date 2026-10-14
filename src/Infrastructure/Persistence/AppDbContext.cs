using Core.Application.Common.Contracts;
using Core.Domain.Entities;
using Core.Domain.Entities.Contract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }


        #region DbSets
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Provider> Providers { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<UserPhone> UserPhones { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<Offer> Offers { get; set; } = null!;
        public DbSet<Message> Messages { get; set; } = null!;
        public DbSet<Review> Reviews { get; set; } = null!;
        public DbSet<Payment> Payments { get; set; } = null!;
        public DbSet<Wallet> Wallets { get; set; } = null!;
        public DbSet<WalletTransaction> WalletTransactions { get; set; } = null!;
        public DbSet<CancelationReason> CancelationReasons { get; set; } = null!;
        public DbSet<OrderStatusHistory> OrderStatusHistories { get; set; } = null!;
        public DbSet<ProviderCategory> ProviderCategories { get; set; } = null!;
        public DbSet<ProviderDocument> ProviderDocuments { get; set; } = null!;
        public DbSet<ProviderStatusHistory> ProviderStatusHistories { get; set; } = null!;
        public DbSet<ApplicationHistory> ApplicationHistories { get; set; } = null!;
        public DbSet<AuditLog> AduitLogs { get; set; } = null!;
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region 1. Soft Delete Global Query Filter
            foreach(var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if(typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType))
                {
                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(GetIsDeletedFilter(entityType.ClrType));
                }
            }
            #endregion


            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    var propertyType = Nullable.GetUnderlyingType(property.ClrType) ?? property.ClrType;

                    if (propertyType.IsEnum)
                    {
                        modelBuilder.Entity(entityType.ClrType)
                            .Property(property.Name)
                            .HasConversion<string>()
                            .HasMaxLength(50); // insted of nvarchar(max)
                    }
                }
            }

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }


        #region Helper Methods
        private LambdaExpression? GetIsDeletedFilter(Type type)
        {
            var parameter = Expression.Parameter(type, "e");
            var property = Expression.Property(parameter, nameof(ISoftDeletable.IsDeleted));
            var falseConstant = Expression.Constant(false);
            var comparison = Expression.Equal(property, falseConstant);

            return Expression.Lambda(comparison, parameter);
        }
        #endregion


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
