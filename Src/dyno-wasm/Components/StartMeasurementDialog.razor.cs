using Fluxor;
using Helpers;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Store.StartMeasurementDialogState;

namespace dyno_wasm.Components;

public partial class StartMeasurementDialog
{

    [Inject]
    public IState<StartMeasurementDialogState> DialogState { get; set; }

    public string _measurementName { get; set; }
    public string _measurementDescription { get; set; }
    public string _duration { get; set; }

    [CascadingParameter]
    private MudDialogInstance MudDialog { get; set; }

    private void Submit() => MudDialog.Close(DialogResult.Ok(new DialogTempResult(_measurementName, _measurementDescription, _duration)));

    private void Cancel() => MudDialog.Cancel();
}