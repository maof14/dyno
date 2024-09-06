using Common;
using dyno_server.Service;
using Microsoft.AspNetCore.SignalR;

namespace dyno_server.SignalR;

public class DynoHub : Hub
{
    private IMonitorService _monitorService;

    public DynoHub(IMonitorService monitorService)
    {
        _monitorService = monitorService;
    }

    public async Task MeasurementRequested()
    {
        _monitorService.Initialize();
        await _monitorService.StartMonitoring();
        _monitorService.Cleanup();

        var result = _monitorService.GetResult();

        // Ta ut resultatet, skjut in i databas mha repo. 
        await Clients.Caller.SendAsync(SignalRMethods.MeasurementCompleted);
    }
}
