using Fluxor;
using ViewModels;

namespace Store.Measurements;

public record MeasurementState
{
    public Guid SelectedMeasurement { get; init; } = Guid.Empty;
    public List<MeasurementModel> Measurements { get; init; } = new List<MeasurementModel>();
}

public class MeasurementFeature : Feature<MeasurementState>
{
    public override string GetName() => nameof(MeasurementState);
    protected override MeasurementState GetInitialState() => new MeasurementState();
}
