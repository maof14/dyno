using Common;
using Fluxor;
using Store.SharedActions;

namespace Store.Measurements;

public class MeasurementEffect
{
    private readonly IClientApiService _clientApiService;

    public MeasurementEffect(IClientApiService clientApiService)
    {
        _clientApiService = clientApiService;
    }

    [EffectMethod(typeof(InitMeasurementViewAction))]
    public async Task OnInitMeasurementViewAction(IDispatcher dispatcher)
    {
        await GetAllMeasurements(dispatcher);
    }

    [EffectMethod(typeof(ReloadMeasurementViewAction))]
    public async Task OnReloadMeasurementViewAction(IDispatcher dispatcher)
    {
        await GetAllMeasurements(dispatcher);
    }
    private async Task GetAllMeasurements(IDispatcher dispatcher)
    {
        try
        {
            dispatcher.Dispatch(new SetMeasurementsLoadingAction() { IsLoading = true });
            var result = await _clientApiService.GetMeasurementModels();
            dispatcher.Dispatch(new UpdateMeasurementsAction() { Measurements = result });
        }
        catch (Exception)
        {
            // Display toast or something
        }

        dispatcher.Dispatch(new SetMeasurementsLoadingAction() { IsLoading = false });
    }

    [EffectMethod]
    public async Task OnDeleteMeasurementAction(DeleteMeasurementAction action, IDispatcher dispatcher)
    {
        try
        {
            dispatcher.Dispatch(new SetMeasurementsLoadingAction { IsLoading = true });
            await _clientApiService.DeleteMeasurement(action.Id);
        }
        catch (Exception) {
            dispatcher.Dispatch(new SetMeasurementsLoadingAction { IsLoading = false });
            return;
        }

        dispatcher.Dispatch(new SetMeasurementsLoadingAction { IsLoading = false });
        dispatcher.Dispatch(new ToastSuccessAction { SuccessMessage = "Measurement deleted." });
        dispatcher.Dispatch(new ReloadMeasurementViewAction());
    }
}