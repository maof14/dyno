using Common;
using Microsoft.AspNetCore.SignalR;

namespace dyno_api.SignalR;

public class DynoHub : Hub
{
    public async Task Test()
    {
        Console.WriteLine("REE");
        await Task.CompletedTask;
    }

    public async Task MeasurementRequested(string name, string description, string duration)
    {
        await Clients.Others.SendAsync(SignalRMethods.MeasurementRequested, name, description, duration);
    }

    // from server
    public async Task MeasurementCompleted()
    {
        await Clients.Others.SendAsync(SignalRMethods.MeasurementCompleted);
    }
}
