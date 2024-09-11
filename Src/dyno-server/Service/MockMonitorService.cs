using System.Device.Gpio;
using dyno_server.Simulation;
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

    public async Task StartMonitoring(string name, string description, string duration)
    {
        var d = int.TryParse(duration, out var seconds);
        await _engineSimulator.RunEngineAsync(name, description, seconds);
    }
}
