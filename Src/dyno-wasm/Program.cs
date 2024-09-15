using Common;
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

builder.Services.AddTransient<IClientApiService, ClientApiService>();
builder.Services.AddTransient<IHubClient, HubClient>();
builder.Services.AddSingleton<ITokenService, TokenService>();

builder.Services.AddHttpClient("APIClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7230/api/");
}); // .AddHttpMessageHandler();... 

builder.Services.AddHttpClient("TokenClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7230/api/");
});

await builder.Build().RunAsync();
