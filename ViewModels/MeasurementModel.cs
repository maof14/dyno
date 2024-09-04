namespace ViewModels;

public class MeasurementModel
{
    public Guid Id { get; set; }

    public MeasurementModel(Guid id, List<MeasurementResultModel> measurementResults)
    {
        Id = id;
        MeasurementResults = measurementResults;
    }

    public List<MeasurementResultModel> MeasurementResults { get; set; } = new List<MeasurementResultModel>();
}
