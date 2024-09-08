namespace Models;

public class MeasurementResult
{
    public MeasurementResult(Guid id, int datapoint, int count, DateTimeOffset dateTimeRecorded)
    {
        Id = id;
        Count = count;
        Datapoint = datapoint;
        DateTimeRecorded = dateTimeRecorded;
    }

    public Guid Id { get; set; }
    public int Datapoint { get ; set; }
    public DateTimeOffset DateTimeRecorded { get; set; }
    public int Count { get; set; }
}