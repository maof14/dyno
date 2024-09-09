using ViewModels;

namespace dyno_server.Simulation;

public class CarEngineSimulator
{
    private Random _random;
    private bool _isRunning;
    private List<MeasurementResultModel> _dataLog;

    public CarEngineSimulator()
    {
        _random = new Random();
        _dataLog = new List<MeasurementResultModel>();
    }

    // Method to calculate horsepower from torque and RPM
    private double CalculateHorsepower(double torque, double rpm)
    {
        return (torque * rpm) / 5252;  // Standard formula for Horsepower
    }

    // Simulates a single data point for torque and RPM
    private MeasurementResultModel SimulateEngineData()
    {
        double torque = _random.Next(100, 400);  // Simulated torque in Nm (randomized)
        double rpm = _random.Next(1000, 7000);   // Simulated RPM (randomized)
        double horsepower = CalculateHorsepower(torque, rpm);

        return new MeasurementResultModel(Guid.NewGuid(), torque, rpm, horsepower, DateTimeOffset.Now);
    }

    // Start the engine and collect data for a specified duration (in seconds)
    public async Task RunEngineAsync(int durationInSeconds)
    {
        Console.WriteLine("Engine starting...");

        _isRunning = true;
        var startTime = DateTime.Now;

        while (_isRunning && (DateTime.Now - startTime).TotalSeconds < durationInSeconds)
        {
            // Simulate engine data at regular intervals (e.g., every second)
            var engineData = SimulateEngineData();

            // Log the engine data
            _dataLog.Add(engineData);

            Console.WriteLine($"Torque: {engineData.Torque} Nm, RPM: {engineData.RPM}, Horsepower: {engineData.Horsepower}");

            // Wait for 1 second before the next reading
            await Task.Delay(1000);
        }

        _isRunning = false;
        Console.WriteLine("Engine stopped.");
    }

    // Stop the engine simulation
    public void StopEngine()
    {
        _isRunning = false;
    }

    // Get the collected engine data log
    public MeasurementModel GetEngineDataLog()
    {
        return new MeasurementModel(
            id: Guid.NewGuid(),
            measurementResults: _dataLog,
            DateTimeOffset.Now);
    }
}
