using Common;
using Fluxor;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using SignalR;
using Store.App;
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
    public IState<AppState> AppState { get; set; }

    [Inject]
    public IHubClient HubClient { get; set; }

    [Parameter]
    public string MeasurementId { get; set; }

    public MeasurementModel Measurement => MeasurementState.Value.Measurements.FirstOrDefault(x => x.Id == Guid.Parse(MeasurementId));

    private int Index = -1; //default value cannot be 0 -> first selectedindex is 0.
    public ChartOptions Options = new ChartOptions();

    public List<ChartSeries> Series => new List<ChartSeries>()
    {
        new ChartSeries { Name = "Torque", Data = Measurement.MeasurementResults.Select(x => x.Torque).ToArray() },
        new ChartSeries { Name = "Horsepower", Data = Measurement.MeasurementResults.Select(x => x.Horsepower).ToArray() }
    };

    public string[] XAxisLabels => Measurement.MeasurementResults.Select((x, i) => i.ToString()).ToArray();

    protected override void OnInitialized() {
        // todo: If Measurement is null, initalize it specifically with some store action from this page.

        base.OnInitialized();
    }

}