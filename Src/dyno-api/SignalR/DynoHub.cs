using Microsoft.AspNetCore.SignalR;

namespace dyno_api.SignalR;

public class DynoHub : Hub
{
    public async Task Test()
    {
        Console.WriteLine("REE");
        await Task.CompletedTask;
    }

    public Task MeasurementRequested(string name, string description, string duration)
    {
        throw new NotImplementedException();
    }
}
