using Common;
using dyno_wasm.Store.SharedActions;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SignalR;
using Store.Measurements;
using ViewModels;

namespace dyno_wasm.Pages;

public partial class Dyno : IAsyncDisposable
{
    [Inject]
    public IDispatcher Dispatcher { get; set; }

    [Inject]
    public IState<MeasurementState> MeasurementState { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IHubClient HubClient { get; set; }

    public List<MeasurementModel> Measurements => MeasurementState.Value.Measurements;

    private bool _measurementRunning = false;

    protected override void OnInitialized()
    {
        Dispatcher.Dispatch(new InitMeasurementViewAction());

        base.OnInitialized();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            await HubClient.ConnectAsync();
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private async Task TestConnection()
    {
        await HubClient.SendMessage(SignalRMethods.Test);
    }

    public async new Task DisposeAsync()
    {
        await HubClient.DisposeAsync();
    }

    private void HandleRowClick(TableRowClickEventArgs<MeasurementModel> tableRowClickEventArgs)
    {
        var a = tableRowClickEventArgs.Row.Item as MeasurementModel;
        NavigationManager.NavigateTo($"/measurement/{a.Id}");
    }

    private async Task InitializeNewMeasurement()
    {
        _measurementRunning = true;
        Dispatcher.Dispatch(new ToastSuccessAction() { SuccessMessage = "Starting measurement." });
        await HubClient.SendMessage(SignalRMethods.MeasurementRequested);
        _measurementRunning = false;
    }
}