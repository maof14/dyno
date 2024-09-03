namespace Models
{
    public class Measurement
    {
        public Guid Id { get; set; }
        public List<MeasurementResult> MeasurementResults { get; set; }
    }
}
