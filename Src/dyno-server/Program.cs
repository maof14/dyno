using Common;
using dyno_server.Service;
using dyno_server.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddTransient<IMonitorService, MockMonitorService>();

builder.Services.AddTransient<IClientApiService, ClientApiService>();

builder.Services.AddHttpClient("APIClient", client =>
{
    client.BaseAddress = new Uri("https://localhost:7230/api/");
});

builder.Services.AddCors(builder =>
{
    builder.AddDefaultPolicy(o =>
    {
        o.WithOrigins("https://localhost:7093")
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapHub<DynoHub>("/dynohub");

app.UseCors();

app.Run();
