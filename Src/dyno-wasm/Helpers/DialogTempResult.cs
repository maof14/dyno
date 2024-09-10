namespace Helpers;

internal class DialogTempResult
{
    public string MeasurementName { get; }
    public string MeasurementDescription { get; }
    public string Duration { get; }

    public DialogTempResult(string measurementName, string measurementDescription, string duration)
    {
        this.MeasurementName = measurementName;
        this.MeasurementDescription = measurementDescription;
        this.Duration = duration;
    }
}