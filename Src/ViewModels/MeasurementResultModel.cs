namespace ViewModels;

public class MeasurementResultModel
{
    public MeasurementResultModel(Guid id, int datapoint, DateTimeOffset dateTimeRecorded, int count)
    {
        Id = id;
        Datapoint = datapoint;
        DateTimeRecorded = dateTimeRecorded;
        Count = count;
    }

    public Guid Id { get; set; }
    public int Datapoint { get; set; }
    public DateTimeOffset DateTimeRecorded { get; set; }
    public int Count { get; set; }
}