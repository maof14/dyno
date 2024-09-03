using Flurl;
using Microsoft.AspNetCore.SignalR.Client;

namespace dyno_gui.SignalR;

public interface IHubClient : IAsyncDisposable
{
    Task ConnectAsync();
    Task DisconnectAsync();

    Task SendMessage();

}


public class HubClient : IHubClient
{
    private HubConnection _hubConnection;

    public HubClient()
    {
        var url = "https://localhost:7239"; // Todo make configurable
        _hubConnection = new HubConnectionBuilder().WithUrl(url.AppendPathSegment("/dynohub")).Build();
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
    }

    public async Task SendMessage()
    {
        await _hubConnection.InvokeAsync(string.Empty);
    }
}
