using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using DevShowcase.Client.Services;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using DevShowCase.Client;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// 1. HTTP Client - VIKTIGT: Dubbelkolla portnumret (7200) mot din launchSettings.json i API:et!
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7180") });

// 2. Auth Services
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthService>();
builder.Services.AddScoped<AuthService>(); // För direkt injicering

// 3. App Services
builder.Services.AddScoped<ApiService>();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
