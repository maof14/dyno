using Fluxor;
using Microsoft.AspNetCore.Components;
using Store.Measurements;
using ViewModels;

namespace dyno_wasm.Pages;

public partial class Dyno
{
    [Inject]
    public IDispatcher Dispatcher { get; set; }

    [Inject]
    public IState<MeasurementState> MeasurementState { get; set; }

    public List<MeasurementModel> Measurements => MeasurementState.Value.Measurements;

    protected override async Task OnInitializedAsync()
    {
        Dispatcher.Dispatch(new InitMeasurementViewAction());

        await base.OnInitializedAsync();
    }
}