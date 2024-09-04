﻿using dyno_gui.Client;
using Fluxor;

namespace dyno_gui.Store.Measurements;

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
        var result = await _clientApiService.GetMeasurementModels();
        dispatcher.Dispatch(new UpdateMeasurementsAction() { Measurements = result });
    }
}