using Common;
using dyno_wasm.Components;
using Fluxor;
using Helpers;
using MudBlazor;
using MudBlazor.Extensions;
using SignalR;
using Store.Measurements;
using Store.SharedActions;

namespace Store.StartMeasurementDialogState;

public class StartMeasurementDialogEffect
{
    public IDialogService DialogService { get; }
    public IHubClient HubClient { get; }

    public StartMeasurementDialogEffect(IDialogService dialogService, IHubClient hubClient)
    {
        DialogService = dialogService;
        HubClient = hubClient;
    }

    [EffectMethod(typeof(InitStartMeasurementDialogAction))]
    public async Task OnInitStartMeasurementDialogEffect(IDispatcher dispatcher)
    {
        if (HubClient.ConnectionState != Microsoft.AspNetCore.SignalR.Client.HubConnectionState.Connected)
        {
            await HubClient.ConnectAsync();
        }

        var options = new DialogOptions { CloseOnEscapeKey = true };

        var dialog = await DialogService.ShowAsync<StartMeasurementDialog>("Simple Dialog", options);
        var result = await dialog.Result;

        if (result.Canceled) {
            return;
        }

        var dialogTempResult = result.Data.As<DialogTempResult>();

        dispatcher.Dispatch(new ToastSuccessAction() { SuccessMessage = $"Starting measurement with duration {dialogTempResult.Duration} second(s)." });
        dispatcher.Dispatch(new SetMeasurementIsRunningAction() { Running = true });
        await HubClient.SendMessage(SignalRMethods.MeasurementRequested, dialogTempResult.MeasurementName, dialogTempResult.MeasurementDescription, dialogTempResult.Duration);
        dispatcher.Dispatch(new SetMeasurementIsRunningAction() { Running = false });
    }
}