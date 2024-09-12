using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

[Table("MeasurementResult")]
public class MeasurementResult
{
    public MeasurementResult()
    {
        
    }

    public MeasurementResult(Guid id, double torque, double rPM, double horsepower, DateTimeOffset dateTimeOffset)
    {
        Id = id;
        Torque = torque;
        RPM = rPM;
        Horsepower = horsepower;
        DateTimeOffset = dateTimeOffset;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Measurement Measurement { get; set; }
    public Guid MeasurementId { get; set; }
    public double Torque { get; set; }  // Torque in Nm
    public double RPM { get; set; }     // Rotations Per Minute
    public double Horsepower { get; set; } // Calculated Horsepower
    public DateTimeOffset DateTimeOffset { get; set; }
}
