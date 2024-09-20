using Common;
using Microsoft.AspNetCore.SignalR.Client;

namespace dyno_server.Service;

public class SignalRClientService : BackgroundService
{
    private readonly ILogger<SignalRClientService> _logger;
    private readonly IMonitorService _monitorService;
    private readonly ITokenService _tokenService;
    private HubConnection _connection;

    public SignalRClientService(
        ILogger<SignalRClientService> logger,
        IMonitorService monitorService,
        ITokenService tokenService)
    {
        _logger = logger;
        _monitorService = monitorService;
        _tokenService = tokenService;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _connection = new HubConnectionBuilder()
            .WithUrl("https://your-signalr-server-url/hub")
            .Build();

        _connection.On<string>("ReceiveMessage", (message) =>
        {
            _logger.LogInformation($"Message received: {message}");
        });

        await base.StartAsync(cancellationToken);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await _connection.StartAsync(stoppingToken);
            _logger.LogInformation("SignalR connection started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken); // Keep running
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error connecting to SignalR: {ex.Message}");
        }
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        await _connection.StopAsync();
        _logger.LogInformation("SignalR connection stopped.");
        await base.StopAsync(cancellationToken);
    }
}
