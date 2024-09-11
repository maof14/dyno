using dyno_server.Contract;
using System.Device.Gpio;

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

    public async Task Test()
    {
        Console.WriteLine("Starting to blink");

        var pin = 18;
        using var controller = new GpioController();
        controller.OpenPin(pin, PinMode.Output);
        bool ledOn = true;

        for(var i = 0; i < 10; i++)
        {
            controller.Write(pin, ((ledOn) ? PinValue.High : PinValue.Low));
            await Task.Delay(200);
            ledOn = !ledOn;
        }

        for (var i = 0; i < 4; i++)
        {
            controller.Write(pin, ((ledOn) ? PinValue.High : PinValue.Low));
            await Task.Delay(50);
            ledOn = !ledOn;
        }

        Console.WriteLine("Disposing of GpioController");
        controller.Dispose();
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
