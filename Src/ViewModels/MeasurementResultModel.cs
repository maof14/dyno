namespace ViewModels;

public class MeasurementResultModel
{
    public MeasurementResultModel() {/* FOR DESERALIZATION ONLY */}

    public MeasurementResultModel(Guid id, double torque, double rPM, double horsepower, DateTimeOffset dateTimeOffset)
    {
        Id = id;
        Torque = torque;
        RPM = rPM;
        Horsepower = horsepower;
        DateTimeOffset = dateTimeOffset;
    }

    public Guid Id { get; }
    public double Torque { get; }  // Torque in Nm
    public double RPM { get; }     // Rotations Per Minute
    public double Horsepower { get; } // Calculated Horsepower
    public DateTimeOffset DateTimeOffset { get; }
}