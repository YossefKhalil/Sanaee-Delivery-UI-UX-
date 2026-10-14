using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SanaeeUI.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

// Sanaee UI data services (shared with the server host).
// On WebAssembly we call the API through this app's own origin so the request is
// handled by the server's reverse proxy — the browser never hits CORS.
builder.Services.AddSanaeeUiServices(sp => new Uri(sp.GetRequiredService<NavigationManager>().BaseUri));

await builder.Build().RunAsync();
