using Common;
using dyno_server.Configuration;
using dyno_server.Service;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace dyno_server.SignalR;

public class DynoHub : Hub
{
    private readonly IMonitorService _monitorService;
    private readonly IClientApiService _clientApiService;
    private readonly ITokenService _tokenService;
    private readonly AppConfiguration _configuration;

    public DynoHub(IMonitorService monitorService, IClientApiService clientApiService, ITokenService tokenService, AppConfiguration configuration)
    {
        _monitorService = monitorService;
        _clientApiService = clientApiService;
        _tokenService = tokenService;
        _configuration = configuration;
    }

    public async Task Test()
    {
        Console.WriteLine("REE");
        await Task.CompletedTask;
    }

    public async Task MeasurementRequested(string name, string description, string duration)
    {
        _monitorService.Initialize();

        await _monitorService.StartMonitoring(name, description, duration);
        
        var result = _monitorService.GetResult();

        _monitorService.Cleanup();

        await EnsureHasApiToken();
        await _clientApiService.CreateMeasurement(result);
        await Clients.Caller.SendAsync(SignalRMethods.MeasurementCompleted);
    }

    private async Task EnsureHasApiToken()
    {
        if(_tokenService.Token == null)
        {
            await _tokenService.GetTokenAsync(_configuration.ServerUser, _configuration.ServerPassword);
        }
    }
}
