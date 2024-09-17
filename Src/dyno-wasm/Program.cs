using Common;
using Configuration;
using dyno_wasm;
using Fluxor;
using Fluxor.Blazor.Web.ReduxDevTools;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Service;
using SignalR;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddFluxor(options =>
{
    options.ScanAssemblies(typeof(Program).Assembly);
    options.UseRouting();
    options.UseReduxDevTools();
});

builder.Services.AddMudServices();

var settings = builder.Configuration.GetSection(nameof(AppConfiguration)).Get<AppConfiguration>();

builder.Services.AddTransient<IClientApiService, ClientApiService>();
builder.Services.AddTransient<IHubClient, HubClient>();
builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddTransient<JwtAuthorizationHandler>();

builder.Services.AddHttpClient("APIClient", client =>
{
    client.BaseAddress = new Uri(settings.ApiBaseAddress);
}).AddHttpMessageHandler<JwtAuthorizationHandler>();

builder.Services.AddHttpClient("TokenClient", client =>
{
    client.BaseAddress = new Uri(settings.ApiBaseAddress);
});

await builder.Build().RunAsync();
