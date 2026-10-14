using MudBlazor.Services;
using SanaeeUI.Client.Pages;
using SanaeeUI.Client.Services;
using SanaeeUI.Components;
using SanaeeUI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddMudServices();

// Base URL of the real Sanaee-Delivery API.
var apiBaseUrl = builder.Configuration["SanaeeApi:BaseUrl"] ?? "http://localhost:5270/";
if (!apiBaseUrl.EndsWith('/')) apiBaseUrl += "/";

// Named client used by the reverse proxy to reach the API server-to-server (no CORS).
builder.Services.AddHttpClient(SanaeeApiProxyClient.Name, c => c.BaseAddress = new Uri(apiBaseUrl));

// Sanaee UI data services (shared with the WebAssembly client).
// On the server (prerender) we call the API directly — no proxy needed.
builder.Services.AddSanaeeUiServices(apiBaseUrl);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();

// Reverse proxy: forward this app's /api/** to the real API (CORS-free bridge
// for the WebAssembly client). The original Sanaee code is not modified.
app.MapSanaeeApiProxy();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(SanaeeUI.Client._Imports).Assembly);

app.Run();
