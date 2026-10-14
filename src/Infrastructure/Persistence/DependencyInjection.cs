using Core.Application.Common.Contracts;
using Core.Application.Common.Contracts.Persistence;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Persistence.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Interceptors;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            // 1. تسجيل الـ Interceptor في الـ DI Container
            services.AddScoped<SoftDeleteInterceptor>(); // أو AuditableEntityInterceptor

            // 2. تسجيل الـ DbContext واستدعاء نص الاتصال والـ Interceptor
            services.AddDbContext<AppDbContext>((serviceProvider, options) =>
            {
                var interceptor = serviceProvider.GetRequiredService<SoftDeleteInterceptor>();

                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)
                )
                .AddInterceptors(interceptor);
            });


            services.AddScoped<IAppDbContext>(provider =>
                            provider.GetRequiredService<AppDbContext>());



            services.AddScoped<DispatchDomainEventsInterceptor>();

            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                var interceptor = sp.GetRequiredService<DispatchDomainEventsInterceptor>();
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName))
                       .AddInterceptors(interceptor);
            });



            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();


            services.AddScoped<IProviderPenaltyService, ProviderPenaltyService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IPaymentService, PaymentService>();

            return services;
        }
    }
}