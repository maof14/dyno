using Common;
using dyno_server.Configuration;
using Microsoft.AspNetCore.SignalR.Client;

namespace dyno_server.Service;

public class SignalRClientService : BackgroundService
{
    private readonly ILogger<SignalRClientService> _logger;
    private readonly IMonitorService _monitorService;
    private readonly ITokenService _tokenService;
    private readonly IHubClient _hubClient;
    private readonly IClientApiService _clientApiService;
    private readonly AppConfiguration _configuration;

    public SignalRClientService(
        ILogger<SignalRClientService> logger,
        IMonitorService monitorService,
        ITokenService tokenService,
        IHubClient hubClient,
        IClientApiService clientApiService,
        AppConfiguration appConfiguration)
    {
        _logger = logger;
        _monitorService = monitorService;
        _tokenService = tokenService;
        _hubClient = hubClient;
        _clientApiService = clientApiService;
        _configuration = appConfiguration;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Starting background service {nameof(SignalRClientService)}");

        _hubClient.On(SignalRMethods.MeasurementRequested, async (string name, string description, string duration) =>
        {
            await HandleMeasurementRequested(name, description, duration);
        });

        await base.StartAsync(cancellationToken);
    }

    private async Task HandleMeasurementRequested(string name, string description, string duration)
    {
        _monitorService.Initialize();
        await _monitorService.StartMonitoring(name, description, duration);
        var result = _monitorService.GetResult();
        await _tokenService.GetTokenAsync(_configuration.ServerUser, _configuration.ServerPassword);
        await _clientApiService.CreateMeasurement(result);
        await _hubClient.SendMessage(SignalRMethods.MeasurementCompleted);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        try
        {
            await _hubClient.ConnectAsync(stoppingToken);
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
        await _hubClient.DisconnectAsync();
        _logger.LogInformation("SignalR connection stopped.");
        await base.StopAsync(cancellationToken);
    }
}
