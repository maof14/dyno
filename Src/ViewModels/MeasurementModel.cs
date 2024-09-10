namespace ViewModels;

public class MeasurementModel
{
    public MeasurementModel() { /* FOR DESERALIZATION ONLY */}

    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset DateTime { get; set; }
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
