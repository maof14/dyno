namespace Infrastructure
{
    public class SignalRConstants
    {
        // Skickas från rpi till gui
        public const string MeasurementCompleted = nameof(MeasurementCompleted);
        // Skickas från gui till rpi
        public const string MeasurementInitialize = nameof(MeasurementInitialize);
    }
}
