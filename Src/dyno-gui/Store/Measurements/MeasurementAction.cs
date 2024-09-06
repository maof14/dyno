using ViewModels;

namespace Store.Measurements;

public class InitMeasurementViewAction
{
}

public class UpdateMeasurementsAction
{
    public List<MeasurementModel> Measurements { get; set; }
}