using dyno_server.Service;
using dyno_server.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();
builder.Services.AddTransient<IMonitorService, MockMonitorService>();

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.MapHub<DynoHub>("/dynohub");

app.Run();
