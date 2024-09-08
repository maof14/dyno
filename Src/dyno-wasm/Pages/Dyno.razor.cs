using Common;
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

    protected override async Task OnInitializedAsync()
    {
        Dispatcher.Dispatch(new InitMeasurementViewAction());

        await base.OnInitializedAsync();
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

    private void InitializeNewMeasurement()
    {
        // Skicka meddelande till signalR att skapa ny measurement. 
        // Tar emot meddelande, ska då dispatcha att denna är klar
        // Uppdatera dispatcha då att är färdig, denna komponent får det nya resultatet. Aki hej da. 
    }
}