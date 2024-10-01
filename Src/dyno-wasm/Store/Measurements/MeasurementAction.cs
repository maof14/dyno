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

public class ReloadMeasurementViewAction
{

}

public class SetMeasurementIsRunningAction
{
    public bool Running { get; set; }
}

public class ClearMeasurementsAction
{

}

public class DeleteMeasurementAction
{
    public Guid Id { get; set; }
}