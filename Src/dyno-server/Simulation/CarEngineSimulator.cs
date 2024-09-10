using ViewModels;

namespace dyno_server.Simulation;

public class CarEngineSimulator
{
    private const double MaxRPM = 7000;          // Maximum RPM the engine can reach
    private const double IdleRPM = 800;          // RPM at idle
    private const double PeakTorqueRPM = 4500;   // RPM where torque peaks
    private const double MaxTorque = 400;        // Maximum Torque in Nm
    private const double MinTorque = 100;        // Minimum Torque at low/high RPMs
    private readonly Random _random = new Random();  // Random generator for variability

    private double _currentRPM;
    private double _currentTorque;
    private List<MeasurementResultModel> _dataLog;

    public CarEngineSimulator()
    {
        _currentRPM = IdleRPM;
        _currentTorque = MinTorque;
        _dataLog = new List<MeasurementResultModel>();
    }

    // Method to calculate horsepower from torque and RPM
    private double CalculateHorsepower(double torque, double rpm)
    {
        return (torque * rpm) / 5252;  // Standard formula for Horsepower
    }

    // Simulates RPM change over time (ramp-up and ramp-down) with a small random fluctuation
    private double SimulateRPM(int elapsedTime, int totalDuration)
    {
        double baseRPM;
        if (elapsedTime < totalDuration / 2)
        {
            baseRPM = IdleRPM + ((MaxRPM - IdleRPM) * (elapsedTime / (double)(totalDuration / 2)));
        }
        else
        {
            baseRPM = MaxRPM - ((MaxRPM - IdleRPM) * ((elapsedTime - totalDuration / 2) / (double)(totalDuration / 2)));
        }

        // Introduce a slight random variation to RPM (+/- 5%)
        double fluctuation = baseRPM * 0.05 * (_random.NextDouble() - 0.5);
        return baseRPM + fluctuation;
    }

    // Simulates Torque based on RPM (peak at a certain RPM, decreases after peak) with slight random variation
    private double SimulateTorque(double rpm)
    {
        double baseTorque;

        if (rpm <= PeakTorqueRPM)
        {
            baseTorque = MinTorque + ((MaxTorque - MinTorque) * (rpm / PeakTorqueRPM));
        }
        else
        {
            baseTorque = MaxTorque - ((MaxTorque - MinTorque) * ((rpm - PeakTorqueRPM) / (MaxRPM - PeakTorqueRPM)));
        }

        // Introduce a slight random variation to torque (+/- 10%)
        double fluctuation = baseTorque * 0.1 * (_random.NextDouble() - 0.5);
        return baseTorque + fluctuation;
    }

    // Simulates a single data point for torque and RPM based on elapsed time
    private MeasurementResultModel SimulateEngineData(int elapsedTime, int totalDuration)
    {
        _currentRPM = SimulateRPM(elapsedTime, totalDuration);
        _currentTorque = SimulateTorque(_currentRPM);
        double horsepower = CalculateHorsepower(_currentTorque, _currentRPM);

        return new MeasurementResultModel(Guid.NewGuid(), _currentTorque, _currentRPM, horsepower, DateTimeOffset.Now);
    }

    // Start the engine and collect data for a specified duration (in seconds)
    public async Task RunEngineAsync(int durationInSeconds)
    {
        Console.WriteLine("Engine starting...");

        for (int elapsedTime = 0; elapsedTime < durationInSeconds; elapsedTime++)
        {
            // Simulate engine data at each second
            var engineData = SimulateEngineData(elapsedTime, durationInSeconds);

            // Log the engine data
            _dataLog.Add(engineData);

            Console.WriteLine($"Time: {elapsedTime}s, Torque: {engineData.Torque:F2} Nm, RPM: {engineData.RPM:F0}, Horsepower: {engineData.Horsepower:F2}");

            // Wait for 1 second before the next reading
            await Task.Delay(1000);
        }

        Console.WriteLine("Engine stopped.");
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
