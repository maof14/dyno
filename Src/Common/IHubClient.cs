using Microsoft.AspNetCore.SignalR.Client;

namespace Common;

public interface IHubClient : IAsyncDisposable
{
    HubConnectionState ConnectionState { get; }
    Task ConnectAsync(CancellationToken cancellationToken = default);
    Task DisconnectAsync(CancellationToken cancellationToken = default);
    IDisposable On(string methodName, Action handler);
    IDisposable On<T1>(string methodName, Action<T1> handler);
    IDisposable On<T1, T2>(string methodName, Action<T1, T2> handler);
    IDisposable On<T1, T2, T3>(string methodName, Action<T1, T2, T3> handler);
    Task SendMessage(string methodName);
    Task SendMessage(string methodName, string arg0);
    Task SendMessage(string methodName, string arg0, string arg1);
    Task SendMessage(string methodName, string arg0, string arg1, string arg2);
}
