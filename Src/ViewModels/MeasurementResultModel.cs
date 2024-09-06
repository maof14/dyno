namespace ViewModels;

public class MeasurementResultModel
{
    public MeasurementResultModel(Guid id, int datapoint, DateTimeOffset dateTimeRecorded)
    {
        Id = id;
        Datapoint = datapoint;
        DateTimeRecorded = dateTimeRecorded;
    }

    public Guid Id { get; set; }
    public int Datapoint { get; set; }
    public DateTimeOffset DateTimeRecorded { get; set; }
}