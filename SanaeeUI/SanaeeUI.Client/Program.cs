using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using SanaeeUI.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

// Sanaee UI data services (shared with the server host)
builder.Services.AddSanaeeUiServices(builder.Configuration["SanaeeApi:BaseUrl"] ?? "http://localhost:5270/");

await builder.Build().RunAsync();
