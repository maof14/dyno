using Common;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SignalR;
using Store.App;
using Store.Measurements;
using Store.StartMeasurementDialogState;
using ViewModels;

namespace dyno_wasm.Pages;

public partial class Dyno : IAsyncDisposable
{
    [Inject]
    public IDispatcher Dispatcher { get; set; }

    [Inject]
    public IState<MeasurementState> MeasurementState { get; set; }

    [Inject]
    public IState<AppState> AppState { get; set; }

    [Inject]
    public NavigationManager NavigationManager { get; set; }

    [Inject]
    public IHubClient HubClient { get; set; }

    public List<MeasurementModel> Measurements => MeasurementState.Value.Measurements;

    protected override void OnInitialized()
    {
        if (AppState.Value.IsLoggedIn)
        {
            Dispatcher.Dispatch(new InitMeasurementViewAction());
        }

        base.OnInitialized();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            if (AppState.Value.IsLoggedIn)
            {
                await HubClient.ConnectAsync();
            }
        }

        await base.OnAfterRenderAsync(firstRender);
    }

    private void HandleDeleteClick(Guid id)
    {
        Dispatcher.Dispatch(new DeleteMeasurementAction { Id = id});
    }

    private async Task TestConnection()
    {
        await HubClient.SendMessage(SignalRMethods.Test);
    }

    private void HandleRefreshClick()
    {
        Dispatcher.Dispatch(new ReloadMeasurementViewAction());
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

    private void InitializeNewMeasurement()
    {
        Dispatcher.Dispatch(new InitStartMeasurementDialogAction());
    }
}