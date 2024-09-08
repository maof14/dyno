using Fluxor;
using Microsoft.AspNetCore.Components;
using SignalR;
using Store.Measurements;
using ViewModels;

namespace dyno_wasm.Pages;

public partial class DynoDetails
{

    [Inject]
    public IDispatcher Dispatcher { get; set; }

    [Inject]
    public IState<MeasurementState> MeasurementState { get; set; }

    [Inject]
    public IHubClient HubClient { get; set; }

    [Parameter]
    public string MeasurementId { get; set; }

    public MeasurementModel Measurement => MeasurementState.Value.Measurements.FirstOrDefault(x => x.Id == Guid.Parse(MeasurementId));

    public override void OnInitialized() {
        // todo: If Measurement is null, initalize with some store action

        base.OnInitialized();
    }

}