using Fluxor;
using ViewModels;

namespace dyno_gui.Store.Measurements;

public record MeasurementState
{
    public Guid SelectedMeasurement { get; init; }
    public List<MeasurementModel> Measurements { get; init; }
}

public class MeasurementFeature : Feature<MeasurementState>
{
    public override string GetName() => nameof(MeasurementState);
    protected override MeasurementState GetInitialState() => new MeasurementState();
}
