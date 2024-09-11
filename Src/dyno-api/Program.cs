using Data;
using dyno_api;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Models;
using Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepository<Measurement>, MockMeasurementRepository>();

builder.Services.Configure<ApiConfiguration>(builder.Configuration.GetSection(nameof(ApiConfiguration)));
var settings = builder.Configuration.GetSection(nameof(ApiConfiguration));

var guiHost = settings.GetValue<string>("GuiHost");
var serverHost = settings.GetValue<string>("ServerHost");

builder.Services.AddCors(builder =>
{
    builder.AddPolicy("GuiPolicy", BuildCorsPolicy(guiHost));
    builder.AddPolicy("ServerPolicy", BuildCorsPolicy(serverHost));
});

builder.Services.AddDbContext<DynoDbContext>(); // Har sin egen oncifugration till db. 

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("GuiPolicy");
app.UseCors("ServerPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static Action<CorsPolicyBuilder> BuildCorsPolicy(string host)
{
    return builder =>
    {
        builder.WithOrigins(host)
        .AllowAnyMethod()
        .AllowAnyHeader();
    };
}