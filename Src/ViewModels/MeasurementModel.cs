namespace ViewModels;

public class MeasurementModel
{
    public MeasurementModel() { /* FOR DESERALIZATION ONLY */}

    public Guid Id { get; }
    public string Name { get; } = string.Empty;
    public string Description { get; } = string.Empty;
    public DateTimeOffset DateTime { get; }
    public List<MeasurementResultModel> MeasurementResults { get; set; } = new List<MeasurementResultModel>();
    public MeasurementModel(Guid id, string name, string description, List<MeasurementResultModel> measurementResults, DateTimeOffset dateTime)
    {
        Id = id;
        Name = name;
        Description = description;
        MeasurementResults = measurementResults;
        DateTime = dateTime;
    }
    public MeasurementModel(Guid id, List<MeasurementResultModel> measurementResults, DateTimeOffset dateTime)
    {
        Id = id;
        MeasurementResults = measurementResults;
        DateTime = dateTime;
    }
}
