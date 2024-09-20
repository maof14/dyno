using Microsoft.AspNetCore.SignalR.Client;

namespace Common
{
    public interface IHubClient : IAsyncDisposable
    {
        HubConnectionState ConnectionState { get; }
        Task ConnectAsync();
        Task DisconnectAsync();
        Task SendMessage(string methodName);
        Task SendMessage(string methodName, string arg0);
        Task SendMessage(string methodName, string arg0, string arg1);
        Task SendMessage(string methodName, string arg0, string arg1, string arg2);
    }
}
