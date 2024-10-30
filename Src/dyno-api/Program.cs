using Azure.Identity;
using Data;
using dyno_api;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Models;
using Repository;
using System.Text;
using Microsoft.OpenApi.Models;
using dyno_api.Helpers;
using Service;
using dyno_api.SignalR;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme()
    {
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Reference = new OpenApiReference()
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme,
        }
    };

    o.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
    o.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

builder.Services.AddSignalR();

builder.Services.AddScoped<IRepository<MeasurementEntity>, MeasurementRepository>();
builder.Services.AddTransient<IAuthHelpers, AuthHelpers>();
builder.Services.AddScoped<IRepository<AppUserEntity>, AppUserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.Configure<ApiConfiguration>(builder.Configuration.GetSection(nameof(ApiConfiguration)));
var settings = builder.Configuration.GetSection(nameof(ApiConfiguration)).Get<ApiConfiguration>();
builder.Services.AddSingleton(settings);

builder.Services.AddCors(builder =>
{
    builder.AddPolicy("GuiPolicy", BuildCorsPolicy(settings.GuiHost));
    builder.AddPolicy("ServerPolicy", BuildCorsPolicy(settings.ServerHost));
});

builder.Services.AddDbContext<DynoDbContext>(); // Har sin egen oncifugration till db. 

#if DEBUG
    Environment.SetEnvironmentVariable("AZURE_CLIENT_SECRET", builder.Configuration.GetValue<string>("ClientSecret"));
#endif

builder.Configuration.AddAzureKeyVault(new Uri(settings.VaultUrl), new DefaultAzureCredential());

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})


.AddJwtBearer(options =>
{
    var secret = builder.Configuration.GetValue<string>("JwtSecret");
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
        ValidateLifetime = true
    };

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Headers.Authorization;
            var path = context.HttpContext.Request.Path;

            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/dynohub"))
            {
                context.Token = accessToken;
            }

            return Task.CompletedTask;
        }
    };
});

// Add authorization services
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("GuiPolicy");
app.UseCors("ServerPolicy");

app.MapHub<DynoHub>("/dynohub");

app.UseHttpsRedirection();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetService<DynoDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.UseAuthentication();
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