namespace Models;

public class Measurement
{
    public Measurement(Guid id, DateTimeOffset dateTime, List<MeasurementResult> measurementResults)
    {
        Id = id;
        DateTime = dateTime;
        MeasurementResults = measurementResults;
    }

    public Guid Id { get; set; }
    public List<MeasurementResult> MeasurementResults { get; set; }
    public DateTimeOffset DateTime { get; set; }
}
