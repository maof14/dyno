using ViewModels;

namespace dyno_server.Service;

public interface IMonitorService
{
    void Initialize(); // Sätt upp koppling mot GPIO
    void Cleanup(); // Rensa upp resurser

    Task Test();

    Task StartMonitoring(); // Starta en monitorering
    MonitorResult GetResult();
}
