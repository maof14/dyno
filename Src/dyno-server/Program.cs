using Common;
using dyno_server.Configuration;
using dyno_server.Service;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddTransient<IMonitorService, MockMonitorService>();
builder.Services.AddTransient<IClientApiService, ClientApiService>();
builder.Services.AddTransient<JwtAuthorizationHandler>();
builder.Services.AddSingleton<ITokenService, TokenService>();

builder.Services.Configure<AppConfiguration>(builder.Configuration.GetSection(nameof(AppConfiguration)));
var settings = builder.Configuration.GetSection(nameof(AppConfiguration)).Get<AppConfiguration>();
builder.Services.AddSingleton(settings);

builder.Services.AddHttpClient("APIClient", client =>
{
    client.BaseAddress = new Uri(settings.ApiAddress);
}).AddHttpMessageHandler<JwtAuthorizationHandler>();

builder.Services.AddHttpClient("TokenClient", client =>
{
    client.BaseAddress = new Uri(settings.ApiAddress);
});

builder.Services.AddCors(builder =>
{
    builder.AddDefaultPolicy(o =>
    {
        o.WithOrigins(settings.ClientAddress)
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

builder.Services.AddHostedService<SignalRClientService>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.UseCors();

app.Run();
