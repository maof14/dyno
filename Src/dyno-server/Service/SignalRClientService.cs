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
    private readonly Random _random = new Random();

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
        _logger.LogInformation($"{nameof(SignalRClientService)}:{nameof(StartAsync)} - Starting background service {nameof(SignalRClientService)}");

        _hubClient.On(SignalRMethods.MeasurementRequested, async (string name, string description, string duration) =>
        {
            await HandleMeasurementRequestedAsync(name, description, duration);
        });

        await base.StartAsync(cancellationToken);
    }

    private async Task HandleMeasurementRequestedAsync(string name, string description, string duration)
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
        await StartConnectionAsync(stoppingToken);

        await ListenForMessagesAsync(stoppingToken);
    }

    private async Task ListenForMessagesAsync(CancellationToken stoppingToken)
    {
        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken); // Keep running
            }
        }
        catch (Exception ex)
        {
            _logger.LogError($"{nameof(SignalRClientService)}:{nameof(ListenForMessagesAsync)} - Error connecting to SignalR: {ex.Message}");
        }
    }

    private async Task StartConnectionAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            var retryCount = 0;
            DateTime startTime = DateTime.UtcNow;

            try
            {
                await _hubClient.ConnectAsync(stoppingToken);
                _logger.LogInformation($"{nameof(SignalRClientService)}:{nameof(StartConnectionAsync)} - SignalR connection started.");
                break;
            }
            catch (Exception ex)
            {
                retryCount++;
                _logger.LogError($"{nameof(SignalRClientService)}:{nameof(StartConnectionAsync)} - Failed to connect to SignalR: {ex.Message}. Attempt: {retryCount}/{_configuration.MaxHubConnectionRetries}.");

                if (retryCount >= _configuration.MaxHubConnectionRetries || DateTime.UtcNow - startTime >= TimeSpan.FromMinutes(_configuration.MaxHubConnectionRetryDurationInMinutes))
                {
                    _logger.LogError($"{nameof(SignalRClientService)}:{nameof(StartConnectionAsync)} - Max retry limit reached. Giving up on connecting to SignalR.");
                    break;
                }

                var delay = GetExponentialBackoffDelay(retryCount);
                _logger.LogInformation($"{nameof(SignalRClientService)}:{nameof(StartConnectionAsync)} - Retrying in {delay.TotalSeconds} seconds...");

                await Task.Delay(delay, stoppingToken);
            }
        }
    }

    private TimeSpan GetExponentialBackoffDelay(int retryCount)
    {
        var baseDelay = TimeSpan.FromSeconds(1);  // Starting delay: 1 second

        double jitterFactor = 0.8 + (_random.NextDouble() * 0.4);
        double delayMilliseconds = baseDelay.TotalMilliseconds * Math.Pow(2, retryCount) * jitterFactor;

        return TimeSpan.FromMilliseconds(Math.Min(delayMilliseconds, TimeSpan.FromMinutes(_configuration.MaxHubConnectionRetryDelayInSeconds).TotalMilliseconds));
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if(_hubClient != null)
        {
            await _hubClient.DisconnectAsync();
            _logger.LogInformation($"{nameof(SignalRClientService)}:{nameof(ExecuteAsync)} - SignalR connection stopped.");
            await base.StopAsync(cancellationToken);
        }
    }
}
