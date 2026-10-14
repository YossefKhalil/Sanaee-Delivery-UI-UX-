using Microsoft.Extensions.DependencyInjection;

namespace SanaeeUI.Client.Services;

public static class ServiceCollectionExtensions
{
    // Registers the Sanaee UI services for both the server (prerender) and the
    // WebAssembly client. The same graph is used in both hosting models.
    public static IServiceCollection AddSanaeeUiServices(this IServiceCollection services, string apiBaseUrl = "http://localhost:5270/")
    {
        var baseUrl = apiBaseUrl.EndsWith('/') ? apiBaseUrl : apiBaseUrl + "/";

        services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(baseUrl) });
        services.AddSingleton<DemoDataService>();
        services.AddSingleton<SanaeeApiClient>();
        services.AddSingleton<AppState>();
        services.AddSingleton<ISanaeeDataService, SanaeeDataServiceRouter>();
        return services;
    }
}
