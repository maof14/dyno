using Common;
using Flurl;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalR;

public interface IHubClient : IAsyncDisposable
{
    Task ConnectAsync();
    Task DisconnectAsync();
    Task SendMessage(string methodName);
}


public class HubClient : IHubClient
{
    private HubConnection _hubConnection;

    public HubClient()
    {
        // var url = "http://raspberrypi:5000"; // Use hostname of dyno-server when running it in production
        var url = "https://localhost:7239"; // Todo make configurable
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(url.AppendPathSegment("/dynohub"))
            .Build();

        _hubConnection.On(SignalRMethods.MeasurementCompleted, () =>
        {
            // En mätning är klar, visa en toast och återkoppla till klient. 
        });
    }

    public async Task ConnectAsync()
    {
        await _hubConnection.StartAsync();
    }

    public async Task DisconnectAsync()
    {
        await _hubConnection.StopAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _hubConnection.StopAsync();
        await _hubConnection.DisposeAsync();

        GC.SuppressFinalize(this);
    }

    public async Task SendMessage(string methodName)
    {
        await _hubConnection.InvokeAsync(methodName);
    }
}
