using dyno_server.Contract;

namespace dyno_server.Service;

public class MockMonitorService : IMonitorService
{
    private MonitorResult _result;

    public void Cleanup()
    {
        return;
    }

    public MonitorResult GetResult()
    {
        return _result;
    }

    public void Initialize()
    {
        return;
    }

    public async Task StartMonitoring()
    {
        _result = new MonitorResult(Guid.NewGuid());
        for (var i = 0; i < 50; i++)
        {
            var rnd = new Random();
            _result.AddDataPoint(new Result(
                dataPoint: rnd.Next(1, 50),
                dateTimeRecorded: DateTimeOffset.UtcNow));

            await Task.Delay(20);
        }
    }

    public void StopMonitoring()
    {
        return;
    }
}
