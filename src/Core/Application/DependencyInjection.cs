using Core.Application.Common.Behaviors;
using Core.Application.Features.Addresses.Profiles;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddValidatorsFromAssembly(assembly);

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            // يقدم مسح شامل لكافة الـ Profiles الموجودة في الـ Application Layer
            services.AddAutoMapper(cfg => cfg.AddMaps(typeof(AddressMappingProfile).Assembly));

            return services;
        }
    }
}
