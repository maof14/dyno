using ViewModels;

namespace dyno_server.Service;

public class MockMonitorService : IMonitorService
{
    private MeasurementModel _result;

    public void Cleanup()
    {
        return;
    }

    public MeasurementModel GetResult()
    {
        return _result;
    }

    public void Initialize()
    {
        return;
    }

    public async Task StartMonitoring()
    {
        var rnd = new Random();
        var tasks = Enumerable.Range(0, 50).Select(async x => {
            await Task.Delay(100);
            return new MeasurementResultModel(Guid.NewGuid(), rnd.Next(0, 100), DateTimeOffset.Now, x);
        }).ToList();

        var result = await Task.WhenAll(tasks);

        _result = new MeasurementModel(id: Guid.NewGuid(), measurementResults: result.ToList(), dateTime: DateTimeOffset.Now);
    }

    public void StopMonitoring()
    {
        throw new NotImplementedException("Not implemented yet, only if neoded.");
    }
}
