using Common;
using dyno_server.Service;
using Microsoft.AspNetCore.SignalR;

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
        Console.WriteLine("REE");
        await Task.CompletedTask;
    }

    public async Task MeasurementRequested()
    {
        _monitorService.Initialize();
        await _monitorService.StartMonitoring();
        _monitorService.Cleanup();

        var result = _monitorService.GetResult();

        // Ta ut resultatet, skjut in i databas mha repo. 
        await _clientApiService.CreateMeasurement(result);
        await Clients.Caller.SendAsync(SignalRMethods.MeasurementCompleted);
    }
}
