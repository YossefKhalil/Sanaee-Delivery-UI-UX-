using Microsoft.Extensions.DependencyInjection;

namespace SanaeeUI.Client.Services;

public static class ServiceCollectionExtensions
{
    // Registers the Sanaee UI services for both the server (prerender) and the
    // WebAssembly client. The same graph is used in both hosting models.
    // `apiBaseUrl` is the address the SanaeeApiClient's HttpClient should target:
    //   - Server: the API directly (server-to-server, no CORS).
    //   - WebAssembly: this app's own origin (handled by the reverse proxy).
    public static IServiceCollection AddSanaeeUiServices(this IServiceCollection services, string apiBaseUrl)
        => services.AddSanaeeUiServices(_ => new Uri(apiBaseUrl));

    public static IServiceCollection AddSanaeeUiServices(this IServiceCollection services, Func<IServiceProvider, Uri> baseAddressFactory)
    {
        services.AddSingleton(sp =>
        {
            var uri = baseAddressFactory(sp);
            var baseUrl = uri.AbsoluteUri;
            if (!baseUrl.EndsWith('/')) baseUrl += "/";
            return new HttpClient { BaseAddress = new Uri(baseUrl) };
        });
        services.AddSingleton<DemoDataService>();
        services.AddSingleton<SanaeeApiClient>();
        services.AddSingleton<AppState>();
        services.AddSingleton<ISanaeeDataService, SanaeeDataServiceRouter>();
        return services;
    }
}
