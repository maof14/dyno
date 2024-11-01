﻿using Common;
using Configuration;
using Flurl;
using Fluxor;
using Microsoft.AspNetCore.SignalR.Client;
using Service;
using Store.Measurements;
using Store.SharedActions;
using System.Text.Json;
using ViewModels;

namespace SignalR;

public class HubClient : IHubClient
{
    private HubConnection _hubConnection;
    private readonly IClientTokenService _tokenService;
    private readonly IClientApiService _clientApiService;
    private readonly AppConfiguration _configuration;

    public HubClient(IDispatcher dispatcher, AppConfiguration configuration, IClientTokenService tokenService, IClientApiService clientApiService)
    {
        // Todo use On methods instead of having dependency to dispatcher here.. .. 

        _tokenService = tokenService;
        _configuration = configuration;
        _clientApiService = clientApiService;
        Dispatcher = dispatcher;

        var url = _configuration.HubBaseAddress;
        _hubConnection = new HubConnectionBuilder()
            .WithUrl(url.AppendPathSegment("/dynohub"), options =>
            {
                options.AccessTokenProvider = () => Task.FromResult(_tokenService.Token);
            })
            .Build();

        _hubConnection.On(SignalRMethods.MeasurementCompleted, async (string measurementData) =>
        {
            var result = JsonSerializer.Deserialize<MeasurementModel>(measurementData);

            await _clientApiService.CreateMeasurement(result);

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

    public Task ConnectAsync(CancellationToken cancellationToken = default) => _hubConnection.StartAsync(cancellationToken);

    public Task DisconnectAsync(CancellationToken cancellationToken = default) => _hubConnection.StopAsync(cancellationToken);

    public IDisposable On(string methodName, Action handler) => _hubConnection.On(methodName, handler);

    public IDisposable On<T1>(string methodName, Action<T1> handler) => _hubConnection.On(methodName, handler);

    public IDisposable On<T1, T2>(string methodName, Action<T1, T2> handler) => _hubConnection.On(methodName, handler);

    public IDisposable On<T1, T2, T3>(string methodName, Action<T1, T2, T3> handler) => _hubConnection.On(methodName, handler);
}
