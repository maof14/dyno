using ViewModels;

namespace Store.Measurements;

public class InitMeasurementViewAction
{
}

public class UpdateMeasurementsAction
{
    public List<MeasurementModel> Measurements { get; set; }
}

public class SetMeasurementsLoadingAction
{
    public bool IsLoading { get; set; }
}