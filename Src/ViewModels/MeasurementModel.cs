namespace ViewModels;

public class MeasurementModel
{
    public Guid Id { get; set; }
    public DateTimeOffset DateTime { get; set; }

    public MeasurementModel(Guid id, List<MeasurementResultModel> measurementResults, DateTimeOffset dateTime)
    {
        Id = id;
        MeasurementResults = measurementResults;
        DateTime = dateTime;
    }

    public List<MeasurementResultModel> MeasurementResults { get; set; } = new List<MeasurementResultModel>();
}
