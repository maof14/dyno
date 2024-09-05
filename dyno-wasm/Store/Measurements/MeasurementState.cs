using Fluxor;
using Microsoft.AspNetCore.Components.Web;
using ViewModels;

namespace Store.Measurements;

public record MeasurementState
{
    public Guid SelectedMeasurement { get; init; } = Guid.Empty;
    public List<MeasurementModel> Measurements { get; init; } = new List<MeasurementModel>();
    public bool IsLoading  { get; init; } = false;
}

public class MeasurementFeature : Feature<MeasurementState>
{
    public override string GetName() => nameof(MeasurementState);
    protected override MeasurementState GetInitialState() => new MeasurementState();
}
