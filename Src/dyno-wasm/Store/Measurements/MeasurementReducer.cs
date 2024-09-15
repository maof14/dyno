using Fluxor;

namespace Store.Measurements;

public class MeasurementReducer
{
    [ReducerMethod]
    public static MeasurementState OnUpdateMeasurementsAction(MeasurementState state, UpdateMeasurementsAction action)
    {
        return state with
        {
            Measurements = action.Measurements,
        };
    }

    [ReducerMethod]
    public static MeasurementState OnSetMeasurementLoadingAction(MeasurementState state, SetMeasurementsLoadingAction action)
    {
        return state with
        {
            IsLoading = action.IsLoading,
        };
    }

    [ReducerMethod]
    public static MeasurementState OnSetMeasurementIsRunningAction(MeasurementState state, SetMeasurementIsRunningAction action) 
    {
        return state with
        {
            Running = action.Running
        };
    }

    [ReducerMethod(typeof(ClearMeasurementsAction))]
    public static MeasurementState OnClearMeasurementsAction(MeasurementState state)
    {
        return state with
        {
            Measurements = []
        };
    }
}
