using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

[Table("Measurement")]
public class Measurement
{
    public Measurement()
    {
        
    }
    public Measurement(Guid id, string name, string description, DateTimeOffset dateTimeOffset, List<MeasurementResult> measurementResults)
    {
        Id = id;
        Name = name;
        Description = description;
        DateTimeOffset = dateTimeOffset;
        MeasurementResults = measurementResults;
    }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public List<MeasurementResult> MeasurementResults { get; set; }
    public DateTimeOffset DateTimeOffset { get; set; }
    public string Name { get; set; } = $"{DateTimeOffset.Now.DayOfWeek.ToString()} {DateTimeOffset.Now.ToString()}";
    public string Description { get; set; } = string.Empty;
}
