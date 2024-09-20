using Common;
using Flurl;
using Fluxor;
using Microsoft.AspNetCore.SignalR.Client;
using Store.Measurements;
using Store.SharedActions;

namespace SignalR;

public class HubClient : IHubClient
{
    private HubConnection _hubConnection;

    public HubClient(IDispatcher dispatcher)
    {
        Dispatcher = dispatcher;

        var url = "https://localhost:7239"; // Todo make configurable
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(url.AppendPathSegment("/dynohub"))
            .Build();

        _hubConnection.On(SignalRMethods.MeasurementCompleted, () =>
        {
            Dispatcher.Dispatch(new ToastSuccessAction() { SuccessMessage = "Measurement completed." });
            Dispatcher.Dispatch(new ReloadMeasurementViewAction());
        });
    }

    public IDispatcher Dispatcher { get; }

    public Task ConnectAsync() => _hubConnection.StartAsync();

    public Task DisconnectAsync() => _hubConnection.StopAsync();

    public async ValueTask DisposeAsync()
    {
        await _hubConnection.StopAsync();
        await _hubConnection.DisposeAsync();

        GC.SuppressFinalize(this);
    }

    public Task SendMessage(string methodName) => _hubConnection.InvokeAsync(methodName);

    public HubConnectionState ConnectionState => _hubConnection.State;

    public Task SendMessage(string methodName, string arg0) => _hubConnection.InvokeAsync(methodName, arg0);

    public Task SendMessage(string methodName, string arg0, string arg1) => _hubConnection.InvokeAsync(methodName, arg0, arg1);

    public Task SendMessage(string methodName, string arg0, string arg1, string arg2) => _hubConnection.InvokeAsync(methodName, arg0, arg1, arg2);
}
