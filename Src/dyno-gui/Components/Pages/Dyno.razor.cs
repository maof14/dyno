using Client;
using Fluxor;
using Microsoft.AspNetCore.Components;
using SignalR;
using Store.Measurements;
using ViewModels;

namespace dyno_gui.Components.Pages;

public partial class Dyno
{
    [Inject]
    public IHubClient HubClient { get; set; }

    [Inject]
    public IClientApiService ApiService { get; set; }

    [Inject]
    public IState<MeasurementState> MeasurementState { get; set; }

    [Inject]
    public IDispatcher Dispatcher { get; set; }

    public List<MeasurementModel> Measurements => MeasurementState.Value.Measurements;

    protected override async Task OnInitializedAsync()
    {
        Dispatcher.Dispatch(new InitMeasurementViewAction());

        await base.OnInitializedAsync();
    }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
            await HubClient.ConnectAsync();

        await base.OnAfterRenderAsync(firstRender);
    }
}