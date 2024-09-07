using Common;
using dyno_server.Service;
using dyno_server.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Needed in production to not only listen on localhost.. (use real ip)
builder.WebHost.UseUrls("http://0.0.0.0:5000");

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
        o.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapHub<DynoHub>("/dynohub");

app.UseCors();

app.Run();
