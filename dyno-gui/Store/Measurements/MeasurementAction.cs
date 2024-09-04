using ViewModels;

namespace dyno_gui.Store.Measurements;

public class InitMeasurementViewAction
{
}

public class UpdateMeasurementsAction
{
    public List<MeasurementModel> Measurements { get; set; }
}