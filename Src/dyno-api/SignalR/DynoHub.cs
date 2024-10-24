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
        // Todo: Use token on SignalR to get the user that requested it.. 
        await Clients.Others.SendAsync(SignalRMethods.MeasurementRequested, name, description, duration);
    }

    public async Task MeasurementCompleted(string measurementData)
    {
        await Clients.Others.SendAsync(SignalRMethods.MeasurementCompleted, measurementData);
    }
}
