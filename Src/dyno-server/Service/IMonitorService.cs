using dyno_server.Contract;

namespace dyno_server.Service;

public interface IMonitorService
{
    void Initialize(); // Sätt upp koppling mot GPIO
    void Cleanup(); // Rensa upp resurser

    Task Test();

    Task StartMonitoring(); // Starta en monitorering
    void StopMonitoring();
    MonitorResult GetResult();
}
