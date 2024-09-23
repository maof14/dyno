using Common;
using dyno_server.Configuration;
using dyno_server.Service;
using Flurl;
using Microsoft.AspNetCore.SignalR.Client;

namespace dyno_server.SignalR;

public class HubClient : IHubClient
{
    private readonly HubConnection _hubConnection;

    public HubClient(
        IMonitorService monitorService,
        AppConfiguration configuration)
    {
        var retryPolicy = new ExponentialBackoffRetryPolicy(
            initialDelay: TimeSpan.FromSeconds(1),
            maxDelay: TimeSpan.FromSeconds(30),
            maxAttempts: 100);

        var url = configuration.HubBaseAddress;
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(url.AppendPathSegment("/dynohub"))
            .WithAutomaticReconnect(retryPolicy)
            .Build();
    }

    public HubConnectionState ConnectionState => throw new NotImplementedException();

    public async Task ConnectAsync(CancellationToken cancellationToken = default)
    {
        await _hubConnection.StartAsync(cancellationToken);
    }

    public async Task DisconnectAsync(CancellationToken cancellationToken = default)
    {
        await _hubConnection.StopAsync(cancellationToken);
    }

    public async ValueTask DisposeAsync()
    {
        await _hubConnection.StopAsync();
        await _hubConnection.DisposeAsync();
    }

    public IDisposable On(string methodName, Action handler) => _hubConnection.On(methodName, handler);
    public IDisposable On<T1>(string methodName, Action<T1> handler) => _hubConnection.On(methodName, handler);
    public IDisposable On<T1, T2>(string methodName, Action<T1, T2> handler) => _hubConnection.On(methodName, handler);
    public IDisposable On<T1, T2, T3>(string methodName, Action<T1, T2, T3> handler) => _hubConnection.On(methodName, handler);
    public Task SendMessage(string methodName) => _hubConnection.InvokeAsync(methodName);
    public Task SendMessage(string methodName, string arg0) => _hubConnection.InvokeAsync(methodName, arg0);
    public Task SendMessage(string methodName, string arg0, string arg1) => _hubConnection.InvokeAsync(methodName, arg0, arg1);
    public Task SendMessage(string methodName, string arg0, string arg1, string arg2) => _hubConnection.InvokeAsync(methodName, arg0, arg1, arg2);
}
