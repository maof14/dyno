namespace Models;

public class MeasurementResult
{
    public Guid Id { get; set; }
    public int DataPoint { get ; set; }
    public DateTimeOffset DateTimeRecorded { get; set; }
    public int Count { get; set; }
}