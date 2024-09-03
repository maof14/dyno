using dyno_server.SignalR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

var app = builder.Build();


app.MapGet("/", () => "Hello World!");

app.MapHub<DynoHub>("/dynohub");

app.Run();
