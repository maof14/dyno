﻿using ViewModels;

namespace dyno_server.Service;

public interface IMonitorService
{
    void Initialize(); // Sätt upp koppling mot GPIO
    void Cleanup(); // Rensa upp resurser

    Task StartMonitoring(string duration); // Starta en monitorering
    MeasurementModel GetResult();
}
