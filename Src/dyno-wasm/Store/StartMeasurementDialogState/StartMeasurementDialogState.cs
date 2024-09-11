using Fluxor;

namespace Store.StartMeasurementDialogState;

public record StartMeasurementDialogState
{
    public string Name { get; set; }
    public string Description { get; set; }
}

public class StartMeasurementDialogFeature : Feature<StartMeasurementDialogState>
{
    public override string GetName() => nameof(StartMeasurementDialogState);
    protected override StartMeasurementDialogState GetInitialState() => new StartMeasurementDialogState();
}

