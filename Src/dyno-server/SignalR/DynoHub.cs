using Common;
using dyno_server.Service;
using Microsoft.AspNetCore.SignalR;
using System.Text.Json;

namespace dyno_server.SignalR;

public class DynoHub : Hub
{
    private readonly IMonitorService _monitorService;
    private readonly IClientApiService _clientApiService;

    public DynoHub(IMonitorService monitorService, IClientApiService clientApiService)
    {
        _monitorService = monitorService;
        _clientApiService = clientApiService;
    }

    public async Task Test()
    {
        await _monitorService.Test();
    }

    public async Task MeasurementRequested(string name, string description, string duration)
    {
        _monitorService.Initialize();

        await _monitorService.StartMonitoring(name, description, duration);
        
        var result = _monitorService.GetResult();

        _monitorService.Cleanup();

        // Ta ut resultatet, skjut in i databas mha repo. 
        await _clientApiService.CreateMeasurement(result);
        await Clients.Caller.SendAsync(SignalRMethods.MeasurementCompleted);
    }
}
