﻿using dyno_server.Simulation;
using ViewModels;

namespace dyno_server.Service;

public class MockMonitorService : IMonitorService
{
    private CarEngineSimulator _engineSimulator;

    public void Cleanup()
    {
        _engineSimulator = null;
    }

    public MeasurementModel GetResult()
    {
        return _engineSimulator.GetEngineDataLog();
    }

    public void Initialize()
    {
        _engineSimulator = new CarEngineSimulator();
    }

    public async Task StartMonitoring(string duration)
    {
        var d = int.TryParse(duration, out var seconds);
        await _engineSimulator.RunEngineAsync(seconds);
    }
}
