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
}
