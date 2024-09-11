using Fluxor;
using ViewModels;

namespace Store.Measurements;

public record MeasurementState
{
    public bool Running { get; init; } = false;
    public List<MeasurementModel> Measurements { get; init; } = new List<MeasurementModel>();
    public bool IsLoading  { get; init; } = false;
}

public class MeasurementFeature : Feature<MeasurementState>
{
    public override string GetName() => nameof(MeasurementState);
    protected override MeasurementState GetInitialState() => new MeasurementState();
}
