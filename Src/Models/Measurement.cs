namespace Models;

public class Measurement
{
    public Measurement(Guid id, string name, string description, DateTimeOffset dateTimeOffset, List<MeasurementResult> measurementResults)
    {
        Id = id;
        Name = name;
        Description = description;
        DateTimeOffset = dateTimeOffset;
        MeasurementResults = measurementResults;
    }

    public Guid Id { get; set; }
    public List<MeasurementResult> MeasurementResults { get; set; }
    public DateTimeOffset DateTimeOffset { get; set; }
    public string Name { get; set; } = $"{DateTimeOffset.Now.DayOfWeek.ToString()} {DateTimeOffset.Now.ToString()}";
    public string Description { get; set; } = string.Empty;
}
